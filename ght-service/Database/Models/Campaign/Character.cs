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
            characterTable.HasMany(character => character.AppliedPerks).WithOne(ap => ap.Character).OnDelete(DeleteBehavior.Restrict);
            characterTable.HasMany(character => character.Items).WithOne(ci => ci.Character);
            characterTable.HasOne(character => character.Campaign).WithMany(campaign => campaign.Party).OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<CharacterPerkDAO>(characterPerkTable => {
            characterPerkTable.HasKey(cp => new {cp.CharacterId, cp.PerkId});
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
    public ICollection<CharacterPerkDAO> AppliedPerks { get; set; } = new HashSet<CharacterPerkDAO>();
    public ICollection<CampaignItemDAO> Items { get; set; } = new HashSet<CampaignItemDAO>();
    [Required]
    public Guid CharacterContentId { get; set; }
    public Content.CharacterDAO? CharacterContent { get; set; }
    public Guid CampaignId { get; set; }
    public CampaignDAO? Campaign { get; set; }
    public Guid CreatedBy { get; set; }
    public Guid UpdatedBy { get; set; }
}

public class CharacterPerkDAO : AuditableEntityBase
{
    [Required]
    public Guid PerkId { get; set; }
    public PerkDAO? Perk { get; set; }
    [Required]
    public Guid CharacterId { get; set; }
    public CharacterDAO? Character { get; set; }
}