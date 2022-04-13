using System.ComponentModel.DataAnnotations;
using GloomhavenTracker.Database.Models.Campaign;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GloomhavenTracker.Database.Models.Content;

public static partial class EntityDefinitions
{
    private static EnumToStringConverter<PERK_ACTION_DAO> perkAction = new EnumToStringConverter<PERK_ACTION_DAO>();
    public static void DefinePerkEntities(this ModelBuilder builder)
    {
        builder.Entity<PerkDAO>(perkTable =>
        {
            perkTable.Property(perk => perk.Action).HasConversion(perkAction);
            perkTable.HasMany(perk => perk.AppliedPerks).WithOne(ap => ap.Perk).OnDelete(DeleteBehavior.Restrict);
        });
    }
}

public enum PERK_ACTION_DAO
{
    add,
    remove
}
public class PerkDAO
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public string ContentCode { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public PERK_ACTION_DAO Action { get; set; }
    public ICollection<CharacterPerkDAO> AppliedPerks { get; set; } = new HashSet<CharacterPerkDAO>();
}
