using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GConge.web.api.Migrations
{
    public partial class RenameLeaveTypeColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LeaveTypeId",
                table: "LeaveRequests");

            migrationBuilder.AddColumn<string>(
                name: "LeaveType",
                table: "LeaveRequests",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "LastModifiedDate" },
                values: new object[] { new DateTime(2022, 9, 26, 9, 41, 17, 938, DateTimeKind.Local).AddTicks(4941), new DateTime(2022, 9, 26, 9, 41, 17, 938, DateTimeKind.Local).AddTicks(4947) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "LastModifiedDate", "Password", "PasswordSalt" },
                values: new object[] { new DateTime(2022, 9, 26, 9, 41, 17, 938, DateTimeKind.Local).AddTicks(4739), new DateTime(2022, 9, 26, 9, 41, 17, 938, DateTimeKind.Local).AddTicks(4803), new byte[] { 97, 82, 41, 175, 159, 92, 253, 137, 180, 188, 29, 159, 104, 74, 243, 202, 2, 112, 218, 74, 18, 155, 33, 118, 61, 101, 35, 136, 240, 90, 153, 92, 200, 35, 142, 33, 3, 100, 95, 163, 24, 85, 76, 1, 169, 4, 104, 163, 191, 233, 189, 178, 167, 232, 30, 40, 159, 117, 83, 67, 68, 124, 71, 150 }, new byte[] { 154, 171, 112, 77, 129, 15, 89, 110, 182, 198, 218, 195, 205, 183, 104, 151, 204, 4, 176, 180, 116, 95, 69, 118, 71, 253, 119, 0, 226, 164, 115, 243, 178, 206, 253, 138, 71, 35, 89, 77, 201, 207, 125, 90, 6, 253, 78, 57, 55, 249, 124, 87, 180, 177, 235, 126, 182, 87, 80, 26, 172, 236, 46, 46, 196, 125, 150, 93, 32, 24, 175, 217, 138, 169, 45, 193, 156, 54, 254, 196, 76, 147, 205, 229, 158, 47, 247, 188, 198, 43, 83, 248, 151, 71, 15, 213, 45, 189, 248, 247, 11, 12, 91, 30, 30, 3, 255, 241, 191, 90, 106, 249, 239, 45, 34, 102, 140, 228, 137, 2, 222, 190, 197, 195, 132, 67, 148, 253 } });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LeaveType",
                table: "LeaveRequests");

            migrationBuilder.AddColumn<int>(
                name: "LeaveTypeId",
                table: "LeaveRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "LastModifiedDate" },
                values: new object[] { new DateTime(2022, 9, 26, 9, 38, 13, 299, DateTimeKind.Local).AddTicks(6966), new DateTime(2022, 9, 26, 9, 38, 13, 299, DateTimeKind.Local).AddTicks(6972) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "LastModifiedDate", "Password", "PasswordSalt" },
                values: new object[] { new DateTime(2022, 9, 26, 9, 38, 13, 299, DateTimeKind.Local).AddTicks(6751), new DateTime(2022, 9, 26, 9, 38, 13, 299, DateTimeKind.Local).AddTicks(6813), new byte[] { 86, 31, 35, 213, 10, 204, 122, 85, 213, 113, 97, 128, 23, 97, 116, 51, 101, 155, 68, 54, 179, 81, 123, 40, 176, 132, 8, 234, 158, 91, 2, 7, 158, 124, 200, 59, 49, 35, 184, 181, 66, 110, 183, 2, 118, 2, 211, 80, 210, 206, 134, 76, 224, 124, 156, 162, 12, 17, 92, 229, 188, 137, 205, 33 }, new byte[] { 3, 95, 144, 57, 161, 217, 208, 80, 200, 177, 99, 83, 155, 239, 204, 201, 148, 88, 103, 23, 118, 83, 24, 35, 24, 124, 43, 101, 102, 252, 104, 52, 114, 158, 16, 7, 248, 199, 52, 207, 100, 163, 232, 81, 101, 71, 17, 76, 217, 59, 67, 114, 14, 163, 15, 73, 202, 17, 238, 49, 130, 134, 1, 94, 20, 62, 123, 28, 17, 73, 22, 226, 58, 194, 13, 230, 25, 213, 234, 141, 96, 107, 147, 239, 177, 106, 94, 82, 17, 149, 189, 247, 40, 27, 130, 146, 120, 204, 196, 202, 179, 176, 126, 94, 65, 211, 231, 205, 121, 132, 102, 78, 217, 50, 176, 124, 61, 13, 78, 146, 101, 11, 78, 232, 17, 61, 185, 115 } });
        }
    }
}
