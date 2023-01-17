using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GloomhavenTracker.Database.Migrations
{
    public partial class hubclienttracking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsObserver",
                table: "HubCombatClient",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "CombatCharacterCombatHubClients",
                columns: table => new
                {
                    CharacterId = table.Column<Guid>(type: "uuid", nullable: false),
                    CombatHubClientId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CombatCharacterCombatHubClients", x => new { x.CharacterId, x.CombatHubClientId });
                    table.ForeignKey(
                        name: "FK_CombatCharacterCombatHubClients_CombatCharacters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "CombatCharacters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CombatCharacterCombatHubClients_HubCombatClient_CombatHubCl~",
                        column: x => x.CombatHubClientId,
                        principalTable: "HubCombatClient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CombatCharacterCombatHubClients_CharacterId",
                table: "CombatCharacterCombatHubClients",
                column: "CharacterId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CombatCharacterCombatHubClients_CombatHubClientId",
                table: "CombatCharacterCombatHubClients",
                column: "CombatHubClientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CombatCharacterCombatHubClients");

            migrationBuilder.DropColumn(
                name: "IsObserver",
                table: "HubCombatClient");
        }
    }
}
