using GloomhavenTracker.Database.Models.Content;
using Microsoft.EntityFrameworkCore;

namespace GloomhavenTracker.Database;

public class ContentContext : DbContext
{
    public ContentContext(DbContextOptions options): base(options) {}
    public DbSet<Game> Game => Set<Game>();
    public DbSet<Effect> Effect => Set<Effect>();
    public DbSet<AttackModifier> AttackModifier => Set<AttackModifier>();
    public DbSet<AttackModifierEffect> AttackModifierEffect => Set<AttackModifierEffect>();
    public DbSet<GameBaseAttackModifier> GameBaseAttackModifiers => Set<GameBaseAttackModifier>();
    public DbSet<Monster> Monster => Set<Monster>();
    public DbSet<MonsterStatSet> MonsterStatSet => Set<MonsterStatSet>();
    public DbSet<MonsterDefenseEffect> MonsterDefenseEffect => Set<MonsterDefenseEffect>();
    public DbSet<MonsterDeathEffect> MonsterDeathEffect => Set<MonsterDeathEffect>();
    public DbSet<MonsterAttackEffect> MonsterAttackEffect => Set<MonsterAttackEffect>();
    public DbSet<MonsterBaseStatImmunity> MonsterBaseStatImmunity => Set<MonsterBaseStatImmunity>();
    public DbSet<Objective> Objective => Set<Objective>();
    public DbSet<Scenario> Scenario => Set<Scenario>();
    public DbSet<ScenarioMonster> ScenarioMonster => Set<ScenarioMonster>();
    public DbSet<ScenarioObjective> ScenarioObjective => Set<ScenarioObjective>();
    public DbSet<Character> Character => Set<Character>();
    public DbSet<CharacterBaseStats> CharacterBaseStats => Set<CharacterBaseStats>();

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