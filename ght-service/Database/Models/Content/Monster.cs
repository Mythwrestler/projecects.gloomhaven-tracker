using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GloomhavenTracker.Database.Models.Content;

public class MonsterStatSet
{
    public int Level { get; set; }
    public string Health { get; set; } = string.Empty;
    public string Movement { get; set; } = string.Empty;
    public string Attack { get; set; } = string.Empty;
    public Boolean isElite { get; set; }
    public Guid? MonsterId {get; set;}
    public Monster? Monster {get; set; }
}

public class Monster
{
    [Key]
    public Guid MonsterId {get; set;} = Guid.NewGuid();
    
    [Required, MaxLength(50), StringLength(50)]
    public string ContentCode { get; set; } = string.Empty;

    [Required, MaxLength(150), StringLength(150)]
    public string Name { get; set; } = string.Empty;
    
    [Required, MaxLength(500), StringLength(500)]
    public string Description { get; set; } = string.Empty;
    public List<MonsterStatSet> BaseStats { get; set; } = new List<MonsterStatSet>();
}