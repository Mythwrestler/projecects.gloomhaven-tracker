
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace GloomhavenTracker.Database.Models.Combat;


public static partial class EntityDefinitions
{
    public static void DefineCombatMonsterEntities(this ModelBuilder builder)
    {
        builder.Entity<MonsterDAO>(monsterTable => {
            monsterTable.HasMany(monster => monster.ActiveEffects).WithOne(effect => effect.Monster).OnDelete(DeleteBehavior.Restrict);
        });
        builder.Entity<MonsterActiveEffectDAO>(monsterActiveEffectsTable => {
            monsterActiveEffectsTable.HasKey(activeEffect => new {activeEffect.MonsterId, activeEffect.ActiveEffectId});
            monsterActiveEffectsTable.HasIndex(activeEffect => new {activeEffect.ActiveEffectId, activeEffect.MonsterId}).IsUnique();
        });
    }
}

public class MonsterDAO : Combatant
{
    public int MonsterNumber { get; set; }
    public int Health { get; set; }
    [Required]
    public Guid ContentMonsterId { get; set; }
    public Content.MonsterDAO? MonsterContent { get; set; }
    public ICollection<MonsterActiveEffectDAO> ActiveEffects { get; set; } = new HashSet<MonsterActiveEffectDAO>();
    public int InstanceId { get; set; }
    public bool IsElite { get; set; }
}

public class MonsterActiveEffectDAO
{
    [Required]
    public Guid MonsterId { get; set; }
    public MonsterDAO? Monster { get; set; }
    [Required]
    public Guid ActiveEffectId { get; set; }
    public ActiveEffectDAO? ActiveEffect { get; set; }
}