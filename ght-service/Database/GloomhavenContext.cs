using GloomhavenTracker.Database.Models.Campaign;
using GloomhavenTracker.Database.Models.Content;
using Microsoft.EntityFrameworkCore;

namespace GloomhavenTracker.Database;

public partial class GloomhavenContext : DbContext
{

    #region Content Db Sets
    public GloomhavenContext(DbContextOptions options): base(options) {}
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
    public DbSet<Models.Content.ScenarioDAO> ScenarioContent => Set<Models.Content.ScenarioDAO>();
    public DbSet<ScenarioMonsterDAO> ScenarioMonster => Set<ScenarioMonsterDAO>();
    public DbSet<ScenarioObjectiveDAO> ScenarioObjective => Set<ScenarioObjectiveDAO>();
    public DbSet<Models.Content.CharacterDAO> CharacterContent => Set<Models.Content.CharacterDAO>();
    public DbSet<CharacterBaseStatsDAO> CharacterBaseStats => Set<CharacterBaseStatsDAO>();
    public DbSet<PerkDAO> Perks => Set<PerkDAO>();
    public DbSet<ItemDAO> Items => Set<ItemDAO>();
    #endregion

    #region Campaign Db Sets
    public DbSet<Models.Campaign.CharacterDAO> CharacterCampaign => Set<Models.Campaign.CharacterDAO>();
    public DbSet<CharacterPerkDAO> AppliedPerk => Set<CharacterPerkDAO>();
    public DbSet<CharacterItemDAO> CharacterItem => Set<CharacterItemDAO>();
    #endregion


    protected override void OnModelCreating(ModelBuilder builder)
    {
        #region Content Entity Definitions
        builder.DefineGameEntities();
        builder.DefineEffectEntities();
        builder.DefineMonsterEntities();
        builder.DefineAttackModifierEntities();
        builder.DefineObjectiveEntities();
        builder.DefineScenarioContentEntities();
        builder.DefineCharacterContentEntities();
        builder.DefinePerkEntities();
        builder.DefineItemEntities();
        #endregion

        #region Campaign Entity Definitions
        builder.DefineCharacterCampaignEntities();
        #endregion
    }
}