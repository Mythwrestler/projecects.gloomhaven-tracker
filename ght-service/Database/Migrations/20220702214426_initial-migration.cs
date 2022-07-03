using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GloomhavenTracker.Database.Migrations
{
    public partial class initialmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuditLog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: true),
                    Action = table.Column<int>(type: "integer", nullable: false),
                    TableName = table.Column<string>(type: "text", nullable: false),
                    PrimaryKey = table.Column<string>(type: "text", nullable: true),
                    AffectedColumns = table.Column<string>(type: "text", nullable: true),
                    OldValues = table.Column<string>(type: "text", nullable: true),
                    NewValues = table.Column<string>(type: "text", nullable: true),
                    DateTimeUTC = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContentEffect",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<int>(type: "integer", nullable: true),
                    Duration = table.Column<int>(type: "integer", nullable: true),
                    Range = table.Column<int>(type: "integer", nullable: true),
                    Element = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentEffect", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContentGame",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ContentCode = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentGame", x => x.Id);
                    table.UniqueConstraint("AK_ContentGame_ContentCode", x => x.ContentCode);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserName = table.Column<string>(type: "text", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "CampaignCampaign",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    GameId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedOnUTC = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedOnUTC = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CampaignCampaign", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CampaignCampaign_ContentGame_GameId",
                        column: x => x.GameId,
                        principalTable: "ContentGame",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContentAttackModifier",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ContentCode = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    IsCurse = table.Column<bool>(type: "boolean", nullable: false),
                    IsBlessing = table.Column<bool>(type: "boolean", nullable: false),
                    TriggerShuffle = table.Column<bool>(type: "boolean", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false),
                    GameId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentAttackModifier", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContentAttackModifier_ContentGame_GameId",
                        column: x => x.GameId,
                        principalTable: "ContentGame",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContentCharacter",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ContentCode = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    GameId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentCharacter", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContentCharacter_ContentGame_GameId",
                        column: x => x.GameId,
                        principalTable: "ContentGame",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContentItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ContentCode = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Number = table.Column<int>(type: "integer", nullable: false),
                    Location = table.Column<string>(type: "text", nullable: false),
                    Frequency = table.Column<string>(type: "text", nullable: false),
                    GameId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContentItem_ContentGame_GameId",
                        column: x => x.GameId,
                        principalTable: "ContentGame",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContentMonster",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ContentCode = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    GameId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentMonster", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContentMonster_ContentGame_GameId",
                        column: x => x.GameId,
                        principalTable: "ContentGame",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContentObjective",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ContentCode = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Health = table.Column<string>(type: "text", nullable: false),
                    RangeAttackable = table.Column<bool>(type: "boolean", nullable: false),
                    MeleeAttackable = table.Column<bool>(type: "boolean", nullable: false),
                    GameId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentObjective", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContentObjective_ContentGame_GameId",
                        column: x => x.GameId,
                        principalTable: "ContentGame",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContentScenario",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ContentCode = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    ScenarioNumber = table.Column<int>(type: "integer", nullable: false),
                    Goal = table.Column<string>(type: "text", nullable: false),
                    CityMapLocation = table.Column<string>(type: "text", nullable: false),
                    ScenarioBookPages = table.Column<List<int>>(type: "integer[]", nullable: false),
                    SupplementalBookPages = table.Column<List<int>>(type: "integer[]", nullable: false),
                    GameId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentScenario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContentScenario_ContentGame_GameId",
                        column: x => x.GameId,
                        principalTable: "ContentGame",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserCampaign",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CampaignId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCampaign", x => new { x.UserId, x.CampaignId });
                    table.ForeignKey(
                        name: "FK_UserCampaign_CampaignCampaign_CampaignId",
                        column: x => x.CampaignId,
                        principalTable: "CampaignCampaign",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserCampaign_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContentAttackModifierEffect",
                columns: table => new
                {
                    EffectId = table.Column<Guid>(type: "uuid", nullable: false),
                    AttackModifierId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentAttackModifierEffect", x => new { x.EffectId, x.AttackModifierId });
                    table.ForeignKey(
                        name: "FK_ContentAttackModifierEffect_ContentAttackModifier_AttackMod~",
                        column: x => x.AttackModifierId,
                        principalTable: "ContentAttackModifier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContentAttackModifierEffect_ContentEffect_EffectId",
                        column: x => x.EffectId,
                        principalTable: "ContentEffect",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContentGameBaseAttackModifiers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    GameId = table.Column<Guid>(type: "uuid", nullable: false),
                    AttackModifierId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentGameBaseAttackModifiers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContentGameBaseAttackModifiers_ContentAttackModifier_Attack~",
                        column: x => x.AttackModifierId,
                        principalTable: "ContentAttackModifier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContentGameBaseAttackModifiers_ContentGame_GameId",
                        column: x => x.GameId,
                        principalTable: "ContentGame",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CampaignCharacter",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Experience = table.Column<int>(type: "integer", nullable: false),
                    Gold = table.Column<int>(type: "integer", nullable: false),
                    PerkPoints = table.Column<int>(type: "integer", nullable: false),
                    CharacterContentId = table.Column<Guid>(type: "uuid", nullable: false),
                    CampaignId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedOnUTC = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedOnUTC = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CampaignCharacter", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CampaignCharacter_CampaignCampaign_CampaignId",
                        column: x => x.CampaignId,
                        principalTable: "CampaignCampaign",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CampaignCharacter_ContentCharacter_CharacterContentId",
                        column: x => x.CharacterContentId,
                        principalTable: "ContentCharacter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContentCharacterBaseStat",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Level = table.Column<int>(type: "integer", nullable: false),
                    Experience = table.Column<int>(type: "integer", nullable: false),
                    Health = table.Column<int>(type: "integer", nullable: false),
                    CharacterId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentCharacterBaseStat", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContentCharacterBaseStat_ContentCharacter_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "ContentCharacter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContentPerk",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ContentCode = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    GameId = table.Column<Guid>(type: "uuid", nullable: false),
                    CharacterId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentPerk", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContentPerk_ContentCharacter_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "ContentCharacter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContentPerk_ContentGame_GameId",
                        column: x => x.GameId,
                        principalTable: "ContentGame",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CampaignCampaignItem",
                columns: table => new
                {
                    ItemId = table.Column<Guid>(type: "uuid", nullable: false),
                    CampaignId = table.Column<Guid>(type: "uuid", nullable: false),
                    InUse = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedOnUTC = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedOnUTC = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CampaignCampaignItem", x => new { x.CampaignId, x.ItemId });
                    table.ForeignKey(
                        name: "FK_CampaignCampaignItem_CampaignCampaign_CampaignId",
                        column: x => x.CampaignId,
                        principalTable: "CampaignCampaign",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CampaignCampaignItem_ContentItem_ItemId",
                        column: x => x.ItemId,
                        principalTable: "ContentItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContentMonsterStatSet",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Level = table.Column<int>(type: "integer", nullable: false),
                    Health = table.Column<string>(type: "text", nullable: false),
                    Movement = table.Column<string>(type: "text", nullable: false),
                    Attack = table.Column<string>(type: "text", nullable: false),
                    RangeAttackable = table.Column<bool>(type: "boolean", nullable: false),
                    MeleeAttackable = table.Column<bool>(type: "boolean", nullable: false),
                    IsElite = table.Column<bool>(type: "boolean", nullable: false),
                    MonsterId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentMonsterStatSet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContentMonsterStatSet_ContentMonster_MonsterId",
                        column: x => x.MonsterId,
                        principalTable: "ContentMonster",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CampaignScenario",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IsClosed = table.Column<bool>(type: "boolean", nullable: false),
                    IsCompleted = table.Column<bool>(type: "boolean", nullable: false),
                    ScenarioContentId = table.Column<Guid>(type: "uuid", nullable: false),
                    CampaignId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedOnUTC = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedOnUTC = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CampaignScenario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CampaignScenario_CampaignCampaign_CampaignId",
                        column: x => x.CampaignId,
                        principalTable: "CampaignCampaign",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CampaignScenario_ContentScenario_ScenarioContentId",
                        column: x => x.ScenarioContentId,
                        principalTable: "ContentScenario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContentScenarioMonster",
                columns: table => new
                {
                    ScenarioId = table.Column<Guid>(type: "uuid", nullable: false),
                    MonsterId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentScenarioMonster", x => new { x.ScenarioId, x.MonsterId });
                    table.ForeignKey(
                        name: "FK_ContentScenarioMonster_ContentMonster_MonsterId",
                        column: x => x.MonsterId,
                        principalTable: "ContentMonster",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContentScenarioMonster_ContentScenario_ScenarioId",
                        column: x => x.ScenarioId,
                        principalTable: "ContentScenario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContentScenarioObjective",
                columns: table => new
                {
                    ScenarioId = table.Column<Guid>(type: "uuid", nullable: false),
                    ObjectiveId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentScenarioObjective", x => new { x.ScenarioId, x.ObjectiveId });
                    table.ForeignKey(
                        name: "FK_ContentScenarioObjective_ContentObjective_ObjectiveId",
                        column: x => x.ObjectiveId,
                        principalTable: "ContentObjective",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContentScenarioObjective_ContentScenario_ScenarioId",
                        column: x => x.ScenarioId,
                        principalTable: "ContentScenario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CampaignCharacterItem",
                columns: table => new
                {
                    ItemId = table.Column<Guid>(type: "uuid", nullable: false),
                    CharacterId = table.Column<Guid>(type: "uuid", nullable: false),
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

            migrationBuilder.CreateTable(
                name: "CampaignCharacterAppliedPerk",
                columns: table => new
                {
                    PerkId = table.Column<Guid>(type: "uuid", nullable: false),
                    CharacterId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedOnUTC = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedOnUTC = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CampaignCharacterAppliedPerk", x => new { x.CharacterId, x.PerkId });
                    table.ForeignKey(
                        name: "FK_CampaignCharacterAppliedPerk_CampaignCharacter_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "CampaignCharacter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CampaignCharacterAppliedPerk_ContentPerk_PerkId",
                        column: x => x.PerkId,
                        principalTable: "ContentPerk",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContentPerkAction",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AttackModifierId = table.Column<Guid>(type: "uuid", nullable: false),
                    Action = table.Column<string>(type: "text", nullable: false),
                    Count = table.Column<int>(type: "integer", nullable: false),
                    PerkId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentPerkAction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContentPerkAction_ContentAttackModifier_AttackModifierId",
                        column: x => x.AttackModifierId,
                        principalTable: "ContentAttackModifier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContentPerkAction_ContentPerk_PerkId",
                        column: x => x.PerkId,
                        principalTable: "ContentPerk",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContentMonsterAttackEffect",
                columns: table => new
                {
                    EffectId = table.Column<Guid>(type: "uuid", nullable: false),
                    MonsterStatSetId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentMonsterAttackEffect", x => new { x.EffectId, x.MonsterStatSetId });
                    table.ForeignKey(
                        name: "FK_ContentMonsterAttackEffect_ContentEffect_EffectId",
                        column: x => x.EffectId,
                        principalTable: "ContentEffect",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContentMonsterAttackEffect_ContentMonsterStatSet_MonsterSta~",
                        column: x => x.MonsterStatSetId,
                        principalTable: "ContentMonsterStatSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContentMonsterBaseStatImmunity",
                columns: table => new
                {
                    MonsterStatSetId = table.Column<Guid>(type: "uuid", nullable: false),
                    Effect = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentMonsterBaseStatImmunity", x => new { x.MonsterStatSetId, x.Effect });
                    table.ForeignKey(
                        name: "FK_ContentMonsterBaseStatImmunity_ContentMonsterStatSet_Monste~",
                        column: x => x.MonsterStatSetId,
                        principalTable: "ContentMonsterStatSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContentMonsterDeathEffect",
                columns: table => new
                {
                    EffectId = table.Column<Guid>(type: "uuid", nullable: false),
                    MonsterStatSetId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentMonsterDeathEffect", x => new { x.EffectId, x.MonsterStatSetId });
                    table.ForeignKey(
                        name: "FK_ContentMonsterDeathEffect_ContentEffect_EffectId",
                        column: x => x.EffectId,
                        principalTable: "ContentEffect",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContentMonsterDeathEffect_ContentMonsterStatSet_MonsterStat~",
                        column: x => x.MonsterStatSetId,
                        principalTable: "ContentMonsterStatSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContentMonsterDefenseEffect",
                columns: table => new
                {
                    EffectId = table.Column<Guid>(type: "uuid", nullable: false),
                    MonsterStatSetId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentMonsterDefenseEffect", x => new { x.EffectId, x.MonsterStatSetId });
                    table.ForeignKey(
                        name: "FK_ContentMonsterDefenseEffect_ContentEffect_EffectId",
                        column: x => x.EffectId,
                        principalTable: "ContentEffect",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContentMonsterDefenseEffect_ContentMonsterStatSet_MonsterSt~",
                        column: x => x.MonsterStatSetId,
                        principalTable: "ContentMonsterStatSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CampaignCampaign_GameId",
                table: "CampaignCampaign",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_CampaignCampaignItem_ItemId",
                table: "CampaignCampaignItem",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_CampaignCharacter_CampaignId",
                table: "CampaignCharacter",
                column: "CampaignId");

            migrationBuilder.CreateIndex(
                name: "IX_CampaignCharacter_CharacterContentId",
                table: "CampaignCharacter",
                column: "CharacterContentId");

            migrationBuilder.CreateIndex(
                name: "IX_CampaignCharacterAppliedPerk_PerkId",
                table: "CampaignCharacterAppliedPerk",
                column: "PerkId");

            migrationBuilder.CreateIndex(
                name: "IX_CampaignCharacterItem_ItemId",
                table: "CampaignCharacterItem",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_CampaignScenario_CampaignId",
                table: "CampaignScenario",
                column: "CampaignId");

            migrationBuilder.CreateIndex(
                name: "IX_CampaignScenario_ScenarioContentId",
                table: "CampaignScenario",
                column: "ScenarioContentId");

            migrationBuilder.CreateIndex(
                name: "IX_ContentAttackModifier_GameId_ContentCode",
                table: "ContentAttackModifier",
                columns: new[] { "GameId", "ContentCode" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContentAttackModifierEffect_AttackModifierId",
                table: "ContentAttackModifierEffect",
                column: "AttackModifierId");

            migrationBuilder.CreateIndex(
                name: "IX_ContentCharacter_GameId_ContentCode",
                table: "ContentCharacter",
                columns: new[] { "GameId", "ContentCode" });

            migrationBuilder.CreateIndex(
                name: "IX_ContentCharacterBaseStat_CharacterId_Level",
                table: "ContentCharacterBaseStat",
                columns: new[] { "CharacterId", "Level" });

            migrationBuilder.CreateIndex(
                name: "IX_ContentEffect_Type_Value_Duration",
                table: "ContentEffect",
                columns: new[] { "Type", "Value", "Duration" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContentGameBaseAttackModifiers_AttackModifierId",
                table: "ContentGameBaseAttackModifiers",
                column: "AttackModifierId");

            migrationBuilder.CreateIndex(
                name: "IX_ContentGameBaseAttackModifiers_GameId",
                table: "ContentGameBaseAttackModifiers",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_ContentItem_GameId",
                table: "ContentItem",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_ContentMonster_GameId_ContentCode",
                table: "ContentMonster",
                columns: new[] { "GameId", "ContentCode" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContentMonsterAttackEffect_MonsterStatSetId",
                table: "ContentMonsterAttackEffect",
                column: "MonsterStatSetId");

            migrationBuilder.CreateIndex(
                name: "IX_ContentMonsterDeathEffect_MonsterStatSetId",
                table: "ContentMonsterDeathEffect",
                column: "MonsterStatSetId");

            migrationBuilder.CreateIndex(
                name: "IX_ContentMonsterDefenseEffect_MonsterStatSetId",
                table: "ContentMonsterDefenseEffect",
                column: "MonsterStatSetId");

            migrationBuilder.CreateIndex(
                name: "IX_ContentMonsterStatSet_MonsterId",
                table: "ContentMonsterStatSet",
                column: "MonsterId");

            migrationBuilder.CreateIndex(
                name: "IX_ContentObjective_GameId_ContentCode",
                table: "ContentObjective",
                columns: new[] { "GameId", "ContentCode" });

            migrationBuilder.CreateIndex(
                name: "IX_ContentPerk_CharacterId",
                table: "ContentPerk",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_ContentPerk_GameId",
                table: "ContentPerk",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_ContentPerkAction_AttackModifierId",
                table: "ContentPerkAction",
                column: "AttackModifierId");

            migrationBuilder.CreateIndex(
                name: "IX_ContentPerkAction_PerkId",
                table: "ContentPerkAction",
                column: "PerkId");

            migrationBuilder.CreateIndex(
                name: "IX_ContentScenario_GameId_ContentCode",
                table: "ContentScenario",
                columns: new[] { "GameId", "ContentCode" });

            migrationBuilder.CreateIndex(
                name: "IX_ContentScenario_GameId_ScenarioNumber",
                table: "ContentScenario",
                columns: new[] { "GameId", "ScenarioNumber" });

            migrationBuilder.CreateIndex(
                name: "IX_ContentScenarioMonster_MonsterId",
                table: "ContentScenarioMonster",
                column: "MonsterId");

            migrationBuilder.CreateIndex(
                name: "IX_ContentScenarioObjective_ObjectiveId",
                table: "ContentScenarioObjective",
                column: "ObjectiveId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCampaign_CampaignId",
                table: "UserCampaign",
                column: "CampaignId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditLog");

            migrationBuilder.DropTable(
                name: "CampaignCampaignItem");

            migrationBuilder.DropTable(
                name: "CampaignCharacterAppliedPerk");

            migrationBuilder.DropTable(
                name: "CampaignCharacterItem");

            migrationBuilder.DropTable(
                name: "CampaignScenario");

            migrationBuilder.DropTable(
                name: "ContentAttackModifierEffect");

            migrationBuilder.DropTable(
                name: "ContentCharacterBaseStat");

            migrationBuilder.DropTable(
                name: "ContentGameBaseAttackModifiers");

            migrationBuilder.DropTable(
                name: "ContentMonsterAttackEffect");

            migrationBuilder.DropTable(
                name: "ContentMonsterBaseStatImmunity");

            migrationBuilder.DropTable(
                name: "ContentMonsterDeathEffect");

            migrationBuilder.DropTable(
                name: "ContentMonsterDefenseEffect");

            migrationBuilder.DropTable(
                name: "ContentPerkAction");

            migrationBuilder.DropTable(
                name: "ContentScenarioMonster");

            migrationBuilder.DropTable(
                name: "ContentScenarioObjective");

            migrationBuilder.DropTable(
                name: "UserCampaign");

            migrationBuilder.DropTable(
                name: "CampaignCharacter");

            migrationBuilder.DropTable(
                name: "ContentItem");

            migrationBuilder.DropTable(
                name: "ContentEffect");

            migrationBuilder.DropTable(
                name: "ContentMonsterStatSet");

            migrationBuilder.DropTable(
                name: "ContentAttackModifier");

            migrationBuilder.DropTable(
                name: "ContentPerk");

            migrationBuilder.DropTable(
                name: "ContentObjective");

            migrationBuilder.DropTable(
                name: "ContentScenario");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "CampaignCampaign");

            migrationBuilder.DropTable(
                name: "ContentMonster");

            migrationBuilder.DropTable(
                name: "ContentCharacter");

            migrationBuilder.DropTable(
                name: "ContentGame");
        }
    }
}
