using System.ComponentModel.DataAnnotations;
using GloomhavenTracker.Database.Models.Content;
using Microsoft.EntityFrameworkCore;

namespace GloomhavenTracker.Database.Models.Combat;


public static partial class EntityDefinitions
{
    public static void DefineCombatAttackModifierDeckEntities(this ModelBuilder builder)
    {
        builder.Entity<AttackModifierDeckDAO>(attackModifierDeckTable => {
            attackModifierDeckTable.HasMany(deck => deck.Cards).WithOne(card => card.Deck).OnDelete(DeleteBehavior.Restrict);
        });
        builder.Entity<AttackModifierDeckCardDAO>(attackModifierDeckCardsTable => {
            attackModifierDeckCardsTable.HasKey(card => new {card.DeckId, card.AttackModifierId});
            attackModifierDeckCardsTable.HasIndex(card => new {card.DeckId, card.position}).IsUnique();
        });
    }
}

public class AttackModifierDeckDAO : AuditableEntityBase
{
    public Guid Id { get; set; }
    public List<int> Positions { get; set; } = new List<int>();
    public ICollection<AttackModifierDeckCardDAO> Cards { get; set; } = new HashSet<AttackModifierDeckCardDAO>();
    public Guid CreatedBy { get; set; }
    public Guid UpdatedBy { get; set; }
}

public class AttackModifierDeckCardDAO
{
    [Required]
    public Guid DeckId { get; set; }
    public AttackModifierDeckDAO? Deck { get; set; } 
    [Required]
    public int position { get; set; }
    [Required]
    public Guid AttackModifierId { get; set; }
    public AttackModifierDAO? AttackModifier { get; set; }
}