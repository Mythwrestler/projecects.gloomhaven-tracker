using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GloomhavenTracker.Database.Migrations
{
    public partial class restructurecampaignitemrelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CampaignCharacterItem");

            migrationBuilder.DropColumn(
                name: "InUse",
                table: "CampaignCampaignItem");

            migrationBuilder.AddColumn<Guid>(
                name: "CharacterId",
                table: "CampaignCampaignItem",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CampaignCampaignItem_CharacterId",
                table: "CampaignCampaignItem",
                column: "CharacterId");

            migrationBuilder.AddForeignKey(
                name: "FK_CampaignCampaignItem_CampaignCharacter_CharacterId",
                table: "CampaignCampaignItem",
                column: "CharacterId",
                principalTable: "CampaignCharacter",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CampaignCampaignItem_CampaignCharacter_CharacterId",
                table: "CampaignCampaignItem");

            migrationBuilder.DropIndex(
                name: "IX_CampaignCampaignItem_CharacterId",
                table: "CampaignCampaignItem");

            migrationBuilder.DropColumn(
                name: "CharacterId",
                table: "CampaignCampaignItem");

            migrationBuilder.AddColumn<bool>(
                name: "InUse",
                table: "CampaignCampaignItem",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "CampaignCharacterItem",
                columns: table => new
                {
                    CharacterId = table.Column<Guid>(type: "uuid", nullable: false),
                    ItemId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedOnUTC = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedOnUTC = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CampaignCharacterItem", x => new { x.CharacterId, x.ItemId });
                    table.ForeignKey(
                        name: "FK_CampaignCharacterItem_CampaignCharacter_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "CampaignCharacter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CampaignCharacterItem_ContentItem_ItemId",
                        column: x => x.ItemId,
                        principalTable: "ContentItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CampaignCharacterItem_ItemId",
                table: "CampaignCharacterItem",
                column: "ItemId");
        }
    }
}
