using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace GloomhavenTracker.Database.Models.Content;

public class Monster
{
    [Key]
    public Guid Id {get; set;} = Guid.NewGuid();
    
    [Required, MaxLength(50), StringLength(50)]
    public string ContentCode { get; set; } = string.Empty;

    [Required, MaxLength(150), StringLength(150)]
    public string Name { get; set; } = string.Empty;
    
    [Required, MaxLength(500), StringLength(500)]
    public string Description { get; set; } = string.Empty;
    public List<MonsterStatSet> BaseStats { get; set; } = new List<MonsterStatSet>();
    public Guid? GameId { get; set; }
    public Game? Game { get; set; }
}

public class MonsterStatSet
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public int Level { get; set; }
    public string Health { get; set; } = string.Empty;
    public string Movement { get; set; } = string.Empty;
    public string Attack { get; set; } = string.Empty;
    public List<EFFECT_TYPE> Immunity { get; set; } = new List<EFFECT_TYPE>();
    public virtual ICollection<MonsterDefenseEffect> DefenseEffects { get; set; } = new HashSet<MonsterDefenseEffect>();
    public virtual ICollection<MonsterAttackEffect> AttackEffects { get; set; } = new HashSet<MonsterAttackEffect>();
    public Boolean IsElite { get; set; }
    public Guid? MonsterId {get; set;}
    public Monster? Monster {get; set; }
}

public class MonsterAttackEffect
{
    public Guid? EffectId {get; set;}
    public Effect? Effect {get; set;}
    public Guid? MonsterStatSetId {get; set;}
    public MonsterStatSet? MonsterStatSet {get; set;}
}

public class MonsterDefenseEffect
{
    public Guid? EffectId {get; set;}
    public Effect? Effect {get; set;}
    public Guid? MonsterStatSetId {get; set;}
    public MonsterStatSet? MonsterStatSet {get; set;}
}