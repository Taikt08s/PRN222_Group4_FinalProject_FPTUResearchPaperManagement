using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Semesters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Term = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Start_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    End_Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Semesters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Full_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password_Hash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Major = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Is_Suspended = table.Column<bool>(type: "bit", nullable: false),
                    Suspended_Until = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DashboardCaches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Semester_Id = table.Column<int>(type: "int", nullable: false),
                    Total_Students = table.Column<int>(type: "int", nullable: false),
                    Submitted_Count = table.Column<int>(type: "int", nullable: false),
                    Approved_Count = table.Column<int>(type: "int", nullable: false),
                    Plagiarism_Count = table.Column<int>(type: "int", nullable: false),
                    Reject_Count = table.Column<int>(type: "int", nullable: false),
                    Generated_At = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DashboardCaches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DashboardCaches_Semesters_Semester_Id",
                        column: x => x.Semester_Id,
                        principalTable: "Semesters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Topics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubmissionInstruction = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Major = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Semester_Id = table.Column<int>(type: "int", nullable: false),
                    Created_By = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Is_Group_Required = table.Column<bool>(type: "bit", nullable: false),
                    Deadline_Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Topics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Topics_Semesters_Semester_Id",
                        column: x => x.Semester_Id,
                        principalTable: "Semesters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Topics_Users_Created_By",
                        column: x => x.Created_By,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StudentGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Topic_Id = table.Column<int>(type: "int", nullable: false),
                    Created_By = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentGroups_Topics_Topic_Id",
                        column: x => x.Topic_Id,
                        principalTable: "Topics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentGroups_Users_Created_By",
                        column: x => x.Created_By,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StudentGroupMembers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Group_Id = table.Column<int>(type: "int", nullable: false),
                    Student_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Is_Leader = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentGroupMembers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentGroupMembers_StudentGroups_Group_Id",
                        column: x => x.Group_Id,
                        principalTable: "StudentGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentGroupMembers_Users_Student_Id",
                        column: x => x.Student_Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Submissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Group_Id = table.Column<int>(type: "int", nullable: false),
                    Topic_Id = table.Column<int>(type: "int", nullable: false),
                    Semester_Id = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Plagiarism_Score = table.Column<float>(type: "real", nullable: false),
                    Plagiarism_Flag = table.Column<bool>(type: "bit", nullable: false),
                    Reject_Reason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Submitted_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Reviewed_At = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Submissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Submissions_Semesters_Semester_Id",
                        column: x => x.Semester_Id,
                        principalTable: "Semesters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Submissions_StudentGroups_Group_Id",
                        column: x => x.Group_Id,
                        principalTable: "StudentGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Submissions_Topics_Topic_Id",
                        column: x => x.Topic_Id,
                        principalTable: "Topics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReviewLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Submission_Id = table.Column<int>(type: "int", nullable: false),
                    Reviewer_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Original_Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    New_Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReviewLogs_Submissions_Submission_Id",
                        column: x => x.Submission_Id,
                        principalTable: "Submissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReviewLogs_Users_Reviewer_Id",
                        column: x => x.Reviewer_Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SubmissionFiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Submission_Id = table.Column<int>(type: "int", nullable: false),
                    File_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    File_Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Firebase_Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Uploaded_At = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubmissionFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubmissionFiles_Submissions_Submission_Id",
                        column: x => x.Submission_Id,
                        principalTable: "Submissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SuspensionRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Student_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Submission_Id = table.Column<int>(type: "int", nullable: false),
                    Detected_By_Ai = table.Column<bool>(type: "bit", nullable: false),
                    Approved_By_Gpec = table.Column<bool>(type: "bit", nullable: false),
                    Approved_By_Sdc = table.Column<bool>(type: "bit", nullable: false),
                    Start_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    End_Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuspensionRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SuspensionRecords_Submissions_Submission_Id",
                        column: x => x.Submission_Id,
                        principalTable: "Submissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SuspensionRecords_Users_Student_Id",
                        column: x => x.Student_Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ThesisModerations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Submission_Id = table.Column<int>(type: "int", nullable: false),
                    Plagiarism_Pass = table.Column<bool>(type: "bit", nullable: false),
                    Plagiarism_Score = table.Column<float>(type: "real", nullable: false),
                    Originality_Pass = table.Column<bool>(type: "bit", nullable: false),
                    Citation_Quality_Pass = table.Column<bool>(type: "bit", nullable: false),
                    Academic_Rigor_Pass = table.Column<bool>(type: "bit", nullable: false),
                    Research_Relevance_Pass = table.Column<bool>(type: "bit", nullable: false),
                    Writing_Quality_Pass = table.Column<bool>(type: "bit", nullable: false),
                    Ethical_Compliance_Pass = table.Column<bool>(type: "bit", nullable: false),
                    Completeness_Pass = table.Column<bool>(type: "bit", nullable: false),
                    Is_Approved = table.Column<bool>(type: "bit", nullable: false),
                    Reasoning = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Violations_Json = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThesisModerations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ThesisModerations_Submissions_Submission_Id",
                        column: x => x.Submission_Id,
                        principalTable: "Submissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Semesters",
                columns: new[] { "Id", "End_Date", "Start_Date", "Term", "Year" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Fall 2025", 2025 },
                    { 2, new DateTime(2026, 4, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Spring 2026", 2026 },
                    { 3, new DateTime(2026, 8, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Summer 2026", 2026 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Created_At", "Email", "Full_Name", "Is_Suspended", "Major", "Password_Hash", "Role", "Suspended_Until" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2025, 12, 11, 2, 32, 48, 606, DateTimeKind.Utc).AddTicks(8346), "admin@fpt.edu.vn", "Administrator", false, "System Administrator", "1", "Administrator", null },
                    { new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2025, 12, 11, 2, 32, 48, 606, DateTimeKind.Utc).AddTicks(8351), "anh@fpt.edu.vn", "Nguyen Van Anh", false, "Ngôn Ngữ Anh", "1", "Student", null },
                    { new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2025, 12, 11, 2, 32, 48, 606, DateTimeKind.Utc).AddTicks(8353), "bao@fpt.edu.vn", "Nguyen Van Bao", false, "Ngôn Ngữ Anh", "1", "Student", null },
                    { new Guid("44444444-4444-4444-4444-444444444444"), new DateTime(2025, 12, 11, 2, 32, 48, 606, DateTimeKind.Utc).AddTicks(8355), "cam@fpt.edu.vn", "Le Thi Cam", false, "Quản Trị Kinh Doanh", "1", "Student", null },
                    { new Guid("55555555-5555-5555-5555-555555555555"), new DateTime(2025, 12, 11, 2, 32, 48, 606, DateTimeKind.Utc).AddTicks(8357), "duc@fpt.edu.vn", "Tran Minh Duc", false, "Quản Trị Kinh Doanh", "1", "Student", null },
                    { new Guid("66666666-6666-6666-6666-666666666666"), new DateTime(2025, 12, 11, 2, 32, 48, 606, DateTimeKind.Utc).AddTicks(8359), "huy@fpt.edu.vn", "Pham Quoc Huy", false, "Kỹ Thuật Phần Mềm", "1", "Student", null },
                    { new Guid("77777777-7777-7777-7777-777777777777"), new DateTime(2025, 12, 11, 2, 32, 48, 606, DateTimeKind.Utc).AddTicks(8361), "khanh@fpt.edu.vn", "Vo Thi Khanh", false, "Kỹ Thuật Phần Mềm", "1", "Student", null },
                    { new Guid("88888888-8888-8888-8888-888888888888"), new DateTime(2025, 12, 11, 2, 32, 48, 606, DateTimeKind.Utc).AddTicks(8363), "lam@fpt.edu.vn", "Nguyen Hoang Lam", false, "Kỹ Thuật Phần Mềm", "1", "Student", null },
                    { new Guid("99999999-9999-9999-9999-999999999999"), new DateTime(2025, 12, 11, 2, 32, 48, 606, DateTimeKind.Utc).AddTicks(8365), "my@fpt.edu.vn", "Tran Thi My", false, "Kỹ Thuật Phần Mềm", "1", "Student", null },
                    { new Guid("aaaaaaa1-aaaa-aaaa-aaaa-aaaaaaaaaaa1"), new DateTime(2025, 12, 11, 2, 32, 48, 606, DateTimeKind.Utc).AddTicks(8367), "son.instructor@fpt.edu.vn", "Pham Thanh Son", false, "Teacher", "1", "Instructor", null },
                    { new Guid("aaaaaaa2-aaaa-aaaa-aaaa-aaaaaaaaaaa2"), new DateTime(2025, 12, 11, 2, 32, 48, 606, DateTimeKind.Utc).AddTicks(8369), "ha.instructor@fpt.edu.vn", "Le Thi Ha", false, "Teacher", "1", "Instructor", null },
                    { new Guid("bbbbbbb1-bbbb-bbbb-bbbb-bbbbbbbbbbb1"), new DateTime(2025, 12, 11, 2, 32, 48, 606, DateTimeKind.Utc).AddTicks(8371), "tri.committee@fpt.edu.vn", "Nguyen Minh Tri", false, "Teacher", "1", "GraduationProjectEvaluationCommitteeMember", null },
                    { new Guid("bbbbbbb2-bbbb-bbbb-bbbb-bbbbbbbbbbb2"), new DateTime(2025, 12, 11, 2, 32, 48, 606, DateTimeKind.Utc).AddTicks(8373), "thu.committee@fpt.edu.vn", "Tran Hoai Thu", false, "Teacher", "1", "GraduationProjectEvaluationCommitteeMember", null },
                    { new Guid("bbbbbbb3-bbbb-bbbb-bbbb-bbbbbbbbbbb3"), new DateTime(2025, 12, 11, 2, 32, 48, 606, DateTimeKind.Utc).AddTicks(8375), "duy.committee@fpt.edu.vn", "Vo Quoc Duy", false, "Teacher", "1", "GraduationProjectEvaluationCommitteeMember", null },
                    { new Guid("bbbbbbb4-bbbb-bbbb-bbbb-bbbbbbbbbbb4"), new DateTime(2025, 12, 11, 2, 32, 48, 606, DateTimeKind.Utc).AddTicks(8377), "yen.committee@fpt.edu.vn", "Pham Thi Yen", false, "Teacher", "1", "GraduationProjectEvaluationCommitteeMember", null }
                });

            migrationBuilder.InsertData(
                table: "Topics",
                columns: new[] { "Id", "Created_By", "Deadline_Date", "Description", "Is_Group_Required", "Major", "Semester_Id", "SubmissionInstruction", "Title" },
                values: new object[,]
                {
                    { 1, new Guid("aaaaaaa1-aaaa-aaaa-aaaa-aaaaaaaaaaa1"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Đo lường vốn từ vựng phái sinh của sinh viên học tiếng Anh như một ngoại ngữ bằng bài kiểm tra không có ngữ cảnh", false, "Ngôn Ngữ Anh", 1, "Students must upload their project files in PDF format to the correct assigned folder. Each submission will be reviewed by the project supervisor and members of the board committee. If the submission is rejected, the student must resubmit it before the deadline. Once the supervisor has approved the project or the secretary has confirmed it, or when the project deadline has passed, the student will no longer be able to modify or upload any files. The system also checks for plagiarism; if plagiarism is detected, the student will be suspended.\n\nSinh viên phải upload file đồ án dưới dạng PDF vào đúng thư mục được chỉ định. Mỗi bài nộp sẽ được giảng viên hướng dẫn và các thành viên hội đồng xét duyệt. Nếu bài nộp bị từ chối, sinh viên phải điều chỉnh và nộp lại trước hạn chót. Khi giảng viên đã phê duyệt, hoặc thư ký đã xác nhận, hoặc khi hết hạn nộp bài, sinh viên sẽ không thể chỉnh sửa hoặc upload bất kỳ file nào khác. Hệ thống cũng tự động kiểm tra đạo văn, nếu phát hiện đạo văn, sinh viên sẽ bị đình chỉ.", "Measuring Vietnamese EFL learners’ productive derivative knowledge in a decontextualized test format" },
                    { 2, new Guid("aaaaaaa1-aaaa-aaaa-aaaa-aaaaaaaaaaa1"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Gọi ngẫu nhiên, làm việc nhóm và kiểm tra trắc nghiệm trực tuyến trong vai trò dự báo lo âu trong lớp học tiếng Anh dự bị (ENT) tại Trường Đại học FPT", false, "Ngôn Ngữ Anh", 1, "Students must upload their project files in PDF format to the correct assigned folder. Each submission will be reviewed by the project supervisor and members of the board committee. If the submission is rejected, the student must resubmit it before the deadline. Once the supervisor has approved the project or the secretary has confirmed it, or when the project deadline has passed, the student will no longer be able to modify or upload any files. The system also checks for plagiarism; if plagiarism is detected, the student will be suspended.\n\nSinh viên phải upload file đồ án dưới dạng PDF vào đúng thư mục được chỉ định. Mỗi bài nộp sẽ được giảng viên hướng dẫn và các thành viên hội đồng xét duyệt. Nếu bài nộp bị từ chối, sinh viên phải điều chỉnh và nộp lại trước hạn chót. Khi giảng viên đã phê duyệt, hoặc thư ký đã xác nhận, hoặc khi hết hạn nộp bài, sinh viên sẽ không thể chỉnh sửa hoặc upload bất kỳ file nào khác. Hệ thống cũng tự động kiểm tra đạo văn, nếu phát hiện đạo văn, sinh viên sẽ bị đình chỉ.", "Predictors of Classroom Anxiety in General English (ENT) classes at FPT University" },
                    { 3, new Guid("aaaaaaa2-aaaa-aaaa-aaaa-aaaaaaaaaaa2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "HomePlus - Cổng dịch vụ sống thông minh cho cư dân", true, "Kỹ Thuật Phần Mềm", 1, "Students must upload their project files in PDF format to the correct assigned folder. Each submission will be reviewed by the project supervisor and members of the board committee. If the submission is rejected, the student must resubmit it before the deadline. Once the supervisor has approved the project or the secretary has confirmed it, or when the project deadline has passed, the student will no longer be able to modify or upload any files. The system also checks for plagiarism; if plagiarism is detected, the student will be suspended.\n\nSinh viên phải upload file đồ án dưới dạng PDF vào đúng thư mục được chỉ định. Mỗi bài nộp sẽ được giảng viên hướng dẫn và các thành viên hội đồng xét duyệt. Nếu bài nộp bị từ chối, sinh viên phải điều chỉnh và nộp lại trước hạn chót. Khi giảng viên đã phê duyệt, hoặc thư ký đã xác nhận, hoặc khi hết hạn nộp bài, sinh viên sẽ không thể chỉnh sửa hoặc upload bất kỳ file nào khác. Hệ thống cũng tự động kiểm tra đạo văn, nếu phát hiện đạo văn, sinh viên sẽ bị đình chỉ.", "HomePlus- Smart Living Services Portal for Apartment Residents" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DashboardCaches_Semester_Id",
                table: "DashboardCaches",
                column: "Semester_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewLogs_Reviewer_Id",
                table: "ReviewLogs",
                column: "Reviewer_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewLogs_Submission_Id",
                table: "ReviewLogs",
                column: "Submission_Id");

            migrationBuilder.CreateIndex(
                name: "IX_StudentGroupMembers_Group_Id",
                table: "StudentGroupMembers",
                column: "Group_Id");

            migrationBuilder.CreateIndex(
                name: "IX_StudentGroupMembers_Student_Id",
                table: "StudentGroupMembers",
                column: "Student_Id");

            migrationBuilder.CreateIndex(
                name: "IX_StudentGroups_Created_By",
                table: "StudentGroups",
                column: "Created_By");

            migrationBuilder.CreateIndex(
                name: "IX_StudentGroups_Topic_Id",
                table: "StudentGroups",
                column: "Topic_Id");

            migrationBuilder.CreateIndex(
                name: "IX_SubmissionFiles_Submission_Id",
                table: "SubmissionFiles",
                column: "Submission_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Submissions_Group_Id",
                table: "Submissions",
                column: "Group_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Submissions_Semester_Id",
                table: "Submissions",
                column: "Semester_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Submissions_Topic_Id",
                table: "Submissions",
                column: "Topic_Id");

            migrationBuilder.CreateIndex(
                name: "IX_SuspensionRecords_Student_Id",
                table: "SuspensionRecords",
                column: "Student_Id");

            migrationBuilder.CreateIndex(
                name: "IX_SuspensionRecords_Submission_Id",
                table: "SuspensionRecords",
                column: "Submission_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ThesisModerations_Submission_Id",
                table: "ThesisModerations",
                column: "Submission_Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Topics_Created_By",
                table: "Topics",
                column: "Created_By");

            migrationBuilder.CreateIndex(
                name: "IX_Topics_Semester_Id",
                table: "Topics",
                column: "Semester_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DashboardCaches");

            migrationBuilder.DropTable(
                name: "ReviewLogs");

            migrationBuilder.DropTable(
                name: "StudentGroupMembers");

            migrationBuilder.DropTable(
                name: "SubmissionFiles");

            migrationBuilder.DropTable(
                name: "SuspensionRecords");

            migrationBuilder.DropTable(
                name: "ThesisModerations");

            migrationBuilder.DropTable(
                name: "Submissions");

            migrationBuilder.DropTable(
                name: "StudentGroups");

            migrationBuilder.DropTable(
                name: "Topics");

            migrationBuilder.DropTable(
                name: "Semesters");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
