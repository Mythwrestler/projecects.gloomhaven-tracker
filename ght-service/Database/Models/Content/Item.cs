using System.ComponentModel.DataAnnotations;
using GloomhavenTracker.Database.Models.Campaign;
using Microsoft.EntityFrameworkCore;

namespace GloomhavenTracker.Database.Models.Content;


public static partial class EntityDefinitions
{
    public static void DefineItemEntities(this ModelBuilder builder)
    {
        builder.Entity<ItemDAO>(itemTable =>
        {
            itemTable.HasMany(item => item.CharacterItems).WithOne(ci => ci.Item).OnDelete(DeleteBehavior.Restrict);
        });
    }
}

public class ItemDAO
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public string ContentCode { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public ICollection<CharacterItemDAO> CharacterItems { get; set; } = new HashSet<CharacterItemDAO>();
}