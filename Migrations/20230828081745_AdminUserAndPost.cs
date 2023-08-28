using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Twilite.Migrations
{
    /// <inheritdoc />
    public partial class AdminUserAndPost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "PostId", "PostContent", "UserName" },
                values: new object[] { 1, "Welcome to twilite! This is a test Post. Enjoy your blogging experience :)", "Twilite-Admin" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Country", "Email", "Password", "UserName" },
                values: new object[] { 1, "SimulationLand", "admin@twilite.com", "admin@Twilite1234", "Twilite-Admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1);
        }
    }
}
