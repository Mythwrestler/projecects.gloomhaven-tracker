using System.ComponentModel.DataAnnotations;
using GloomhavenTracker.Database.Models.Campaign;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GloomhavenTracker.Database.Models.Content;
public static partial class EntityDefinitions
{
    private static EnumToStringConverter<LOCATION_DAO> itemLocation = new EnumToStringConverter<LOCATION_DAO>();
    private static EnumToStringConverter<FREQUENCY_DAO> itemFrequency = new EnumToStringConverter<FREQUENCY_DAO>();
    public static void DefineItemEntities(this ModelBuilder builder)
    {
        builder.Entity<ItemDAO>(itemTable =>
        {
            itemTable.HasMany(item => item.CampaginItems).WithOne(ci => ci.Item).OnDelete(DeleteBehavior.Restrict);
            itemTable.Property(item => item.Location).HasConversion(itemLocation);
            itemTable.Property(item => item.Frequency).HasConversion(itemFrequency);
        });
    }
}


public enum LOCATION_DAO
{
    chest,
    feet,
    hand,
    head,
    small
}

public enum FREQUENCY_DAO
{
    single,
    multiple
}

public class ItemDAO
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public string ContentCode { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Number { get; set; }
    public LOCATION_DAO Location { get; set; }
    public FREQUENCY_DAO Frequency { get; set; }
    public virtual ICollection<CampaignItemDAO> CampaginItems { get; set; } = new HashSet<CampaignItemDAO>();
    [Required]
    public Guid GameId { get; set; }
    public GameDAO Game { get; set; } = new GameDAO();
}