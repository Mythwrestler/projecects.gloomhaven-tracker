using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace GloomhavenTracker.Database.Models.Content;

public static partial class EntityDefinitions
{
    public static void DefineMonsterEntities(this ModelBuilder builder)
    {
        builder.Entity<Monster>(monsterTable =>
        {
            monsterTable.HasIndex(monster => new { monster.GameId, monster.ContentCode }).IsUnique();
            monsterTable.HasMany(monster => monster.BaseStats).WithOne(bs => bs.Monster).OnDelete(DeleteBehavior.Restrict);
            monsterTable.HasMany(monster => monster.ScenarioMonsters).WithOne(sm => sm.Monster).OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<MonsterStatSet>(monsterStatTable =>
        {
            monsterStatTable.HasMany(ae => ae.AttackEffects).WithOne(me => me.MonsterStatSet).OnDelete(DeleteBehavior.Restrict);
            monsterStatTable.HasMany(ae => ae.DefenseEffects).WithOne(me => me.MonsterStatSet).OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<MonsterAttackEffect>(monsterAttackEffectTable =>
        {
            monsterAttackEffectTable.HasKey(me => new { me.EffectId, me.MonsterStatSetId });
        });

        builder.Entity<MonsterDefenseEffect>(monsterDefenseEffectTable =>
        {
            monsterDefenseEffectTable.HasKey(me => new { me.EffectId, me.MonsterStatSetId });
        });

    }
}

public class Monster
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public string ContentCode { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public ICollection<MonsterStatSet> BaseStats { get; set; } = new HashSet<MonsterStatSet>();
    public ICollection<ScenarioMonster> ScenarioMonsters { get; set; } = new HashSet<ScenarioMonster>();
    [Required]
    public Guid GameId { get; set; }
    public Game? Game { get; set; }
}

public class MonsterStatSet
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public int Level { get; set; }
    public string Health { get; set; } = string.Empty;
    public string Movement { get; set; } = string.Empty;
    public string Attack { get; set; } = string.Empty;
    public List<EFFECT_TYPE> Immunity { get; set; } = new List<EFFECT_TYPE>();
    public virtual ICollection<MonsterDefenseEffect> DefenseEffects { get; set; } = new HashSet<MonsterDefenseEffect>();
    public virtual ICollection<MonsterAttackEffect> AttackEffects { get; set; } = new HashSet<MonsterAttackEffect>();
    public Boolean IsElite { get; set; }
    [Required]
    public Guid MonsterId { get; set; }
    public Monster? Monster { get; set; }
}

public class MonsterAttackEffect
{
    [Required]
    public Guid EffectId { get; set; }
    public Effect? Effect { get; set; }
    [Required]
    public Guid MonsterStatSetId { get; set; }
    public MonsterStatSet? MonsterStatSet { get; set; }
}

public class MonsterDefenseEffect
{
    [Required]
    public Guid EffectId { get; set; }
    public Effect? Effect { get; set; }
    [Required]
    public Guid MonsterStatSetId { get; set; }
    public MonsterStatSet? MonsterStatSet { get; set; }
}