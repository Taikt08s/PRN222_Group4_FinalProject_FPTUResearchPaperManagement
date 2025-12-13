using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSeedDataInstructionSubmission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 1,
                column: "SubmissionInstruction",
                value: "• The maximum upload size for a single file is 2GB (Kích thước tải lên tối đa cho một file duy nhất là 2GB).\r\n• Students, please appoint a representative to submit the project (Sinh viên vui lòng đề ra một người đại diện nộp đồ án).\r\n• File names cannot contain special characters and spaces (Tên file tải lên không thể chứa ký tự đặc biệt và khoảng trắng).\r\n  ○ Examples (Ví dụ):\r\n    ▪ Valid name (Tên file hợp lệ): \"Present_Order.pdf\"\r\n    ▪ Invalid name (Tên file không hợp lệ): \"Present order.pdf\" (Không chấp nhận khoảng trắng).\r\n    ▪ Invalid name (Tên file không hợp lệ): \"Trình tự hiện tại.pdf\" (Không chấp nhận tiếng Việt và khoảng trắng).\r\n• File names cannot contain the group code (Tên file không bao gồm mã nhóm. Nên là \"Present_Order\" thay vì \"GRA497_G1_Present_Order\").\r\n• When the project supervisor has approved or the project deadline has expired, the student cannot modify it anymore, such as uploading files, submitting, etc. (Khi người giám sát đồ án đã phê duyệt hoặc thời hạn làm đồ án đã kết thúc, hoặc khi hạn chót của đồ án đã hết, sinh viên sẽ không thể chỉnh sửa hoặc thực hiện bất kỳ hoạt động nào khác như upload file hoặc submission, v.v.).\r\nFolder Structures (Cấu trúc thư mục)\r\n• Student must upload the thesis file to the 'Final Thesis + Reports' directory (Tải file báo cáo đã hoàn thiện lên thư mục 'Final Thesis + Reports').\r\n• Student must upload presentation files to the 'Slides' directory (Sinh viên tải các file dùng cho việc thuyết trình như PowerPoint lên thư mục 'Slides').\r\n• Student must upload (database, code, etc.) files to the 'Others' directory (Sinh viên tải lên các file chứa mã nguồn, dữ liệu, minh chứng, hình ảnh…, lên thư mục 'Others').\r\n• Reports and Final Thesis Directories accept files with 'doc', 'docx', 'pdf', 'xls', 'xlsx', 'jpg', 'jpeg', 'png', 'gif', 'pptx', 'txt', 'odt', 'ods', 'ppt' extensions. Please move the 'Others' Directories to upload other types of document: Code, Database, video, audio, etc. (Các thư mục Final Thesis chấp nhận các file phần mở rộng như 'doc', 'docx', 'pdf', 'xls', 'xlsx', 'jpg', 'jpeg', 'png', 'gif', 'pptx', 'txt', 'odt', 'ods', 'ppt'. Vui lòng di chuyển 'Others' để tải lên các loại tài liệu khác: Mã nguồn, CSDL, video, âm thanh…).\r\n");

            migrationBuilder.UpdateData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 2,
                column: "SubmissionInstruction",
                value: "• The maximum upload size for a single file is 2GB (Kích thước tải lên tối đa cho một file duy nhất là 2GB).\r\n• Students, please appoint a representative to submit the project (Sinh viên vui lòng đề ra một người đại diện nộp đồ án).\r\n• File names cannot contain special characters and spaces (Tên file tải lên không thể chứa ký tự đặc biệt và khoảng trắng).\r\n  ○ Examples (Ví dụ):\r\n    ▪ Valid name (Tên file hợp lệ): \"Present_Order.pdf\"\r\n    ▪ Invalid name (Tên file không hợp lệ): \"Present order.pdf\" (Không chấp nhận khoảng trắng).\r\n    ▪ Invalid name (Tên file không hợp lệ): \"Trình tự hiện tại.pdf\" (Không chấp nhận tiếng Việt và khoảng trắng).\r\n• File names cannot contain the group code (Tên file không bao gồm mã nhóm. Nên là \"Present_Order\" thay vì \"GRA497_G1_Present_Order\").\r\n• When the project supervisor has approved or the project deadline has expired, the student cannot modify it anymore, such as uploading files, submitting, etc. (Khi người giám sát đồ án đã phê duyệt hoặc thời hạn làm đồ án đã kết thúc, hoặc khi hạn chót của đồ án đã hết, sinh viên sẽ không thể chỉnh sửa hoặc thực hiện bất kỳ hoạt động nào khác như upload file hoặc submission, v.v.).\r\nFolder Structures (Cấu trúc thư mục)\r\n• Student must upload the thesis file to the 'Final Thesis + Reports' directory (Tải file báo cáo đã hoàn thiện lên thư mục 'Final Thesis + Reports').\r\n• Student must upload presentation files to the 'Slides' directory (Sinh viên tải các file dùng cho việc thuyết trình như PowerPoint lên thư mục 'Slides').\r\n• Student must upload (database, code, etc.) files to the 'Others' directory (Sinh viên tải lên các file chứa mã nguồn, dữ liệu, minh chứng, hình ảnh…, lên thư mục 'Others').\r\n• Reports and Final Thesis Directories accept files with 'doc', 'docx', 'pdf', 'xls', 'xlsx', 'jpg', 'jpeg', 'png', 'gif', 'pptx', 'txt', 'odt', 'ods', 'ppt' extensions. Please move the 'Others' Directories to upload other types of document: Code, Database, video, audio, etc. (Các thư mục Final Thesis chấp nhận các file phần mở rộng như 'doc', 'docx', 'pdf', 'xls', 'xlsx', 'jpg', 'jpeg', 'png', 'gif', 'pptx', 'txt', 'odt', 'ods', 'ppt'. Vui lòng di chuyển 'Others' để tải lên các loại tài liệu khác: Mã nguồn, CSDL, video, âm thanh…).\r\n");

            migrationBuilder.UpdateData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 3,
                column: "SubmissionInstruction",
                value: "• The maximum upload size for a single file is 2GB (Kích thước tải lên tối đa cho một file duy nhất là 2GB).\r\n• Students, please appoint a representative to submit the project (Sinh viên vui lòng đề ra một người đại diện nộp đồ án).\r\n• File names cannot contain special characters and spaces (Tên file tải lên không thể chứa ký tự đặc biệt và khoảng trắng).\r\n  ○ Examples (Ví dụ):\r\n    ▪ Valid name (Tên file hợp lệ): \"Present_Order.pdf\"\r\n    ▪ Invalid name (Tên file không hợp lệ): \"Present order.pdf\" (Không chấp nhận khoảng trắng).\r\n    ▪ Invalid name (Tên file không hợp lệ): \"Trình tự hiện tại.pdf\" (Không chấp nhận tiếng Việt và khoảng trắng).\r\n• File names cannot contain the group code (Tên file không bao gồm mã nhóm. Nên là \"Present_Order\" thay vì \"GRA497_G1_Present_Order\").\r\n• When the project supervisor has approved or the project deadline has expired, the student cannot modify it anymore, such as uploading files, submitting, etc. (Khi người giám sát đồ án đã phê duyệt hoặc thời hạn làm đồ án đã kết thúc, hoặc khi hạn chót của đồ án đã hết, sinh viên sẽ không thể chỉnh sửa hoặc thực hiện bất kỳ hoạt động nào khác như upload file hoặc submission, v.v.).\r\nFolder Structures (Cấu trúc thư mục)\r\n• Student must upload the thesis file to the 'Final Thesis + Reports' directory (Tải file báo cáo đã hoàn thiện lên thư mục 'Final Thesis + Reports').\r\n• Student must upload presentation files to the 'Slides' directory (Sinh viên tải các file dùng cho việc thuyết trình như PowerPoint lên thư mục 'Slides').\r\n• Student must upload (database, code, etc.) files to the 'Others' directory (Sinh viên tải lên các file chứa mã nguồn, dữ liệu, minh chứng, hình ảnh…, lên thư mục 'Others').\r\n• Reports and Final Thesis Directories accept files with 'doc', 'docx', 'pdf', 'xls', 'xlsx', 'jpg', 'jpeg', 'png', 'gif', 'pptx', 'txt', 'odt', 'ods', 'ppt' extensions. Please move the 'Others' Directories to upload other types of document: Code, Database, video, audio, etc. (Các thư mục Final Thesis chấp nhận các file phần mở rộng như 'doc', 'docx', 'pdf', 'xls', 'xlsx', 'jpg', 'jpeg', 'png', 'gif', 'pptx', 'txt', 'odt', 'ods', 'ppt'. Vui lòng di chuyển 'Others' để tải lên các loại tài liệu khác: Mã nguồn, CSDL, video, âm thanh…).\r\n");

            migrationBuilder.UpdateData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 4,
                column: "SubmissionInstruction",
                value: "• The maximum upload size for a single file is 2GB (Kích thước tải lên tối đa cho một file duy nhất là 2GB).\r\n• Students, please appoint a representative to submit the project (Sinh viên vui lòng đề ra một người đại diện nộp đồ án).\r\n• File names cannot contain special characters and spaces (Tên file tải lên không thể chứa ký tự đặc biệt và khoảng trắng).\r\n  ○ Examples (Ví dụ):\r\n    ▪ Valid name (Tên file hợp lệ): \"Present_Order.pdf\"\r\n    ▪ Invalid name (Tên file không hợp lệ): \"Present order.pdf\" (Không chấp nhận khoảng trắng).\r\n    ▪ Invalid name (Tên file không hợp lệ): \"Trình tự hiện tại.pdf\" (Không chấp nhận tiếng Việt và khoảng trắng).\r\n• File names cannot contain the group code (Tên file không bao gồm mã nhóm. Nên là \"Present_Order\" thay vì \"GRA497_G1_Present_Order\").\r\n• When the project supervisor has approved or the project deadline has expired, the student cannot modify it anymore, such as uploading files, submitting, etc. (Khi người giám sát đồ án đã phê duyệt hoặc thời hạn làm đồ án đã kết thúc, hoặc khi hạn chót của đồ án đã hết, sinh viên sẽ không thể chỉnh sửa hoặc thực hiện bất kỳ hoạt động nào khác như upload file hoặc submission, v.v.).\r\nFolder Structures (Cấu trúc thư mục)\r\n• Student must upload the thesis file to the 'Final Thesis + Reports' directory (Tải file báo cáo đã hoàn thiện lên thư mục 'Final Thesis + Reports').\r\n• Student must upload presentation files to the 'Slides' directory (Sinh viên tải các file dùng cho việc thuyết trình như PowerPoint lên thư mục 'Slides').\r\n• Student must upload (database, code, etc.) files to the 'Others' directory (Sinh viên tải lên các file chứa mã nguồn, dữ liệu, minh chứng, hình ảnh…, lên thư mục 'Others').\r\n• Reports and Final Thesis Directories accept files with 'doc', 'docx', 'pdf', 'xls', 'xlsx', 'jpg', 'jpeg', 'png', 'gif', 'pptx', 'txt', 'odt', 'ods', 'ppt' extensions. Please move the 'Others' Directories to upload other types of document: Code, Database, video, audio, etc. (Các thư mục Final Thesis chấp nhận các file phần mở rộng như 'doc', 'docx', 'pdf', 'xls', 'xlsx', 'jpg', 'jpeg', 'png', 'gif', 'pptx', 'txt', 'odt', 'ods', 'ppt'. Vui lòng di chuyển 'Others' để tải lên các loại tài liệu khác: Mã nguồn, CSDL, video, âm thanh…).\r\n");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "Created_At",
                value: new DateTime(2025, 12, 12, 16, 32, 3, 512, DateTimeKind.Utc).AddTicks(2780));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "Created_At",
                value: new DateTime(2025, 12, 12, 16, 32, 3, 512, DateTimeKind.Utc).AddTicks(2784));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "Created_At",
                value: new DateTime(2025, 12, 12, 16, 32, 3, 512, DateTimeKind.Utc).AddTicks(2786));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                column: "Created_At",
                value: new DateTime(2025, 12, 12, 16, 32, 3, 512, DateTimeKind.Utc).AddTicks(2790));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                column: "Created_At",
                value: new DateTime(2025, 12, 12, 16, 32, 3, 512, DateTimeKind.Utc).AddTicks(2793));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666666"),
                column: "Created_At",
                value: new DateTime(2025, 12, 12, 16, 32, 3, 512, DateTimeKind.Utc).AddTicks(2795));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777777"),
                column: "Created_At",
                value: new DateTime(2025, 12, 12, 16, 32, 3, 512, DateTimeKind.Utc).AddTicks(2798));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-8888-8888-888888888888"),
                column: "Created_At",
                value: new DateTime(2025, 12, 12, 16, 32, 3, 512, DateTimeKind.Utc).AddTicks(2800));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-9999-9999-999999999999"),
                column: "Created_At",
                value: new DateTime(2025, 12, 12, 16, 32, 3, 512, DateTimeKind.Utc).AddTicks(2802));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaa1-aaaa-aaaa-aaaa-aaaaaaaaaaa1"),
                column: "Created_At",
                value: new DateTime(2025, 12, 12, 16, 32, 3, 512, DateTimeKind.Utc).AddTicks(2831));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaa2-aaaa-aaaa-aaaa-aaaaaaaaaaa2"),
                column: "Created_At",
                value: new DateTime(2025, 12, 12, 16, 32, 3, 512, DateTimeKind.Utc).AddTicks(2834));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbb1-bbbb-bbbb-bbbb-bbbbbbbbbbb1"),
                column: "Created_At",
                value: new DateTime(2025, 12, 12, 16, 32, 3, 512, DateTimeKind.Utc).AddTicks(2836));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbb2-bbbb-bbbb-bbbb-bbbbbbbbbbb2"),
                column: "Created_At",
                value: new DateTime(2025, 12, 12, 16, 32, 3, 512, DateTimeKind.Utc).AddTicks(2838));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbb3-bbbb-bbbb-bbbb-bbbbbbbbbbb3"),
                column: "Created_At",
                value: new DateTime(2025, 12, 12, 16, 32, 3, 512, DateTimeKind.Utc).AddTicks(2840));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbb4-bbbb-bbbb-bbbb-bbbbbbbbbbb4"),
                column: "Created_At",
                value: new DateTime(2025, 12, 12, 16, 32, 3, 512, DateTimeKind.Utc).AddTicks(2843));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 1,
                column: "SubmissionInstruction",
                value: "Students must upload their project files in PDF format to the correct assigned folder. Each submission will be reviewed by the project supervisor and members of the board committee. If the submission is rejected, the student must resubmit it before the deadline. Once the supervisor has approved the project or the secretary has confirmed it, or when the project deadline has passed, the student will no longer be able to modify or upload any files. The system also checks for plagiarism; if plagiarism is detected, the student will be suspended.\n\nSinh viên phải upload file đồ án dưới dạng PDF vào đúng thư mục được chỉ định. Mỗi bài nộp sẽ được giảng viên hướng dẫn và các thành viên hội đồng xét duyệt. Nếu bài nộp bị từ chối, sinh viên phải điều chỉnh và nộp lại trước hạn chót. Khi giảng viên đã phê duyệt, hoặc thư ký đã xác nhận, hoặc khi hết hạn nộp bài, sinh viên sẽ không thể chỉnh sửa hoặc upload bất kỳ file nào khác. Hệ thống cũng tự động kiểm tra đạo văn, nếu phát hiện đạo văn, sinh viên sẽ bị đình chỉ.");

            migrationBuilder.UpdateData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 2,
                column: "SubmissionInstruction",
                value: "Students must upload their project files in PDF format to the correct assigned folder. Each submission will be reviewed by the project supervisor and members of the board committee. If the submission is rejected, the student must resubmit it before the deadline. Once the supervisor has approved the project or the secretary has confirmed it, or when the project deadline has passed, the student will no longer be able to modify or upload any files. The system also checks for plagiarism; if plagiarism is detected, the student will be suspended.\n\nSinh viên phải upload file đồ án dưới dạng PDF vào đúng thư mục được chỉ định. Mỗi bài nộp sẽ được giảng viên hướng dẫn và các thành viên hội đồng xét duyệt. Nếu bài nộp bị từ chối, sinh viên phải điều chỉnh và nộp lại trước hạn chót. Khi giảng viên đã phê duyệt, hoặc thư ký đã xác nhận, hoặc khi hết hạn nộp bài, sinh viên sẽ không thể chỉnh sửa hoặc upload bất kỳ file nào khác. Hệ thống cũng tự động kiểm tra đạo văn, nếu phát hiện đạo văn, sinh viên sẽ bị đình chỉ.");

            migrationBuilder.UpdateData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 3,
                column: "SubmissionInstruction",
                value: "Students must upload their project files in PDF format to the correct assigned folder. Each submission will be reviewed by the project supervisor and members of the board committee. If the submission is rejected, the student must resubmit it before the deadline. Once the supervisor has approved the project or the secretary has confirmed it, or when the project deadline has passed, the student will no longer be able to modify or upload any files. The system also checks for plagiarism; if plagiarism is detected, the student will be suspended.\n\nSinh viên phải upload file đồ án dưới dạng PDF vào đúng thư mục được chỉ định. Mỗi bài nộp sẽ được giảng viên hướng dẫn và các thành viên hội đồng xét duyệt. Nếu bài nộp bị từ chối, sinh viên phải điều chỉnh và nộp lại trước hạn chót. Khi giảng viên đã phê duyệt, hoặc thư ký đã xác nhận, hoặc khi hết hạn nộp bài, sinh viên sẽ không thể chỉnh sửa hoặc upload bất kỳ file nào khác. Hệ thống cũng tự động kiểm tra đạo văn, nếu phát hiện đạo văn, sinh viên sẽ bị đình chỉ.");

            migrationBuilder.UpdateData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 4,
                column: "SubmissionInstruction",
                value: "Students must upload their project files in PDF format to the correct assigned folder. Each submission will be reviewed by the project supervisor and members of the board committee. If the submission is rejected, the student must resubmit it before the deadline. Once the supervisor has approved the project or the secretary has confirmed it, or when the project deadline has passed, the student will no longer be able to modify or upload any files. The system also checks for plagiarism; if plagiarism is detected, the student will be suspended.\n\nSinh viên phải upload file đồ án dưới dạng PDF vào đúng thư mục được chỉ định. Mỗi bài nộp sẽ được giảng viên hướng dẫn và các thành viên hội đồng xét duyệt. Nếu bài nộp bị từ chối, sinh viên phải điều chỉnh và nộp lại trước hạn chót. Khi giảng viên đã phê duyệt, hoặc thư ký đã xác nhận, hoặc khi hết hạn nộp bài, sinh viên sẽ không thể chỉnh sửa hoặc upload bất kỳ file nào khác. Hệ thống cũng tự động kiểm tra đạo văn, nếu phát hiện đạo văn, sinh viên sẽ bị đình chỉ.");

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
    }
}
