using GloomhavenTracker.Database.Models.Content;
using Microsoft.EntityFrameworkCore;

namespace GloomhavenTracker.Database;

public class ContentContext : DbContext
{
    public ContentContext(DbContextOptions options): base(options) {}
    public DbSet<GameDAO> Game => Set<GameDAO>();
    public DbSet<EffectDAO> Effect => Set<EffectDAO>();
    public DbSet<AttackModifierDAO> AttackModifier => Set<AttackModifierDAO>();
    public DbSet<AttackModifierEffectDAO> AttackModifierEffect => Set<AttackModifierEffectDAO>();
    public DbSet<GameBaseAttackModifierDAO> GameBaseAttackModifiers => Set<GameBaseAttackModifierDAO>();
    public DbSet<MonsterDAO> Monster => Set<MonsterDAO>();
    public DbSet<MonsterStatSetDAO> MonsterStatSet => Set<MonsterStatSetDAO>();
    public DbSet<MonsterDefenseEffectDAO> MonsterDefenseEffect => Set<MonsterDefenseEffectDAO>();
    public DbSet<MonsterDeathEffectDAO> MonsterDeathEffect => Set<MonsterDeathEffectDAO>();
    public DbSet<MonsterAttackEffectDAO> MonsterAttackEffect => Set<MonsterAttackEffectDAO>();
    public DbSet<MonsterBaseStatImmunityDAO> MonsterBaseStatImmunity => Set<MonsterBaseStatImmunityDAO>();
    public DbSet<ObjectiveDAO> Objective => Set<ObjectiveDAO>();
    public DbSet<ScenarioDAO> Scenario => Set<ScenarioDAO>();
    public DbSet<ScenarioMonsterDAO> ScenarioMonster => Set<ScenarioMonsterDAO>();
    public DbSet<ScenarioObjectiveDAO> ScenarioObjective => Set<ScenarioObjectiveDAO>();
    public DbSet<CharacterDAO> Character => Set<CharacterDAO>();
    public DbSet<CharacterBaseStatsDAO> CharacterBaseStats => Set<CharacterBaseStatsDAO>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.DefineGameEntities();
        builder.DefineEffectEntities();
        builder.DefineMonsterEntities();
        builder.DefineAttackModifierEntities();
        builder.DefineObjectiveEntities();
        builder.DefineScenarioEntities();
        builder.DefineCharacterEntities();
    }
}