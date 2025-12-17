using DataAccessLayer;
using DataAccessLayer.Mappers;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PRN222_Group4_FinalProject_FPTUResearchPaperManagement.Hubs;
using Repository;
using Repository.Interfaces;
using Service;
using Service.Interfaces;
using System.Security.Claims;
using System.Text;
using Google.Cloud.Storage.V1;
using Service.Options;

var builder = WebApplication.CreateBuilder(args);

// EF
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repos + Services
builder.Services.AddSingleton<StorageClient>(sp =>
{
    var env = sp.GetRequiredService<IWebHostEnvironment>();

    var credentialPaths = Path.Combine(
        env.ContentRootPath,
        "Credentials",
        "fpturesearchpapermanagement-3465b1d3b88e.json"
    );

    var credential = GoogleCredential.FromFile(credentialPaths);
    return StorageClient.Create(credential);
});
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ISemesterRepository, SemesterRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<ITopicRepository, TopicRepository>();
builder.Services.AddScoped<ISubmissionRepository, SubmissionRepository>();
builder.Services.AddScoped<ISubmissionFileRepository, SubmissionFileRepository>();
builder.Services.AddScoped<IStudentGroupRepository, StudentGroupRepository>();
builder.Services.AddScoped<ITopicService, TopicService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ISemesterService, SemesterService>();
builder.Services.AddScoped<ISubmissionService, SubmissionService>();
builder.Services.AddScoped<IOpenAiSubmissionService, OpenAiSubmissionService>();
builder.Services.AddScoped<ISuspensionService, SuspensionService>();
builder.Services.AddScoped<ISuspensionRepository, SuspensionRepository>();
builder.Services.AddScoped<IReviewLogRepository, ReviewLogRepository>();
builder.Services.AddScoped<IReviewLogService, ReviewLogService>();
builder.Services.Configure<OpenAiOptions>(
    builder.Configuration.GetSection("OpenAI"));
builder.Services.AddRazorPages();

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile(new MappingProfile());
});

// Authentication configuration
var jwtSettings = builder.Configuration.GetSection("Jwt");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            if (context.Request.Cookies.ContainsKey("jwt"))
            {
                context.Token = context.Request.Cookies["jwt"];
            }
            return Task.CompletedTask;
        },

        OnChallenge = context =>
        {
            context.HandleResponse();
            Console.WriteLine("Challenge failed");
            context.Response.Redirect("/Authentication/Login");
            return Task.CompletedTask;
        }
    };

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]!)),
        NameClaimType = ClaimTypes.Name,
        RoleClaimType = ClaimTypes.Role
    };
});

builder.Services.AddSignalR();

builder.Services.AddAuthorization();

builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeFolder("/");
    options.Conventions.AllowAnonymousToPage("/Authentication/Login");
    options.Conventions.AllowAnonymousToFolder("/Administrator");
});

// var credentialPath = Path.Combine(
//     Directory.GetCurrentDirectory(),
//     "Credentials",
//     "fpturesearchpapermanagement-3465b1d3b88e.json"
// );

// FirebaseApp.Create(new AppOptions
// {
//     Credential = GoogleCredential.FromFile(credentialPath)
// });

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.MapHub<NotificationHub>("/notificationHub");
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
app.Run();
