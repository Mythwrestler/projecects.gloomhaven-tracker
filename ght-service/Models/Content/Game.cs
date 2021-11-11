using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GloomhavenTracker.Service.Models.Content;
public class Game : ContentItem {
    public List<AttackModifier> BaseModifierDeck {get; set;} = new List<AttackModifier>();
    
    [JsonIgnore]
    public ContentSummary Summary {
        get {
            return new ContentSummary() {
                ContentCode = ContentCode,
                Name = Name,
                Description = Description
            };
        }
    }
};