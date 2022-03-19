using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
}


public class MonsterStatSet
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public int Level { get; set; }
    public string Health { get; set; } = string.Empty;
    public string Movement { get; set; } = string.Empty;
    public string Attack { get; set; } = string.Empty;
    public List<EFFECT_TYPE> Immunity { get; set; } = new List<EFFECT_TYPE>();
    public List<MonsterEffect> DefenseEffects { get; set; } = new List<MonsterEffect>();
    public List<MonsterEffect> AttackEffects { get; set; } = new List<MonsterEffect>();
    public Boolean IsElite { get; set; }
    public Guid? MonsterId {get; set;}
    public Monster? Monster {get; set; }
}


public class MonsterEffect : Effect {}