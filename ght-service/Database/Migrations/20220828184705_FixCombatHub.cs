using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GloomhavenTracker.Database.Migrations
{
    public partial class FixCombatHub : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CombatHubClientDAO_CombatCombat_CombatId",
                table: "CombatHubClientDAO");

            migrationBuilder.DropForeignKey(
                name: "FK_CombatHubClientDAO_User_UserId",
                table: "CombatHubClientDAO");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CombatHubClientDAO",
                table: "CombatHubClientDAO");

            migrationBuilder.RenameTable(
                name: "CombatHubClientDAO",
                newName: "HubCombatClient");

            migrationBuilder.RenameIndex(
                name: "IX_CombatHubClientDAO_UserId",
                table: "HubCombatClient",
                newName: "IX_HubCombatClient_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_CombatHubClientDAO_CombatId",
                table: "HubCombatClient",
                newName: "IX_HubCombatClient_CombatId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HubCombatClient",
                table: "HubCombatClient",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HubCombatClient_CombatCombat_CombatId",
                table: "HubCombatClient",
                column: "CombatId",
                principalTable: "CombatCombat",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_HubCombatClient_User_UserId",
                table: "HubCombatClient",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HubCombatClient_CombatCombat_CombatId",
                table: "HubCombatClient");

            migrationBuilder.DropForeignKey(
                name: "FK_HubCombatClient_User_UserId",
                table: "HubCombatClient");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HubCombatClient",
                table: "HubCombatClient");

            migrationBuilder.RenameTable(
                name: "HubCombatClient",
                newName: "CombatHubClientDAO");

            migrationBuilder.RenameIndex(
                name: "IX_HubCombatClient_UserId",
                table: "CombatHubClientDAO",
                newName: "IX_CombatHubClientDAO_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_HubCombatClient_CombatId",
                table: "CombatHubClientDAO",
                newName: "IX_CombatHubClientDAO_CombatId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CombatHubClientDAO",
                table: "CombatHubClientDAO",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CombatHubClientDAO_CombatCombat_CombatId",
                table: "CombatHubClientDAO",
                column: "CombatId",
                principalTable: "CombatCombat",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CombatHubClientDAO_User_UserId",
                table: "CombatHubClientDAO",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
