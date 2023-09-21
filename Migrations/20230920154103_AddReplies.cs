using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Twilite.Migrations
{
    /// <inheritdoc />
    public partial class AddReplies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReplyInfo",
                columns: table => new
                {
                    ReplyId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ReplyUserName = table.Column<string>(type: "text", nullable: true),
                    ReplyContent = table.Column<string>(type: "text", nullable: true),
                    ReplyLikes = table.Column<string>(type: "text", nullable: true),
                    PostInfoModelPostId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReplyInfo", x => x.ReplyId);
                    table.ForeignKey(
                        name: "FK_ReplyInfo_Posts_PostInfoModelPostId",
                        column: x => x.PostInfoModelPostId,
                        principalTable: "Posts",
                        principalColumn: "PostId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReplyInfo_PostInfoModelPostId",
                table: "ReplyInfo",
                column: "PostInfoModelPostId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReplyInfo");
        }
    }
}
