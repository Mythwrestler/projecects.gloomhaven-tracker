using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GloomhavenTracker.Database.Migrations
{
    public partial class initialcombatmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CombatActiveEffects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EffectId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoundsRemaining = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedOnUTC = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedOnUTC = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CombatActiveEffects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CombatActiveEffects_ContentEffect_EffectId",
                        column: x => x.EffectId,
                        principalTable: "ContentEffect",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CombatAttackModifierDecks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CurrentPosition = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedOnUTC = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedOnUTC = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CombatAttackModifierDecks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CombatCombat",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CampaignId = table.Column<Guid>(type: "uuid", nullable: false),
                    ScenarioId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedOnUTC = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedOnUTC = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CombatCombat", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CombatCombat_CampaignCampaign_CampaignId",
                        column: x => x.CampaignId,
                        principalTable: "CampaignCampaign",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CombatCombat_ContentScenario_ScenarioId",
                        column: x => x.ScenarioId,
                        principalTable: "ContentScenario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CombatAttackModifierDeckCards",
                columns: table => new
                {
                    DeckId = table.Column<Guid>(type: "uuid", nullable: false),
                    AttackModifierId = table.Column<Guid>(type: "uuid", nullable: false),
                    position = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CombatAttackModifierDeckCards", x => new { x.DeckId, x.AttackModifierId });
                    table.ForeignKey(
                        name: "FK_CombatAttackModifierDeckCards_CombatAttackModifierDecks_Dec~",
                        column: x => x.DeckId,
                        principalTable: "CombatAttackModifierDecks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CombatAttackModifierDeckCards_ContentAttackModifier_AttackM~",
                        column: x => x.AttackModifierId,
                        principalTable: "ContentAttackModifier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CombatCharacters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CampaignCharacterId = table.Column<Guid>(type: "uuid", nullable: false),
                    Health = table.Column<int>(type: "integer", nullable: false),
                    CreatedOnUTC = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedOnUTC = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CombatId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CombatCharacters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CombatCharacters_CampaignCharacter_CampaignCharacterId",
                        column: x => x.CampaignCharacterId,
                        principalTable: "CampaignCharacter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CombatCharacters_CombatCombat_CombatId",
                        column: x => x.CombatId,
                        principalTable: "CombatCombat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CombatElements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CombatId = table.Column<Guid>(type: "uuid", nullable: false),
                    Element = table.Column<string>(type: "text", nullable: false),
                    Strength = table.Column<string>(type: "text", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedOnUTC = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedOnUTC = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CombatElements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CombatElements_CombatCombat_CombatId",
                        column: x => x.CombatId,
                        principalTable: "CombatCombat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CombatMonsters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MonsterNumber = table.Column<int>(type: "integer", nullable: false),
                    Health = table.Column<int>(type: "integer", nullable: false),
                    ContentMonsterId = table.Column<Guid>(type: "uuid", nullable: false),
                    MonsterContentId = table.Column<Guid>(type: "uuid", nullable: true),
                    InstanceId = table.Column<int>(type: "integer", nullable: false),
                    IsElite = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedOnUTC = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedOnUTC = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CombatId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CombatMonsters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CombatMonsters_CombatCombat_CombatId",
                        column: x => x.CombatId,
                        principalTable: "CombatCombat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CombatMonsters_ContentMonster_MonsterContentId",
                        column: x => x.MonsterContentId,
                        principalTable: "ContentMonster",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CombatObjectives",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ContentObjectiveId = table.Column<Guid>(type: "uuid", nullable: false),
                    ObjectiveId = table.Column<Guid>(type: "uuid", nullable: true),
                    Health = table.Column<int>(type: "integer", nullable: false),
                    CreatedOnUTC = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedOnUTC = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CombatId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CombatObjectives", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CombatObjectives_CombatCombat_CombatId",
                        column: x => x.CombatId,
                        principalTable: "CombatCombat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CombatObjectives_ContentObjective_ObjectiveId",
                        column: x => x.ObjectiveId,
                        principalTable: "ContentObjective",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CombatCharacterActiveEffects",
                columns: table => new
                {
                    CharacterId = table.Column<Guid>(type: "uuid", nullable: false),
                    ActiveEffectId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CombatCharacterActiveEffects", x => new { x.CharacterId, x.ActiveEffectId });
                    table.ForeignKey(
                        name: "FK_CombatCharacterActiveEffects_CombatActiveEffects_ActiveEffe~",
                        column: x => x.ActiveEffectId,
                        principalTable: "CombatActiveEffects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CombatCharacterActiveEffects_CombatCharacters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "CombatCharacters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CombatMonsterActiveEffects",
                columns: table => new
                {
                    MonsterId = table.Column<Guid>(type: "uuid", nullable: false),
                    ActiveEffectId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CombatMonsterActiveEffects", x => new { x.MonsterId, x.ActiveEffectId });
                    table.ForeignKey(
                        name: "FK_CombatMonsterActiveEffects_CombatActiveEffects_ActiveEffect~",
                        column: x => x.ActiveEffectId,
                        principalTable: "CombatActiveEffects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CombatMonsterActiveEffects_CombatMonsters_MonsterId",
                        column: x => x.MonsterId,
                        principalTable: "CombatMonsters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CombatObjectiveActiveEffects",
                columns: table => new
                {
                    ObjectiveId = table.Column<Guid>(type: "uuid", nullable: false),
                    ActiveEffectId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CombatObjectiveActiveEffects", x => new { x.ObjectiveId, x.ActiveEffectId });
                    table.ForeignKey(
                        name: "FK_CombatObjectiveActiveEffects_CombatActiveEffects_ActiveEffe~",
                        column: x => x.ActiveEffectId,
                        principalTable: "CombatActiveEffects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CombatObjectiveActiveEffects_CombatObjectives_ObjectiveId",
                        column: x => x.ObjectiveId,
                        principalTable: "CombatObjectives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CombatActiveEffects_EffectId",
                table: "CombatActiveEffects",
                column: "EffectId");

            migrationBuilder.CreateIndex(
                name: "IX_CombatAttackModifierDeckCards_AttackModifierId",
                table: "CombatAttackModifierDeckCards",
                column: "AttackModifierId");

            migrationBuilder.CreateIndex(
                name: "IX_CombatAttackModifierDeckCards_DeckId_position",
                table: "CombatAttackModifierDeckCards",
                columns: new[] { "DeckId", "position" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CombatCharacterActiveEffects_ActiveEffectId_CharacterId",
                table: "CombatCharacterActiveEffects",
                columns: new[] { "ActiveEffectId", "CharacterId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CombatCharacters_CampaignCharacterId",
                table: "CombatCharacters",
                column: "CampaignCharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_CombatCharacters_CombatId",
                table: "CombatCharacters",
                column: "CombatId");

            migrationBuilder.CreateIndex(
                name: "IX_CombatCombat_CampaignId",
                table: "CombatCombat",
                column: "CampaignId");

            migrationBuilder.CreateIndex(
                name: "IX_CombatCombat_ScenarioId",
                table: "CombatCombat",
                column: "ScenarioId");

            migrationBuilder.CreateIndex(
                name: "IX_CombatElements_CombatId_Element",
                table: "CombatElements",
                columns: new[] { "CombatId", "Element" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CombatMonsterActiveEffects_ActiveEffectId_MonsterId",
                table: "CombatMonsterActiveEffects",
                columns: new[] { "ActiveEffectId", "MonsterId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CombatMonsters_CombatId",
                table: "CombatMonsters",
                column: "CombatId");

            migrationBuilder.CreateIndex(
                name: "IX_CombatMonsters_MonsterContentId",
                table: "CombatMonsters",
                column: "MonsterContentId");

            migrationBuilder.CreateIndex(
                name: "IX_CombatObjectiveActiveEffects_ActiveEffectId_ObjectiveId",
                table: "CombatObjectiveActiveEffects",
                columns: new[] { "ActiveEffectId", "ObjectiveId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CombatObjectives_CombatId",
                table: "CombatObjectives",
                column: "CombatId");

            migrationBuilder.CreateIndex(
                name: "IX_CombatObjectives_ObjectiveId",
                table: "CombatObjectives",
                column: "ObjectiveId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CombatAttackModifierDeckCards");

            migrationBuilder.DropTable(
                name: "CombatCharacterActiveEffects");

            migrationBuilder.DropTable(
                name: "CombatElements");

            migrationBuilder.DropTable(
                name: "CombatMonsterActiveEffects");

            migrationBuilder.DropTable(
                name: "CombatObjectiveActiveEffects");

            migrationBuilder.DropTable(
                name: "CombatAttackModifierDecks");

            migrationBuilder.DropTable(
                name: "CombatCharacters");

            migrationBuilder.DropTable(
                name: "CombatMonsters");

            migrationBuilder.DropTable(
                name: "CombatActiveEffects");

            migrationBuilder.DropTable(
                name: "CombatObjectives");

            migrationBuilder.DropTable(
                name: "CombatCombat");
        }
    }
}
