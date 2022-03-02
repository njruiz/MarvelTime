using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    public partial class CharacterLikeAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CharacterLikes",
                columns: table => new
                {
                    SourceUserId = table.Column<int>(type: "integer", nullable: false),
                    LikedCharacterId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterLikes", x => new { x.SourceUserId, x.LikedCharacterId });
                    table.ForeignKey(
                        name: "FK_CharacterLikes_AspNetUsers_SourceUserId",
                        column: x => x.SourceUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacterLikes_Characters_LikedCharacterId",
                        column: x => x.LikedCharacterId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CharacterLikes_LikedCharacterId",
                table: "CharacterLikes",
                column: "LikedCharacterId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CharacterLikes");
        }
    }
}
