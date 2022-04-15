using System.ComponentModel.DataAnnotations;
using GloomhavenTracker.Database.Models.Campaign;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GloomhavenTracker.Database.Models.Content;

public static partial class EntityDefinitions
{
    private static EnumToStringConverter<PERK_ACTION_DAO> perkActionType = new EnumToStringConverter<PERK_ACTION_DAO>();
    public static void DefinePerkEntities(this ModelBuilder builder)
    {
        builder.Entity<PerkDAO>(perkTable =>
        {
            perkTable.HasMany(perk => perk.CharacterPerks).WithOne(ap => ap.Perk).OnDelete(DeleteBehavior.Restrict);
            perkTable.HasMany(perk => perk.CharacterPerks).WithOne(cp => cp.Perk).OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<PerkActionDAO>(perkActionTable => {
            perkActionTable.Property(pa => pa.Action).HasConversion(perkActionType);
            perkActionTable.HasOne(pa => pa.AttackModifier).WithMany(am => am.PerkActions).OnDelete(DeleteBehavior.Restrict);
        });
    }
}

public class PerkDAO
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public string ContentCode { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public ICollection<PerkActionDAO> Actions { get; set; } = new HashSet<PerkActionDAO>();
    public virtual ICollection<CharacterPerkDAO> CharacterPerks { get; set; } = new HashSet<CharacterPerkDAO>();
    [Required]
    public Guid GameId { get; set; }
    public GameDAO? Game { get; set; }}

public enum PERK_ACTION_DAO
{
    add,
    remove
}

public class PerkActionDAO
{
    [Key]
    public Guid Id { get; set; }
    public Guid AttackModifierId { get; set; }
    public AttackModifierDAO? AttackModifier { get; set; }
    public PERK_ACTION_DAO Action { get; set; }
    public int Count { get; set; }
}