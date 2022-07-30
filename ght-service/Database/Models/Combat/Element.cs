using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GloomhavenTracker.Database.Models.Combat;

public static partial class EntityDefinitions
{
    private static EnumToStringConverter<Content.ELEMENT_DAO> elementTypeConverter = new EnumToStringConverter<Content.ELEMENT_DAO>();
    private static EnumToStringConverter<ELEMENT_STRENGTH_DAO> elementStrengthConverter = new EnumToStringConverter<ELEMENT_STRENGTH_DAO>();
    public static void DefineCombatElementEntities(this ModelBuilder builder)
    {
        builder.Entity<ElementDAO>(elementTable => {
            elementTable.Property(element => element.Element).HasConversion(elementTypeConverter);
            elementTable.Property(element => element.Strength).HasConversion(elementStrengthConverter);
            elementTable.HasIndex(element => new { element.CombatId, element.Element}).IsUnique();
        });
    }
}

public enum ELEMENT_STRENGTH_DAO
{
    spent,
    weak,
    strong
}

public class ElementDAO : AuditableEntityBase
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    public Guid CombatId { get; set; }
    public CombatDAO? Combat { get; set; }
    public Content.ELEMENT_DAO Element { get; set; }
    public ELEMENT_STRENGTH_DAO Strength { get; set; }
    public Guid CreatedBy { get; set; }
    public Guid UpdatedBy { get; set; }
}