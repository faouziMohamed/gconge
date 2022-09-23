using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GConge.web.api.Migrations
{
    public partial class UpdateUserAndEmployeeTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "Employees");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "LastModifiedDate", "Password", "PasswordSalt" },
                values: new object[] { new DateTime(2022, 9, 23, 13, 23, 2, 747, DateTimeKind.Local).AddTicks(6690), new DateTime(2022, 9, 23, 13, 23, 2, 747, DateTimeKind.Local).AddTicks(6749), new byte[] { 241, 172, 166, 44, 122, 112, 177, 2, 55, 9, 116, 203, 107, 155, 179, 8, 102, 96, 95, 117, 253, 59, 199, 128, 104, 14, 161, 16, 209, 231, 248, 222, 49, 45, 187, 237, 198, 213, 238, 116, 194, 100, 91, 211, 25, 163, 134, 5, 217, 209, 164, 167, 72, 145, 13, 104, 203, 93, 238, 122, 179, 238, 68, 39 }, new byte[] { 155, 138, 91, 44, 126, 242, 121, 240, 179, 250, 172, 158, 100, 233, 69, 192, 222, 236, 14, 125, 75, 173, 80, 136, 101, 24, 124, 248, 222, 143, 52, 219, 128, 79, 187, 231, 207, 216, 162, 224, 151, 115, 242, 246, 149, 143, 240, 117, 251, 179, 244, 104, 89, 240, 155, 44, 97, 236, 112, 0, 247, 4, 115, 198, 177, 12, 91, 99, 143, 62, 155, 103, 217, 140, 80, 166, 136, 227, 106, 215, 31, 163, 157, 123, 34, 123, 76, 50, 48, 160, 7, 134, 122, 106, 125, 231, 164, 216, 192, 179, 107, 246, 122, 199, 127, 173, 243, 202, 28, 52, 203, 254, 172, 131, 119, 188, 3, 49, 130, 176, 254, 11, 183, 47, 230, 80, 2, 14 } });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
