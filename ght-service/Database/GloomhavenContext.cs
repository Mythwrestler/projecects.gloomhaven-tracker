using GloomhavenTracker.Database.Models;
using GloomhavenTracker.Database.Models.Campaign;
using GloomhavenTracker.Database.Models.Content;
using Microsoft.EntityFrameworkCore;

namespace GloomhavenTracker.Database;

public partial class GloomhavenContext : DbContext
{

    #region Content Db Sets
    public GloomhavenContext(DbContextOptions options) : base(options) { }
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
    public DbSet<CharacterPerkDAO> CharacterPerk => Set<CharacterPerkDAO>();
    public DbSet<CharacterItemDAO> CharacterItem => Set<CharacterItemDAO>();
    public DbSet<Models.Campaign.ScenarioDAO> ScenarioCampaign => Set<Models.Campaign.ScenarioDAO>();
    public DbSet<CampaignDAO> Campaign => Set<CampaignDAO>();
    #endregion

    #region Audit
        public DbSet<Audit> AuditLog => Set<Audit>();
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
        builder.DefineScenarioCampaignEntities();
        builder.DefineCampaignEntities();
        #endregion
    }

    public override int SaveChanges()
    {
        CaptureAuditBeforeSave();
        return base.SaveChanges();
    }

    private void CaptureAuditBeforeSave()
    {
        DateTime now = DateTime.UtcNow;
        var auditEntries = new List<AuditEntry>();

        foreach (var changedEntity in ChangeTracker.Entries())
        {
            if (changedEntity.Entity is AuditableEntityBase && changedEntity.State != EntityState.Detached && changedEntity.State != EntityState.Unchanged)
            {
                var auditEntry = new AuditEntry(changedEntity);
                auditEntry.TableName = changedEntity.Entity.GetType().Name;
                auditEntry.UserId = null;
                auditEntry.DateTimeUTC = now;
                auditEntries.Add(auditEntry);
                foreach (var property in changedEntity.Properties)
                {
                    string propertyName = property.Metadata.Name;
                    if (property.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValues[propertyName] = property.CurrentValue;
                        continue;
                    }
                    switch (changedEntity.State)
                    {
                        case EntityState.Added:
                            auditEntry.Action = AUDIT_ACTION.Create;
                            auditEntry.NewValues[propertyName] = property.CurrentValue;
                            break;
                        case EntityState.Deleted:
                            auditEntry.Action = AUDIT_ACTION.Delete;
                            auditEntry.OldValues[propertyName] = property.OriginalValue;
                            break;
                        case EntityState.Modified:
                            if (property.IsModified)
                            {
                                auditEntry.ChangedColumns.Add(propertyName);
                                auditEntry.Action = AUDIT_ACTION.Update;
                                auditEntry.OldValues[propertyName] = property.OriginalValue;
                                auditEntry.NewValues[propertyName] = property.CurrentValue;
                            }
                            break;
                    }
                }

                if (changedEntity.Entity is AuditableEntityBase entity)
                {
                    switch (changedEntity.State)
                    {
                        case EntityState.Added:
                            entity.CreatedOnUTC = now;
                            entity.UpdatedOnUTC = now;
                            break;

                        case EntityState.Modified:
                            Entry(entity).Property(x => x.CreatedOnUTC).IsModified = false;
                            entity.UpdatedOnUTC = now;
                            break;
                    }
                }
            }
        }
    }
}