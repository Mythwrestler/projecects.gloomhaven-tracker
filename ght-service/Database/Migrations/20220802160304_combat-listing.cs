using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GloomhavenTracker.Database.Migrations
{
    public partial class combatlisting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentPosition",
                table: "CombatAttackModifierDecks");

            migrationBuilder.AddColumn<Guid>(
                name: "MonsterModifierDeckId",
                table: "CombatCombat",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "ScenarioLevel",
                table: "CombatCombat",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<List<int>>(
                name: "Positions",
                table: "CombatAttackModifierDecks",
                type: "integer[]",
                nullable: false);

            migrationBuilder.CreateIndex(
                name: "IX_CombatCombat_MonsterModifierDeckId",
                table: "CombatCombat",
                column: "MonsterModifierDeckId");

            migrationBuilder.AddForeignKey(
                name: "FK_CombatCombat_CombatAttackModifierDecks_MonsterModifierDeckId",
                table: "CombatCombat",
                column: "MonsterModifierDeckId",
                principalTable: "CombatAttackModifierDecks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CombatCombat_CombatAttackModifierDecks_MonsterModifierDeckId",
                table: "CombatCombat");

            migrationBuilder.DropIndex(
                name: "IX_CombatCombat_MonsterModifierDeckId",
                table: "CombatCombat");

            migrationBuilder.DropColumn(
                name: "MonsterModifierDeckId",
                table: "CombatCombat");

            migrationBuilder.DropColumn(
                name: "ScenarioLevel",
                table: "CombatCombat");

            migrationBuilder.DropColumn(
                name: "Positions",
                table: "CombatAttackModifierDecks");

            migrationBuilder.AddColumn<int>(
                name: "CurrentPosition",
                table: "CombatAttackModifierDecks",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
