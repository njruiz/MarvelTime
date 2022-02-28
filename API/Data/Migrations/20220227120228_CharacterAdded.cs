using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace API.Data.Migrations
{
    public partial class CharacterAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Characters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DateOfBirth = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CharacterId = table.Column<string>(type: "text", nullable: true),
                    CharacterName = table.Column<string>(type: "text", nullable: true),
                    RealName = table.Column<string>(type: "text", nullable: true),
                    Gender = table.Column<string>(type: "text", nullable: true),
                    Role = table.Column<string>(type: "text", nullable: true),
                    SourceOfPower = table.Column<string>(type: "text", nullable: true),
                    McuStory = table.Column<string>(type: "text", nullable: true),
                    PlaceOfOrigin = table.Column<string>(type: "text", nullable: true),
                    PlayedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Photos_Character",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Url = table.Column<string>(type: "text", nullable: true),
                    IsMain = table.Column<bool>(type: "boolean", nullable: false),
                    PublicId = table.Column<string>(type: "text", nullable: true),
                    CharacterId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos_Character", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Photos_Character_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Photos_Character_CharacterId",
                table: "Photos_Character",
                column: "CharacterId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Photos_Character");

            migrationBuilder.DropTable(
                name: "Characters");
        }
    }
}
