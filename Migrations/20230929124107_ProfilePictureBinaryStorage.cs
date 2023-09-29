using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Twilite.Migrations
{
    /// <inheritdoc />
    public partial class ProfilePictureBinaryStorage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserAvatarLocation",
                table: "UserProfiles");

            migrationBuilder.AddColumn<byte[]>(
                name: "ProfilePictureBytes",
                table: "UserProfiles",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfilePictureBytes",
                table: "UserProfiles");

            migrationBuilder.AddColumn<string>(
                name: "UserAvatarLocation",
                table: "UserProfiles",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
