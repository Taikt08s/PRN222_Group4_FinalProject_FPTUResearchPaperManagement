using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class TopicInstructorId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Instructor_Id",
                table: "Topics",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 1,
                column: "Instructor_Id",
                value: null);

            migrationBuilder.UpdateData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 2,
                column: "Instructor_Id",
                value: null);

            migrationBuilder.UpdateData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 3,
                column: "Instructor_Id",
                value: null);

            migrationBuilder.UpdateData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 4,
                column: "Instructor_Id",
                value: null);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "Created_At",
                value: new DateTime(2025, 12, 16, 10, 13, 11, 519, DateTimeKind.Utc).AddTicks(5381));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "Created_At",
                value: new DateTime(2025, 12, 16, 10, 13, 11, 519, DateTimeKind.Utc).AddTicks(5384));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "Created_At",
                value: new DateTime(2025, 12, 16, 10, 13, 11, 519, DateTimeKind.Utc).AddTicks(5386));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                column: "Created_At",
                value: new DateTime(2025, 12, 16, 10, 13, 11, 519, DateTimeKind.Utc).AddTicks(5388));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                column: "Created_At",
                value: new DateTime(2025, 12, 16, 10, 13, 11, 519, DateTimeKind.Utc).AddTicks(5390));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666666"),
                column: "Created_At",
                value: new DateTime(2025, 12, 16, 10, 13, 11, 519, DateTimeKind.Utc).AddTicks(5392));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777777"),
                column: "Created_At",
                value: new DateTime(2025, 12, 16, 10, 13, 11, 519, DateTimeKind.Utc).AddTicks(5394));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-8888-8888-888888888888"),
                column: "Created_At",
                value: new DateTime(2025, 12, 16, 10, 13, 11, 519, DateTimeKind.Utc).AddTicks(5396));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-9999-9999-999999999999"),
                column: "Created_At",
                value: new DateTime(2025, 12, 16, 10, 13, 11, 519, DateTimeKind.Utc).AddTicks(5398));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaa1-aaaa-aaaa-aaaa-aaaaaaaaaaa1"),
                column: "Created_At",
                value: new DateTime(2025, 12, 16, 10, 13, 11, 519, DateTimeKind.Utc).AddTicks(5400));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaa2-aaaa-aaaa-aaaa-aaaaaaaaaaa2"),
                column: "Created_At",
                value: new DateTime(2025, 12, 16, 10, 13, 11, 519, DateTimeKind.Utc).AddTicks(5402));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbb1-bbbb-bbbb-bbbb-bbbbbbbbbbb1"),
                column: "Created_At",
                value: new DateTime(2025, 12, 16, 10, 13, 11, 519, DateTimeKind.Utc).AddTicks(5404));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbb2-bbbb-bbbb-bbbb-bbbbbbbbbbb2"),
                column: "Created_At",
                value: new DateTime(2025, 12, 16, 10, 13, 11, 519, DateTimeKind.Utc).AddTicks(5407));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbb3-bbbb-bbbb-bbbb-bbbbbbbbbbb3"),
                column: "Created_At",
                value: new DateTime(2025, 12, 16, 10, 13, 11, 519, DateTimeKind.Utc).AddTicks(5408));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbb4-bbbb-bbbb-bbbb-bbbbbbbbbbb4"),
                column: "Created_At",
                value: new DateTime(2025, 12, 16, 10, 13, 11, 519, DateTimeKind.Utc).AddTicks(5410));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Instructor_Id",
                table: "Topics");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "Created_At",
                value: new DateTime(2025, 12, 16, 8, 17, 35, 864, DateTimeKind.Utc).AddTicks(3830));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "Created_At",
                value: new DateTime(2025, 12, 16, 8, 17, 35, 864, DateTimeKind.Utc).AddTicks(3834));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "Created_At",
                value: new DateTime(2025, 12, 16, 8, 17, 35, 864, DateTimeKind.Utc).AddTicks(3837));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                column: "Created_At",
                value: new DateTime(2025, 12, 16, 8, 17, 35, 864, DateTimeKind.Utc).AddTicks(3839));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                column: "Created_At",
                value: new DateTime(2025, 12, 16, 8, 17, 35, 864, DateTimeKind.Utc).AddTicks(3841));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666666"),
                column: "Created_At",
                value: new DateTime(2025, 12, 16, 8, 17, 35, 864, DateTimeKind.Utc).AddTicks(3843));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777777"),
                column: "Created_At",
                value: new DateTime(2025, 12, 16, 8, 17, 35, 864, DateTimeKind.Utc).AddTicks(3845));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-8888-8888-888888888888"),
                column: "Created_At",
                value: new DateTime(2025, 12, 16, 8, 17, 35, 864, DateTimeKind.Utc).AddTicks(3847));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-9999-9999-999999999999"),
                column: "Created_At",
                value: new DateTime(2025, 12, 16, 8, 17, 35, 864, DateTimeKind.Utc).AddTicks(3849));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaa1-aaaa-aaaa-aaaa-aaaaaaaaaaa1"),
                column: "Created_At",
                value: new DateTime(2025, 12, 16, 8, 17, 35, 864, DateTimeKind.Utc).AddTicks(3851));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaa2-aaaa-aaaa-aaaa-aaaaaaaaaaa2"),
                column: "Created_At",
                value: new DateTime(2025, 12, 16, 8, 17, 35, 864, DateTimeKind.Utc).AddTicks(3853));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbb1-bbbb-bbbb-bbbb-bbbbbbbbbbb1"),
                column: "Created_At",
                value: new DateTime(2025, 12, 16, 8, 17, 35, 864, DateTimeKind.Utc).AddTicks(3856));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbb2-bbbb-bbbb-bbbb-bbbbbbbbbbb2"),
                column: "Created_At",
                value: new DateTime(2025, 12, 16, 8, 17, 35, 864, DateTimeKind.Utc).AddTicks(3858));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbb3-bbbb-bbbb-bbbb-bbbbbbbbbbb3"),
                column: "Created_At",
                value: new DateTime(2025, 12, 16, 8, 17, 35, 864, DateTimeKind.Utc).AddTicks(3860));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbb4-bbbb-bbbb-bbbb-bbbbbbbbbbb4"),
                column: "Created_At",
                value: new DateTime(2025, 12, 16, 8, 17, 35, 864, DateTimeKind.Utc).AddTicks(3862));
        }
    }
}
