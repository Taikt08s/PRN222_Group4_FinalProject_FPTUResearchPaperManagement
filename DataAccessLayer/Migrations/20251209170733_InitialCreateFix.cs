using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateFix : Migration
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
