using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace GloomhavenTracker.Database.Models.Content;

public static partial class EntityDefinitions
{
    public static void DefineMonsterEntities(this ModelBuilder builder)
    {
        builder.Entity<MonsterDAO>(monsterTable =>
        {
            monsterTable.HasIndex(monster => new { monster.GameId, monster.ContentCode }).IsUnique();
            monsterTable.HasMany(monster => monster.BaseStats).WithOne(bs => bs.Monster).OnDelete(DeleteBehavior.Restrict);
            monsterTable.HasMany(monster => monster.ScenarioMonsters).WithOne(sm => sm.Monster).OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<MonsterStatSetDAO>(monsterStatTable =>
        {
            monsterStatTable.HasMany(ae => ae.AttackEffects).WithOne(me => me.MonsterStatSet).OnDelete(DeleteBehavior.Restrict);
            monsterStatTable.HasMany(ae => ae.DefenseEffects).WithOne(me => me.MonsterStatSet).OnDelete(DeleteBehavior.Restrict);
            monsterStatTable.HasMany(ae => ae.DeathEffects).WithOne(me => me.MonsterStatSet).OnDelete(DeleteBehavior.Restrict);
            monsterStatTable.HasMany(mi => mi.Immunity).WithOne(me => me.MonsterStatSet).OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<MonsterAttackEffectDAO>(monsterAttackEffectTable =>
        {
            monsterAttackEffectTable.HasKey(me => new { me.EffectId, me.MonsterStatSetId });
        });

        builder.Entity<MonsterDefenseEffectDAO>(monsterDefenseEffectTable =>
        {
            monsterDefenseEffectTable.HasKey(me => new { me.EffectId, me.MonsterStatSetId });
        });

        builder.Entity<MonsterDeathEffectDAO>(monsterDeathEffectTable =>
        {
            monsterDeathEffectTable.HasKey(me => new { me.EffectId, me.MonsterStatSetId });
        });

        builder.Entity<MonsterBaseStatImmunityDAO>(monsterBaseStatImmunityTable =>
        {
            monsterBaseStatImmunityTable.HasKey(mi => new {mi.MonsterStatSetId, mi.Effect});
            monsterBaseStatImmunityTable.Property(mi => mi.Effect).HasConversion(effectType);
        });

    }
}

public class MonsterDAO
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public string ContentCode { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public virtual ICollection<MonsterStatSetDAO> BaseStats { get; set; } = new HashSet<MonsterStatSetDAO>();
    public virtual ICollection<ScenarioMonsterDAO> ScenarioMonsters { get; set; } = new HashSet<ScenarioMonsterDAO>();
    [Required]
    public Guid GameId { get; set; }
    public GameDAO? Game { get; set; }
}

public class MonsterStatSetDAO
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public int Level { get; set; }
    public string Health { get; set; } = string.Empty;
    public string Movement { get; set; } = string.Empty;
    public string Attack { get; set; } = string.Empty;
    public bool RangeAttackable { get; set; } = true;
    public bool MeleeAttackable { get; set; } = true;
    public virtual ICollection<MonsterBaseStatImmunityDAO> Immunity { get; set; } = new HashSet<MonsterBaseStatImmunityDAO>();
    public virtual ICollection<MonsterDefenseEffectDAO> DefenseEffects { get; set; } = new HashSet<MonsterDefenseEffectDAO>();
    public virtual ICollection<MonsterAttackEffectDAO> AttackEffects { get; set; } = new HashSet<MonsterAttackEffectDAO>();
    public virtual ICollection<MonsterDeathEffectDAO> DeathEffects { get; set; } = new HashSet<MonsterDeathEffectDAO>();
    public Boolean IsElite { get; set; }
    [Required]
    public Guid MonsterId { get; set; }
    public MonsterDAO? Monster { get; set; }
}

public class MonsterAttackEffectDAO
{
    [Required]
    public Guid EffectId { get; set; }
    public EffectDAO? Effect { get; set; }
    [Required]
    public Guid MonsterStatSetId { get; set; }
    public MonsterStatSetDAO? MonsterStatSet { get; set; }
}

public class MonsterDefenseEffectDAO
{
    [Required]
    public Guid EffectId { get; set; }
    public EffectDAO? Effect { get; set; }
    [Required]
    public Guid MonsterStatSetId { get; set; }
    public MonsterStatSetDAO? MonsterStatSet { get; set; }
}

public class MonsterDeathEffectDAO
{
    [Required]
    public Guid EffectId { get; set; }
    public EffectDAO? Effect { get; set; }
    [Required]
    public Guid MonsterStatSetId { get; set; }
    public MonsterStatSetDAO? MonsterStatSet { get; set; }
}

public class MonsterBaseStatImmunityDAO
{
    [Required]
    public Guid MonsterStatSetId { get; set; }
    public MonsterStatSetDAO? MonsterStatSet { get; set; }
    public EFFECT_TYPE_DAO Effect { get; set; }
}