using GloomhavenTracker.Database.Models;
using GloomhavenTracker.Database.Models.Campaign;
using GloomhavenTracker.Database.Models.Content;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace GloomhavenTracker.Database;

public partial class GloomhavenContext : DbContext
{

    #region Content Db Sets
    public GloomhavenContext(DbContextOptions options) : base(options) { }
    public DbSet<GameDAO> ContentGame => Set<GameDAO>();
    public DbSet<EffectDAO> ContentEffect => Set<EffectDAO>();
    public DbSet<AttackModifierDAO> ContentAttackModifier => Set<AttackModifierDAO>();
    public DbSet<AttackModifierEffectDAO> ContentAttackModifierEffect => Set<AttackModifierEffectDAO>();
    public DbSet<GameBaseAttackModifierDAO> ContentGameBaseAttackModifiers => Set<GameBaseAttackModifierDAO>();
    public DbSet<MonsterDAO> ContentMonster => Set<MonsterDAO>();
    public DbSet<MonsterStatSetDAO> ContentMonsterStatSet => Set<MonsterStatSetDAO>();
    public DbSet<MonsterDefenseEffectDAO> ContentMonsterDefenseEffect => Set<MonsterDefenseEffectDAO>();
    public DbSet<MonsterDeathEffectDAO> ContentMonsterDeathEffect => Set<MonsterDeathEffectDAO>();
    public DbSet<MonsterAttackEffectDAO> ContentMonsterAttackEffect => Set<MonsterAttackEffectDAO>();
    public DbSet<MonsterBaseStatImmunityDAO> ContentMonsterBaseStatImmunity => Set<MonsterBaseStatImmunityDAO>();
    public DbSet<ObjectiveDAO> ContentObjective => Set<ObjectiveDAO>();
    public DbSet<Models.Content.ScenarioDAO> ContentScenario => Set<Models.Content.ScenarioDAO>();
    public DbSet<ScenarioMonsterDAO> ContentScenarioMonster => Set<ScenarioMonsterDAO>();
    public DbSet<ScenarioObjectiveDAO> ContentScenarioObjective => Set<ScenarioObjectiveDAO>();
    public DbSet<Models.Content.CharacterDAO> ContentCharacter => Set<Models.Content.CharacterDAO>();
    public DbSet<CharacterBaseStatsDAO> ContentCharacterBaseStat => Set<CharacterBaseStatsDAO>();
    public DbSet<PerkDAO> ContentPerk => Set<PerkDAO>();
    public DbSet<PerkActionDAO> ContentPerkAction => Set<PerkActionDAO>();
    public DbSet<ItemDAO> ContentItem => Set<ItemDAO>();
    #endregion

    #region Campaign Db Sets
    public DbSet<Models.Campaign.CharacterDAO> CampaignCharacter => Set<Models.Campaign.CharacterDAO>();
    public DbSet<Models.Campaign.CharacterPerkDAO> CampaignCharacterAppliedPerk => Set<Models.Campaign.CharacterPerkDAO>();
    public DbSet<CharacterItemDAO> CampaignCharacterItem => Set<CharacterItemDAO>();
    public DbSet<Models.Campaign.ScenarioDAO> CampaignScenario => Set<Models.Campaign.ScenarioDAO>();
    public DbSet<CampaignDAO> CampaignCampaign => Set<CampaignDAO>();
    public DbSet<CampaignItemDAO> CampaignCampaignItem => Set<CampaignItemDAO>();
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
        BeforeSave();
        return base.SaveChanges();
    }


    private void BeforeSave()
    {
        DateTime nowUTC = DateTime.UtcNow;
        var auditEntries = new List<AuditEntry>();

        ChangeTracker.Entries()
            .Where(entity => entity.State != EntityState.Detached && entity.State != EntityState.Unchanged)
            .ToList()
            .ForEach(changedEntity =>
            {
                if (changedEntity.Entity is AuditableEntityBase auditEntity)
                {
                    auditEntries.Add(GenerateAuditEntry(changedEntity, nowUTC));
                    ApplyAuditDatesToChangedEntry(changedEntity, auditEntity, nowUTC);

                }
            });
    }

    private AuditEntry GenerateAuditEntry(EntityEntry changedEntity, DateTime dateTime)
    {
        AuditEntry auditEntry = new AuditEntry(changedEntity);
        auditEntry.TableName = changedEntity.Entity.GetType().Name;
        auditEntry.UserId = null;
        auditEntry.DateTimeUTC = dateTime;
        foreach (var property in changedEntity.Properties)
        {
            string propertyName = property.Metadata.Name;
            if (property.Metadata.IsPrimaryKey())
            {
                auditEntry.KeyValues[propertyName] = property.CurrentValue;
            }
            else
            {
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
        }
        return auditEntry;
    }

    private void ApplyAuditDatesToChangedEntry(EntityEntry changedEntity, AuditableEntityBase auditEntity, DateTime dateTime)
    {
        switch (changedEntity.State)
        {
            case EntityState.Added:
                auditEntity.CreatedOnUTC = dateTime;
                auditEntity.UpdatedOnUTC = dateTime;
                break;

            case EntityState.Modified:
                Entry(auditEntity).Property(x => x.CreatedOnUTC).IsModified = false;
                auditEntity.UpdatedOnUTC = dateTime;
                break;
        }
    }

}