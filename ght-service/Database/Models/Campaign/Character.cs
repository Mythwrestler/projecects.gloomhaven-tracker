using System.ComponentModel.DataAnnotations;
using GloomhavenTracker.Database.Models.Content;
using Microsoft.EntityFrameworkCore;

namespace GloomhavenTracker.Database.Models.Campaign;

public static partial class EntityDefinitions
{
    public static void DefineCharacterCampaignEntities(this ModelBuilder builder)
    {
        builder.Entity<CharacterDAO>(characterTable =>
        {
            characterTable.HasOne(character => character.CharacterContent).WithMany(character => character.CampaignCharacters).OnDelete(DeleteBehavior.Restrict);
            characterTable.HasMany(character => character.Perks).WithOne(ap => ap.Character).OnDelete(DeleteBehavior.Restrict);
            characterTable.HasMany(character => character.Items).WithOne(ci => ci.Character).OnDelete(DeleteBehavior.Restrict);
            characterTable.HasOne(character => character.Campaign).WithMany(campaign => campaign.Party).OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<CharacterPerkDAO>(appliedPerkTable => {
            appliedPerkTable.HasKey(appliedPerk => new { appliedPerk.CharacterId, appliedPerk.PerkId } );
        });

        builder.Entity<CharacterItemDAO>(characterItemTable => {
            characterItemTable.HasKey(charItem => new {charItem.CharacterId, charItem.ItemId });
        });
    }
}

public class CharacterDAO : AuditableEntityBase
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public int Experience { get; set; }
    public int Gold { get; set; }
    public int PerkPoints { get; set; }
    public ICollection<CharacterPerkDAO> Perks { get; set; } = new HashSet<CharacterPerkDAO>();
    public ICollection<CharacterItemDAO> Items { get; set; } = new HashSet<CharacterItemDAO>();
    [Required]
    public Guid CharacterContentId { get; set; }
    public Content.CharacterDAO? CharacterContent { get; set; }
    public Guid CampaignId { get; set; }
    public CampaignDAO? Campaign { get; set; }
}

public class CharacterPerkDAO : AuditableEntityBase
{
    [Required]
    public Guid PerkId { get; set; }
    public PerkDAO? Perk { get; set; }
    [Required]
    public Guid CharacterId { get; set; }
    public CharacterDAO? Character { get; set; }
    public bool Applied { get; set; }
}

public class CharacterItemDAO : AuditableEntityBase
{
    [Required]
    public Guid ItemId { get; set; }
    public ItemDAO? Item { get; set; }
    [Required]
    public Guid CharacterId { get; set; }
    public CharacterDAO? Character { get; set; }
}