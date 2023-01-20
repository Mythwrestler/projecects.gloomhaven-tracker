using System.ComponentModel.DataAnnotations;

namespace GloomhavenTracker.Database.Models.Combat;

public class Combatant : AuditableEntityBase
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    public Guid CombatId { get; set; }
    public CombatDAO Combat { get; set; } = null!;
    public Guid CreatedBy { get; set; }
    public Guid UpdatedBy { get; set; }
}