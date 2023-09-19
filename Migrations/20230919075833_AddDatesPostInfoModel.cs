using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Twilite.Migrations
{
    /// <inheritdoc />
    public partial class AddDatesPostInfoModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PostContent",
                table: "Posts",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(450)",
                oldMaxLength: 450);

            migrationBuilder.AddColumn<string>(
                name: "PostEditedDate",
                table: "Posts",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PostedDate",
                table: "Posts",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PostEditedDate",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "PostedDate",
                table: "Posts");

            migrationBuilder.AlterColumn<string>(
                name: "PostContent",
                table: "Posts",
                type: "character varying(450)",
                maxLength: 450,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
