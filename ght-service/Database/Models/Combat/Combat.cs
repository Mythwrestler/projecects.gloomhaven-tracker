using System.ComponentModel.DataAnnotations;
using GloomhavenTracker.Database.Models.Campaign;
using Microsoft.EntityFrameworkCore;

namespace GloomhavenTracker.Database.Models.Combat;

public static partial class EntityDefinitions
{
    public static void DefineCombatEntities(this ModelBuilder builder)
    {
        builder.Entity<CombatDAO>(combatTable => {
            combatTable.HasMany(combat => combat.Monsters).WithOne(monster => monster.Combat).OnDelete(DeleteBehavior.Restrict);
            combatTable.HasMany(combat => combat.Characters).WithOne(character => character.Combat).OnDelete(DeleteBehavior.Restrict);
            combatTable.HasMany(combat => combat.Objectives).WithOne(objective => objective.Combat).OnDelete(DeleteBehavior.Restrict);
            combatTable.HasMany(combat => combat.Elements).WithOne(element => element.Combat).OnDelete(DeleteBehavior.Restrict);
        });
    }
}

public class CombatDAO : AuditableEntityBase
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    [Required]
    public Guid CampaignId { get; set; }
    public CampaignDAO? Campaign { get; set; }
    [Required]
    public Guid ScenarioId { get; set; }
    public Content.ScenarioDAO? Scenario { get; set; }
    public int ScenarioLevel { get; set; }
    public Guid MonsterModifierDeckId { get; set; }
    public AttackModifierDeckDAO? MonsterModifierDeck { get; set; }
    public ICollection<Combat.MonsterDAO> Monsters { get; set; } = new HashSet<Combat.MonsterDAO>();
    public ICollection<Combat.CharacterDAO> Characters { get; set; } = new HashSet<Combat.CharacterDAO>();
    public ICollection<Combat.ObjectiveDAO> Objectives { get; set; } = new HashSet<Combat.ObjectiveDAO>();
    public ICollection<Combat.ElementDAO> Elements { get; set; } = new HashSet<Combat.ElementDAO>();
    public Guid CreatedBy { get; set; }
    public Guid UpdatedBy { get; set; }
}