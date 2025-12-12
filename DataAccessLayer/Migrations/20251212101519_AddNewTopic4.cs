using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddNewTopic4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Topics",
                columns: new[] { "Id", "Created_By", "Deadline_Date", "Description", "Is_Group_Required", "Major", "Semester_Id", "Status", "SubmissionInstruction", "Title" },
                values: new object[] { 4, new Guid("aaaaaaa2-aaaa-aaaa-aaaa-aaaaaaaaaaa2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hệ thống quản lý hoạt động Phòng Khảo thí Đại học FPT HCM tích hợp chatbox AI", true, "Kỹ Thuật Phần Mềm", 1, "Created", "Students must upload their project files in PDF format to the correct assigned folder. Each submission will be reviewed by the project supervisor and members of the board committee. If the submission is rejected, the student must resubmit it before the deadline. Once the supervisor has approved the project or the secretary has confirmed it, or when the project deadline has passed, the student will no longer be able to modify or upload any files. The system also checks for plagiarism; if plagiarism is detected, the student will be suspended.\n\nSinh viên phải upload file đồ án dưới dạng PDF vào đúng thư mục được chỉ định. Mỗi bài nộp sẽ được giảng viên hướng dẫn và các thành viên hội đồng xét duyệt. Nếu bài nộp bị từ chối, sinh viên phải điều chỉnh và nộp lại trước hạn chót. Khi giảng viên đã phê duyệt, hoặc thư ký đã xác nhận, hoặc khi hết hạn nộp bài, sinh viên sẽ không thể chỉnh sửa hoặc upload bất kỳ file nào khác. Hệ thống cũng tự động kiểm tra đạo văn, nếu phát hiện đạo văn, sinh viên sẽ bị đình chỉ.", "FPTU HCM examination department operation management system integrates AI chatbox" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "Created_At",
                value: new DateTime(2025, 12, 12, 10, 15, 19, 168, DateTimeKind.Utc).AddTicks(8478));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "Created_At",
                value: new DateTime(2025, 12, 12, 10, 15, 19, 168, DateTimeKind.Utc).AddTicks(8483));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "Created_At",
                value: new DateTime(2025, 12, 12, 10, 15, 19, 168, DateTimeKind.Utc).AddTicks(8486));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                column: "Created_At",
                value: new DateTime(2025, 12, 12, 10, 15, 19, 168, DateTimeKind.Utc).AddTicks(8489));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                column: "Created_At",
                value: new DateTime(2025, 12, 12, 10, 15, 19, 168, DateTimeKind.Utc).AddTicks(8493));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666666"),
                column: "Created_At",
                value: new DateTime(2025, 12, 12, 10, 15, 19, 168, DateTimeKind.Utc).AddTicks(8495));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777777"),
                column: "Created_At",
                value: new DateTime(2025, 12, 12, 10, 15, 19, 168, DateTimeKind.Utc).AddTicks(8497));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-8888-8888-888888888888"),
                column: "Created_At",
                value: new DateTime(2025, 12, 12, 10, 15, 19, 168, DateTimeKind.Utc).AddTicks(8499));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-9999-9999-999999999999"),
                column: "Created_At",
                value: new DateTime(2025, 12, 12, 10, 15, 19, 168, DateTimeKind.Utc).AddTicks(8502));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaa1-aaaa-aaaa-aaaa-aaaaaaaaaaa1"),
                column: "Created_At",
                value: new DateTime(2025, 12, 12, 10, 15, 19, 168, DateTimeKind.Utc).AddTicks(8504));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaa2-aaaa-aaaa-aaaa-aaaaaaaaaaa2"),
                column: "Created_At",
                value: new DateTime(2025, 12, 12, 10, 15, 19, 168, DateTimeKind.Utc).AddTicks(8506));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbb1-bbbb-bbbb-bbbb-bbbbbbbbbbb1"),
                column: "Created_At",
                value: new DateTime(2025, 12, 12, 10, 15, 19, 168, DateTimeKind.Utc).AddTicks(8509));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbb2-bbbb-bbbb-bbbb-bbbbbbbbbbb2"),
                column: "Created_At",
                value: new DateTime(2025, 12, 12, 10, 15, 19, 168, DateTimeKind.Utc).AddTicks(8511));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbb3-bbbb-bbbb-bbbb-bbbbbbbbbbb3"),
                column: "Created_At",
                value: new DateTime(2025, 12, 12, 10, 15, 19, 168, DateTimeKind.Utc).AddTicks(8513));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbb4-bbbb-bbbb-bbbb-bbbbbbbbbbb4"),
                column: "Created_At",
                value: new DateTime(2025, 12, 12, 10, 15, 19, 168, DateTimeKind.Utc).AddTicks(8515));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "Created_At",
                value: new DateTime(2025, 12, 11, 10, 8, 19, 137, DateTimeKind.Utc).AddTicks(5217));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "Created_At",
                value: new DateTime(2025, 12, 11, 10, 8, 19, 137, DateTimeKind.Utc).AddTicks(5222));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "Created_At",
                value: new DateTime(2025, 12, 11, 10, 8, 19, 137, DateTimeKind.Utc).AddTicks(5224));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                column: "Created_At",
                value: new DateTime(2025, 12, 11, 10, 8, 19, 137, DateTimeKind.Utc).AddTicks(5227));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                column: "Created_At",
                value: new DateTime(2025, 12, 11, 10, 8, 19, 137, DateTimeKind.Utc).AddTicks(5229));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666666"),
                column: "Created_At",
                value: new DateTime(2025, 12, 11, 10, 8, 19, 137, DateTimeKind.Utc).AddTicks(5232));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777777"),
                column: "Created_At",
                value: new DateTime(2025, 12, 11, 10, 8, 19, 137, DateTimeKind.Utc).AddTicks(5234));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-8888-8888-888888888888"),
                column: "Created_At",
                value: new DateTime(2025, 12, 11, 10, 8, 19, 137, DateTimeKind.Utc).AddTicks(5236));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-9999-9999-999999999999"),
                column: "Created_At",
                value: new DateTime(2025, 12, 11, 10, 8, 19, 137, DateTimeKind.Utc).AddTicks(5238));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaa1-aaaa-aaaa-aaaa-aaaaaaaaaaa1"),
                column: "Created_At",
                value: new DateTime(2025, 12, 11, 10, 8, 19, 137, DateTimeKind.Utc).AddTicks(5240));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaa2-aaaa-aaaa-aaaa-aaaaaaaaaaa2"),
                column: "Created_At",
                value: new DateTime(2025, 12, 11, 10, 8, 19, 137, DateTimeKind.Utc).AddTicks(5243));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbb1-bbbb-bbbb-bbbb-bbbbbbbbbbb1"),
                column: "Created_At",
                value: new DateTime(2025, 12, 11, 10, 8, 19, 137, DateTimeKind.Utc).AddTicks(5247));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbb2-bbbb-bbbb-bbbb-bbbbbbbbbbb2"),
                column: "Created_At",
                value: new DateTime(2025, 12, 11, 10, 8, 19, 137, DateTimeKind.Utc).AddTicks(5249));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbb3-bbbb-bbbb-bbbb-bbbbbbbbbbb3"),
                column: "Created_At",
                value: new DateTime(2025, 12, 11, 10, 8, 19, 137, DateTimeKind.Utc).AddTicks(5251));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbb4-bbbb-bbbb-bbbb-bbbbbbbbbbb4"),
                column: "Created_At",
                value: new DateTime(2025, 12, 11, 10, 8, 19, 137, DateTimeKind.Utc).AddTicks(5254));
        }
    }
}
