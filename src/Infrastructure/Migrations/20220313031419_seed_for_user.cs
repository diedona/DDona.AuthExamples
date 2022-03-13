using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class seed_for_user : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Claims", "DateOfBirth", "Password", "Role", "Username" },
                values: new object[] { new Guid("8947de4d-069b-42fa-95a2-4b86462a816b"), null, new DateTime(1999, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "$2a$11$pJganvm9VxDwyafMZ/yvPOrsUOmdEVF/7OcPydvBuWBn9c9v89Ig.", "admin", "diego.dona" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("8947de4d-069b-42fa-95a2-4b86462a816b"));
        }
    }
}
