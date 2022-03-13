using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class user_inactivated_by_me : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("ce43ad34-9067-4156-a4fc-f1d45d4565d2"));

            migrationBuilder.AddColumn<Guid>(
                name: "InactivatedByUserId",
                table: "User",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Claims", "DateOfBirth", "InactivatedAt", "InactivatedByUserId", "Password", "Role", "Username" },
                values: new object[] { new Guid("0e3d52a2-bb66-408c-a3e2-b842e278cef7"), null, new DateTime(1999, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "$2a$11$4mVz2KdmS2IPCcAH6gzAFurO8E3ccijT4t2S0.czAuwfW9IrRj9sm", "admin", "diego.dona" });

            migrationBuilder.CreateIndex(
                name: "IX_User_InactivatedByUserId",
                table: "User",
                column: "InactivatedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_User_InactivatedByUserId",
                table: "User",
                column: "InactivatedByUserId",
                principalTable: "User",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_User_InactivatedByUserId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_InactivatedByUserId",
                table: "User");

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("0e3d52a2-bb66-408c-a3e2-b842e278cef7"));

            migrationBuilder.DropColumn(
                name: "InactivatedByUserId",
                table: "User");

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Claims", "DateOfBirth", "InactivatedAt", "Password", "Role", "Username" },
                values: new object[] { new Guid("ce43ad34-9067-4156-a4fc-f1d45d4565d2"), null, new DateTime(1999, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "$2a$11$iUXjk5CeNVyjWt7iEux/4ObazXXoKkStevbnJsoFAe7gOe9VeeQfi", "admin", "diego.dona" });
        }
    }
}
