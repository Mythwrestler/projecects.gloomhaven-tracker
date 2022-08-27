using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GloomhavenTracker.Database.Migrations
{
    public partial class CombtHubClients : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CombatHubClientDAO",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CombatId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClientId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CombatHubClientDAO", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CombatHubClientDAO_CombatCombat_CombatId",
                        column: x => x.CombatId,
                        principalTable: "CombatCombat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CombatHubClientDAO_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CombatHubClientDAO_CombatId",
                table: "CombatHubClientDAO",
                column: "CombatId");

            migrationBuilder.CreateIndex(
                name: "IX_CombatHubClientDAO_UserId",
                table: "CombatHubClientDAO",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CombatHubClientDAO");
        }
    }
}
