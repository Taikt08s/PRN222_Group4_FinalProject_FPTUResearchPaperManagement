using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccessLayer
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Semester> Semesters { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<StudentGroup> StudentGroups { get; set; }
        public DbSet<StudentGroupMember> StudentGroupMembers { get; set; }
        public DbSet<Submission> Submissions { get; set; }
        public DbSet<SubmissionFile> SubmissionFiles { get; set; }
        public DbSet<ReviewLog> ReviewLogs { get; set; }
        public DbSet<SuspensionRecord> SuspensionRecords { get; set; }
        public DbSet<ThesisModeration> ThesisModerations { get; set; }
        public DbSet<DashboardCache> DashboardCaches { get; set; }

        public AppDbContext() { }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    => optionsBuilder.UseSqlServer(GetConnectionString());

        private string GetConnectionString()
        {
            IConfiguration config = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
                      .AddJsonFile("appsettings.json", true, true)
                      .Build();
            var strConn = config["ConnectionStrings:DefaultConnection"];

            return strConn;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasIndex(x => x.Email)
                .IsUnique();

            // Topic → Semester
            modelBuilder.Entity<Topic>()
                .HasOne(t => t.Semester)
                .WithMany(s => s.Topics)
                .HasForeignKey(t => t.Semester_Id).OnDelete(DeleteBehavior.Restrict);

            // Topic → Creator
            modelBuilder.Entity<Topic>()
                .HasOne(t => t.Creator)
                .WithMany(u => u.Topics)
                .HasForeignKey(t => t.Created_By).OnDelete(DeleteBehavior.Restrict);

            // StudentGroup → Topic
            modelBuilder.Entity<StudentGroup>()
                .HasOne(g => g.Topic)
                .WithMany(t => t.Groups)
                .HasForeignKey(g => g.Topic_Id).OnDelete(DeleteBehavior.Restrict);

            // StudentGroup → User (Creator)
            modelBuilder.Entity<StudentGroup>()
                .HasOne(g => g.Creator)
                .WithMany(u => u.CreatedGroups)
                .HasForeignKey(g => g.Created_By).OnDelete(DeleteBehavior.Restrict);

            // StudentGroupMember → StudentGroup
            modelBuilder.Entity<StudentGroupMember>()
                .HasOne(m => m.Group)
                .WithMany(g => g.Members)
                .HasForeignKey(m => m.Group_Id).OnDelete(DeleteBehavior.Restrict);

            // StudentGroupMember → User
            modelBuilder.Entity<StudentGroupMember>()
                .HasOne(m => m.Student)
                .WithMany()
                .HasForeignKey(m => m.Student_Id).OnDelete(DeleteBehavior.Restrict);

            // Submission → Group
            modelBuilder.Entity<Submission>()
                .HasOne(s => s.Group)
                .WithMany()
                .HasForeignKey(s => s.Group_Id).OnDelete(DeleteBehavior.Restrict);

            // Submission → Topic
            modelBuilder.Entity<Submission>()
                .HasOne(s => s.Topic)
                .WithMany()
                .HasForeignKey(s => s.Topic_Id).OnDelete(DeleteBehavior.Restrict);

            // Submission → Semester
            modelBuilder.Entity<Submission>()
                .HasOne(s => s.Semester)
                .WithMany()
                .HasForeignKey(s => s.Semester_Id).OnDelete(DeleteBehavior.Restrict);

            // SubmissionFile → Submission
            modelBuilder.Entity<SubmissionFile>()
                .HasOne(f => f.Submission)
                .WithMany(s => s.Files)
                .HasForeignKey(f => f.Submission_Id).OnDelete(DeleteBehavior.Restrict);

            // ReviewLog → Submission
            modelBuilder.Entity<ReviewLog>()
                .HasOne(r => r.Submission)
                .WithMany()
                .HasForeignKey(r => r.Submission_Id).OnDelete(DeleteBehavior.Restrict);

            // ReviewLog → Reviewer (User)
            modelBuilder.Entity<ReviewLog>()
                .HasOne(r => r.Reviewer)
                .WithMany()
                .HasForeignKey(r => r.Reviewer_Id).OnDelete(DeleteBehavior.Restrict);

            // SuspensionRecord → Submission
            modelBuilder.Entity<SuspensionRecord>()
                .HasOne(s => s.Submission)
                .WithMany()
                .HasForeignKey(s => s.Submission_Id).OnDelete(DeleteBehavior.Restrict);

            // SuspensionRecord → Student (User)
            modelBuilder.Entity<SuspensionRecord>()
                .HasOne(s => s.Student)
                .WithMany()
                .HasForeignKey(s => s.Student_Id).OnDelete(DeleteBehavior.Restrict);

            // ThesisModeration → Submission (1-1)
            modelBuilder.Entity<ThesisModeration>()
                .HasOne(m => m.Submission)
                .WithOne(s => s.Moderation)
                .HasForeignKey<ThesisModeration>(m => m.Submission_Id).OnDelete(DeleteBehavior.Restrict);

            // DashboardCache → Semester
            modelBuilder.Entity<DashboardCache>()
                .HasOne(d => d.Semester)
                .WithMany()
                .HasForeignKey(d => d.Semester_Id).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
