using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSeedDataDeadlineTopic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 1,
                column: "Deadline_Date",
                value: new DateTime(2025, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 2,
                column: "Deadline_Date",
                value: new DateTime(2025, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 3,
                column: "Deadline_Date",
                value: new DateTime(2025, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 4,
                column: "Deadline_Date",
                value: new DateTime(2025, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "Created_At",
                value: new DateTime(2025, 12, 13, 7, 31, 57, 254, DateTimeKind.Utc).AddTicks(4061));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "Created_At",
                value: new DateTime(2025, 12, 13, 7, 31, 57, 254, DateTimeKind.Utc).AddTicks(4065));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "Created_At",
                value: new DateTime(2025, 12, 13, 7, 31, 57, 254, DateTimeKind.Utc).AddTicks(4067));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                column: "Created_At",
                value: new DateTime(2025, 12, 13, 7, 31, 57, 254, DateTimeKind.Utc).AddTicks(4070));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                column: "Created_At",
                value: new DateTime(2025, 12, 13, 7, 31, 57, 254, DateTimeKind.Utc).AddTicks(4072));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666666"),
                column: "Created_At",
                value: new DateTime(2025, 12, 13, 7, 31, 57, 254, DateTimeKind.Utc).AddTicks(4074));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777777"),
                column: "Created_At",
                value: new DateTime(2025, 12, 13, 7, 31, 57, 254, DateTimeKind.Utc).AddTicks(4076));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-8888-8888-888888888888"),
                column: "Created_At",
                value: new DateTime(2025, 12, 13, 7, 31, 57, 254, DateTimeKind.Utc).AddTicks(4078));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-9999-9999-999999999999"),
                column: "Created_At",
                value: new DateTime(2025, 12, 13, 7, 31, 57, 254, DateTimeKind.Utc).AddTicks(4080));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaa1-aaaa-aaaa-aaaa-aaaaaaaaaaa1"),
                column: "Created_At",
                value: new DateTime(2025, 12, 13, 7, 31, 57, 254, DateTimeKind.Utc).AddTicks(4082));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaa2-aaaa-aaaa-aaaa-aaaaaaaaaaa2"),
                column: "Created_At",
                value: new DateTime(2025, 12, 13, 7, 31, 57, 254, DateTimeKind.Utc).AddTicks(4084));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbb1-bbbb-bbbb-bbbb-bbbbbbbbbbb1"),
                column: "Created_At",
                value: new DateTime(2025, 12, 13, 7, 31, 57, 254, DateTimeKind.Utc).AddTicks(4086));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbb2-bbbb-bbbb-bbbb-bbbbbbbbbbb2"),
                column: "Created_At",
                value: new DateTime(2025, 12, 13, 7, 31, 57, 254, DateTimeKind.Utc).AddTicks(4088));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbb3-bbbb-bbbb-bbbb-bbbbbbbbbbb3"),
                column: "Created_At",
                value: new DateTime(2025, 12, 13, 7, 31, 57, 254, DateTimeKind.Utc).AddTicks(4090));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbb4-bbbb-bbbb-bbbb-bbbbbbbbbbb4"),
                column: "Created_At",
                value: new DateTime(2025, 12, 13, 7, 31, 57, 254, DateTimeKind.Utc).AddTicks(4092));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 1,
                column: "Deadline_Date",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 2,
                column: "Deadline_Date",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 3,
                column: "Deadline_Date",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 4,
                column: "Deadline_Date",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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
    }
}
