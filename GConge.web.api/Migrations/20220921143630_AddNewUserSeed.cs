using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GConge.web.api.Migrations
{
    public partial class AddNewUserSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Users",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedBy", "DateCreated", "Email", "Firstname", "LastModifiedBy", "LastModifiedDate", "Lastname", "Password", "Phone", "Role" },
                values: new object[] { 1, "System", new DateTime(2022, 9, 21, 15, 36, 30, 318, DateTimeKind.Local).AddTicks(2603), "admin.email@email.com", "Admin", "System", new DateTime(2022, 9, 21, 15, 36, 30, 318, DateTimeKind.Local).AddTicks(2670), "Admin", "admin", "+212123456789", "Admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Users");
        }
    }
}
