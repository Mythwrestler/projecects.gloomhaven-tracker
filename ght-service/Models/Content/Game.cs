using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GloomhavenTracker.Service.Models.Content;
public class Game
{

    [JsonPropertyName("contentCode")]
    public string ContentCode { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    public List<AttackModifier> BaseModifierDeck { get; set; } = new List<AttackModifier>();

    [JsonIgnore]
    public ContentSummary Summary
    {
        get
        {
            return new ContentSummary()
            {
                ContentCode = ContentCode,
                Name = Name,
                Description = Description
            };
        }
    }
};