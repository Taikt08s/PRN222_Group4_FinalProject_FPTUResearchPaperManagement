using BusinessObject.Enums;
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

        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

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

            // ===== SAMPLE DATA SEEDING =====
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    Full_Name = "Administrator",
                    Email = "admin@fpt.edu.vn",
                    Password_Hash = "1",
                    Role = AccountRole.Administrator.ToString(),
                    Major = "System Administrator",
                    Is_Suspended = false,
                    Created_At = DateTime.UtcNow
                },
                new User
                {
                    Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    Full_Name = "Nguyen Van Anh",
                    Email = "anh@fpt.edu.vn",
                    Password_Hash = "1",
                    Role = AccountRole.Student.ToString(),
                    Major = "Ngôn Ngữ Anh",
                    Is_Suspended = false,
                    Created_At = DateTime.UtcNow
                },
                new User
                {
                    Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    Full_Name = "Nguyen Van Bao",
                    Email = "bao@fpt.edu.vn",
                    Password_Hash = "1",
                    Role = AccountRole.Student.ToString(),
                    Major = "Ngôn Ngữ Anh",
                    Is_Suspended = false,
                    Created_At = DateTime.UtcNow
                },
                new User
                {
                    Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                    Full_Name = "Le Thi Cam",
                    Email = "cam@fpt.edu.vn",
                    Password_Hash = "1",
                    Role = AccountRole.Student.ToString(),
                    Major = "Quản Trị Kinh Doanh",
                    Is_Suspended = false,
                    Created_At = DateTime.UtcNow
                },
                new User
                {
                    Id = Guid.Parse("55555555-5555-5555-5555-555555555555"),
                    Full_Name = "Tran Minh Duc",
                    Email = "duc@fpt.edu.vn",
                    Password_Hash = "1",
                    Role = AccountRole.Student.ToString(),
                    Major = "Quản Trị Kinh Doanh",
                    Is_Suspended = false,
                    Created_At = DateTime.UtcNow
                },
                new User
                {
                    Id = Guid.Parse("66666666-6666-6666-6666-666666666666"),
                    Full_Name = "Pham Quoc Huy",
                    Email = "huy@fpt.edu.vn",
                    Password_Hash = "1",
                    Role = AccountRole.Student.ToString(),
                    Major = "Kỹ Thuật Phần Mềm",
                    Is_Suspended = false,
                    Created_At = DateTime.UtcNow
                },
                new User
                {
                    Id = Guid.Parse("77777777-7777-7777-7777-777777777777"),
                    Full_Name = "Vo Thi Khanh",
                    Email = "khanh@fpt.edu.vn",
                    Password_Hash = "1",
                    Role = AccountRole.Student.ToString(),
                    Major = "Kỹ Thuật Phần Mềm",
                    Is_Suspended = false,
                    Created_At = DateTime.UtcNow
                },
                new User
                {
                    Id = Guid.Parse("88888888-8888-8888-8888-888888888888"),
                    Full_Name = "Nguyen Hoang Lam",
                    Email = "lam@fpt.edu.vn",
                    Password_Hash = "1",
                    Role = AccountRole.Student.ToString(),
                    Major = "Kỹ Thuật Phần Mềm",
                    Is_Suspended = false,
                    Created_At = DateTime.UtcNow
                },
                new User
                {
                    Id = Guid.Parse("99999999-9999-9999-9999-999999999999"),
                    Full_Name = "Tran Thi My",
                    Email = "my@fpt.edu.vn",
                    Password_Hash = "1",
                    Role = AccountRole.Student.ToString(),
                    Major = "Kỹ Thuật Phần Mềm",
                    Is_Suspended = false,
                    Created_At = DateTime.UtcNow
                },
                new User
                {
                    Id = Guid.Parse("aaaaaaa1-aaaa-aaaa-aaaa-aaaaaaaaaaa1"),
                    Full_Name = "Pham Thanh Son",
                    Email = "son.instructor@fpt.edu.vn",
                    Password_Hash = "1",
                    Role = AccountRole.Instructor.ToString(),
                    Major = "Teacher",
                    Is_Suspended = false,
                    Created_At = DateTime.UtcNow
                },
                new User
                {
                    Id = Guid.Parse("aaaaaaa2-aaaa-aaaa-aaaa-aaaaaaaaaaa2"),
                    Full_Name = "Le Thi Ha",
                    Email = "ha.instructor@fpt.edu.vn",
                    Password_Hash = "1",
                    Role = AccountRole.Instructor.ToString(),
                    Major = "Teacher",
                    Is_Suspended = false,
                    Created_At = DateTime.UtcNow
                },
                new User
                {
                    Id = Guid.Parse("bbbbbbb1-bbbb-bbbb-bbbb-bbbbbbbbbbb1"),
                    Full_Name = "Nguyen Minh Tri",
                    Email = "tri.committee@fpt.edu.vn",
                    Password_Hash = "1",
                    Role = AccountRole.GraduationProjectEvaluationCommitteeMember.ToString(),
                    Major = "Teacher",
                    Is_Suspended = false,
                    Created_At = DateTime.UtcNow
                },
                new User
                {
                    Id = Guid.Parse("bbbbbbb2-bbbb-bbbb-bbbb-bbbbbbbbbbb2"),
                    Full_Name = "Tran Hoai Thu",
                    Email = "thu.committee@fpt.edu.vn",
                    Password_Hash = "1",
                    Role = AccountRole.GraduationProjectEvaluationCommitteeMember.ToString(),
                    Major = "Teacher",
                    Is_Suspended = false,
                    Created_At = DateTime.UtcNow
                },
                new User
                {
                    Id = Guid.Parse("bbbbbbb3-bbbb-bbbb-bbbb-bbbbbbbbbbb3"),
                    Full_Name = "Vo Quoc Duy",
                    Email = "duy.committee@fpt.edu.vn",
                    Password_Hash = "1",
                    Role = AccountRole.GraduationProjectEvaluationCommitteeMember.ToString(),
                    Major = "Teacher",
                    Is_Suspended = false,
                    Created_At = DateTime.UtcNow
                },
                new User
                {
                    Id = Guid.Parse("bbbbbbb4-bbbb-bbbb-bbbb-bbbbbbbbbbb4"),
                    Full_Name = "Pham Thi Yen",
                    Email = "yen.committee@fpt.edu.vn",
                    Password_Hash = "1",
                    Role = AccountRole.GraduationProjectEvaluationCommitteeMember.ToString(),
                    Major = "Teacher",
                    Is_Suspended = false,
                    Created_At = DateTime.UtcNow
                }
            );

            modelBuilder.Entity<Semester>().HasData(
                new Semester
                {
                    Id = 1,
                    Year = 2025,
                    Term = "Fall 2025",
                    Start_Date = new DateTime(2025, 8, 1),
                    End_Date = new DateTime(2025, 12, 31)
                },
                new Semester
                {
                    Id = 2,
                    Year = 2026,
                    Term = "Spring 2026",
                    Start_Date = new DateTime(2026, 1, 1),
                    End_Date = new DateTime(2026, 4, 30)
                },
                new Semester
                {
                    Id = 3,
                    Year = 2026,
                    Term = "Summer 2026",
                    Start_Date = new DateTime(2026, 5, 1),
                    End_Date = new DateTime(2026, 8, 31)
                }
            );
            modelBuilder.Entity<Topic>().HasData(
                new Topic
                {
                    Id = 1,
                    Title =
                        "Measuring Vietnamese EFL learners’ productive derivative knowledge in a decontextualized test format",
                    Description =
                        "Đo lường vốn từ vựng phái sinh của sinh viên học tiếng Anh như một ngoại ngữ bằng bài kiểm tra không có ngữ cảnh",
                    Created_By = Guid.Parse("aaaaaaa1-aaaa-aaaa-aaaa-aaaaaaaaaaa1"),
                    Major = "Ngôn Ngữ Anh",
                    Is_Group_Required = false,
                    Status = TopicStatus.Created.ToString(),
                    SubmissionInstruction = "Students must upload their project files in PDF format to the correct assigned folder. " + "Each submission will be reviewed by the project supervisor and members of the board committee. " + "If the submission is rejected, the student must resubmit it before the deadline. Once the supervisor has approved the project or the secretary has confirmed it, or when the project deadline has passed, the student will no longer be able to modify or upload any files. " + "The system also checks for plagiarism; if plagiarism is detected, the student will be suspended." + "\n\nSinh viên phải upload file đồ án dưới dạng PDF vào đúng thư mục được chỉ định. " + "Mỗi bài nộp sẽ được giảng viên hướng dẫn và các thành viên hội đồng xét duyệt. " + "Nếu bài nộp bị từ chối, sinh viên phải điều chỉnh và nộp lại trước hạn chót. Khi giảng viên đã phê duyệt, hoặc thư ký đã xác nhận, hoặc khi hết hạn nộp bài, sinh viên sẽ không thể chỉnh sửa hoặc upload bất kỳ file nào khác. " + "Hệ thống cũng tự động kiểm tra đạo văn, nếu phát hiện đạo văn, sinh viên sẽ bị đình chỉ.",
                    Semester_Id = 1
                },
                new Topic
                {
                    Id = 2,
                    Title = "Predictors of Classroom Anxiety in General English (ENT) classes at FPT University",
                    Description =
                        "Gọi ngẫu nhiên, làm việc nhóm và kiểm tra trắc nghiệm trực tuyến trong vai trò dự báo lo âu trong lớp học tiếng Anh dự bị (ENT) tại Trường Đại học FPT",
                    Created_By = Guid.Parse("aaaaaaa1-aaaa-aaaa-aaaa-aaaaaaaaaaa1"),
                    Major = "Ngôn Ngữ Anh",
                    Is_Group_Required = false,
                    Status = TopicStatus.Created.ToString(),
                    SubmissionInstruction = "Students must upload their project files in PDF format to the correct assigned folder. " + "Each submission will be reviewed by the project supervisor and members of the board committee. " + "If the submission is rejected, the student must resubmit it before the deadline. Once the supervisor has approved the project or the secretary has confirmed it, or when the project deadline has passed, the student will no longer be able to modify or upload any files. " + "The system also checks for plagiarism; if plagiarism is detected, the student will be suspended." + "\n\nSinh viên phải upload file đồ án dưới dạng PDF vào đúng thư mục được chỉ định. " + "Mỗi bài nộp sẽ được giảng viên hướng dẫn và các thành viên hội đồng xét duyệt. " + "Nếu bài nộp bị từ chối, sinh viên phải điều chỉnh và nộp lại trước hạn chót. Khi giảng viên đã phê duyệt, hoặc thư ký đã xác nhận, hoặc khi hết hạn nộp bài, sinh viên sẽ không thể chỉnh sửa hoặc upload bất kỳ file nào khác. " + "Hệ thống cũng tự động kiểm tra đạo văn, nếu phát hiện đạo văn, sinh viên sẽ bị đình chỉ.",
                    Semester_Id = 1
                },
                new Topic
                {
                    Id = 3,
                    Title = "HomePlus- Smart Living Services Portal for Apartment Residents",
                    Description = "HomePlus - Cổng dịch vụ sống thông minh cho cư dân",
                    Created_By = Guid.Parse("aaaaaaa2-aaaa-aaaa-aaaa-aaaaaaaaaaa2"),
                    Major = "Kỹ Thuật Phần Mềm",
                    Is_Group_Required = true,
                    Status = TopicStatus.Created.ToString(),
                    SubmissionInstruction = "Students must upload their project files in PDF format to the correct assigned folder. " + "Each submission will be reviewed by the project supervisor and members of the board committee. " + "If the submission is rejected, the student must resubmit it before the deadline. Once the supervisor has approved the project or the secretary has confirmed it, or when the project deadline has passed, the student will no longer be able to modify or upload any files. " + "The system also checks for plagiarism; if plagiarism is detected, the student will be suspended." + "\n\nSinh viên phải upload file đồ án dưới dạng PDF vào đúng thư mục được chỉ định. " + "Mỗi bài nộp sẽ được giảng viên hướng dẫn và các thành viên hội đồng xét duyệt. " + "Nếu bài nộp bị từ chối, sinh viên phải điều chỉnh và nộp lại trước hạn chót. Khi giảng viên đã phê duyệt, hoặc thư ký đã xác nhận, hoặc khi hết hạn nộp bài, sinh viên sẽ không thể chỉnh sửa hoặc upload bất kỳ file nào khác. " + "Hệ thống cũng tự động kiểm tra đạo văn, nếu phát hiện đạo văn, sinh viên sẽ bị đình chỉ.",
                    Semester_Id = 1
                }
            );
        }
    }
}