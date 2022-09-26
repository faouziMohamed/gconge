using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GConge.web.api.Migrations
{
    public partial class AddLeaveTypeOnLeaveRequestTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LeaveTypeId",
                table: "LeaveRequests");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "LastModifiedDate" },
                values: new object[] { new DateTime(2022, 9, 23, 22, 22, 21, 184, DateTimeKind.Local).AddTicks(7511), new DateTime(2022, 9, 23, 22, 22, 21, 184, DateTimeKind.Local).AddTicks(7517) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "LastModifiedDate", "Password", "PasswordSalt" },
                values: new object[] { new DateTime(2022, 9, 23, 22, 22, 21, 184, DateTimeKind.Local).AddTicks(7294), new DateTime(2022, 9, 23, 22, 22, 21, 184, DateTimeKind.Local).AddTicks(7340), new byte[] { 0, 246, 190, 224, 41, 33, 204, 251, 104, 169, 250, 56, 220, 161, 201, 117, 62, 103, 246, 64, 63, 62, 80, 50, 65, 147, 215, 215, 225, 222, 100, 99, 118, 6, 169, 97, 46, 163, 162, 50, 68, 207, 159, 165, 81, 141, 47, 25, 21, 97, 87, 3, 103, 32, 211, 168, 225, 28, 217, 90, 133, 191, 250, 21 }, new byte[] { 109, 129, 206, 88, 196, 0, 217, 106, 65, 180, 158, 41, 246, 137, 199, 181, 180, 41, 48, 4, 70, 176, 181, 229, 205, 29, 53, 77, 18, 122, 66, 168, 28, 24, 67, 70, 131, 177, 164, 75, 89, 193, 43, 33, 147, 161, 68, 37, 120, 155, 185, 205, 200, 25, 8, 57, 237, 220, 8, 243, 215, 106, 244, 162, 61, 48, 221, 56, 95, 69, 102, 158, 9, 26, 160, 87, 77, 28, 97, 237, 162, 169, 78, 119, 176, 144, 124, 182, 182, 240, 116, 167, 68, 247, 236, 14, 247, 12, 66, 95, 163, 170, 71, 29, 83, 191, 151, 142, 211, 151, 90, 255, 175, 59, 21, 48, 196, 155, 167, 23, 126, 106, 191, 85, 80, 172, 55, 181 } });
        }
    }
}
