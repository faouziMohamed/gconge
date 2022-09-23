using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GConge.web.api.Migrations
{
    public partial class UpdateTablesColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Approved",
                table: "LeaveRequests");

            migrationBuilder.DropColumn(
                name: "Cancelled",
                table: "LeaveRequests");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "Users",
                newName: "PhoneNumber");

            migrationBuilder.AlterColumn<byte[]>(
                name: "Password",
                table: "Users",
                type: "longblob",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordSalt",
                table: "Users",
                type: "longblob",
                nullable: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateUpdated",
                table: "LeaveRequests",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Employees",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "LastModifiedDate", "Password", "PasswordSalt" },
                values: new object[] { new DateTime(2022, 9, 23, 13, 11, 49, 204, DateTimeKind.Local).AddTicks(4789), new DateTime(2022, 9, 23, 13, 11, 49, 204, DateTimeKind.Local).AddTicks(4835), new byte[] { 219, 18, 134, 151, 151, 205, 177, 211, 216, 171, 69, 213, 11, 111, 111, 183, 154, 235, 209, 222, 192, 142, 239, 148, 78, 87, 69, 173, 2, 252, 110, 1, 132, 246, 153, 85, 82, 176, 204, 130, 228, 77, 60, 144, 42, 92, 136, 53, 146, 120, 166, 230, 1, 199, 199, 43, 234, 187, 149, 27, 218, 201, 165, 146 }, new byte[] { 8, 6, 110, 41, 240, 223, 250, 155, 152, 25, 131, 72, 89, 96, 126, 176, 23, 143, 89, 127, 40, 53, 60, 244, 11, 53, 45, 201, 1, 67, 103, 220, 149, 164, 100, 19, 219, 195, 206, 65, 232, 199, 173, 213, 199, 188, 175, 70, 36, 54, 215, 230, 220, 85, 10, 112, 219, 189, 105, 34, 107, 175, 205, 250, 29, 234, 197, 78, 167, 71, 140, 40, 20, 35, 41, 106, 1, 218, 162, 249, 58, 94, 94, 96, 6, 184, 205, 35, 122, 65, 238, 226, 38, 178, 214, 90, 218, 85, 54, 244, 67, 8, 112, 17, 115, 164, 136, 110, 217, 118, 236, 121, 176, 130, 120, 76, 56, 132, 46, 238, 76, 175, 63, 202, 91, 200, 22, 185 } });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_Email",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PasswordSalt",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DateUpdated",
                table: "LeaveRequests");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Employees");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Users",
                newName: "Phone");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "longblob")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<bool>(
                name: "Approved",
                table: "LeaveRequests",
                type: "tinyint(1)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Cancelled",
                table: "LeaveRequests",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "LastModifiedDate", "Password" },
                values: new object[] { new DateTime(2022, 9, 21, 15, 36, 30, 318, DateTimeKind.Local).AddTicks(2603), new DateTime(2022, 9, 21, 15, 36, 30, 318, DateTimeKind.Local).AddTicks(2670), "admin" });
        }
    }
}
