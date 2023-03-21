using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GloomhavenTracker.Database.Migrations
{
    public partial class combatcharacterlevel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CombatMonsters_ContentMonster_MonsterContentId",
                table: "CombatMonsters");

            migrationBuilder.DropForeignKey(
                name: "FK_CombatObjectives_ContentObjective_ObjectiveId",
                table: "CombatObjectives");

            migrationBuilder.AlterColumn<Guid>(
                name: "ObjectiveId",
                table: "CombatObjectives",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "MonsterContentId",
                table: "CombatMonsters",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "CombatCharacters",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_CombatMonsters_ContentMonster_MonsterContentId",
                table: "CombatMonsters",
                column: "MonsterContentId",
                principalTable: "ContentMonster",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CombatObjectives_ContentObjective_ObjectiveId",
                table: "CombatObjectives",
                column: "ObjectiveId",
                principalTable: "ContentObjective",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CombatMonsters_ContentMonster_MonsterContentId",
                table: "CombatMonsters");

            migrationBuilder.DropForeignKey(
                name: "FK_CombatObjectives_ContentObjective_ObjectiveId",
                table: "CombatObjectives");

            migrationBuilder.DropColumn(
                name: "Level",
                table: "CombatCharacters");

            migrationBuilder.AlterColumn<Guid>(
                name: "ObjectiveId",
                table: "CombatObjectives",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "MonsterContentId",
                table: "CombatMonsters",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_CombatMonsters_ContentMonster_MonsterContentId",
                table: "CombatMonsters",
                column: "MonsterContentId",
                principalTable: "ContentMonster",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CombatObjectives_ContentObjective_ObjectiveId",
                table: "CombatObjectives",
                column: "ObjectiveId",
                principalTable: "ContentObjective",
                principalColumn: "Id");
        }
    }
}
