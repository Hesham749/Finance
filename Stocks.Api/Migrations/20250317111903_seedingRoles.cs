using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Stocks.Api.Migrations
{
    /// <inheritdoc />
    public partial class seedingRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3a97653a-9b3b-42e1-b158-7922a30e3f08", "3a97653a-9b3b-42e1-b158-7922a30e3f08", "Admin", "ADMIN" },
                    { "97d784d1-d212-4c28-815f-e4b00c5f85e3", "97d784d1-d212-4c28-815f-e4b00c5f85e3", "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3a97653a-9b3b-42e1-b158-7922a30e3f08");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "97d784d1-d212-4c28-815f-e4b00c5f85e3");
        }
    }
}
