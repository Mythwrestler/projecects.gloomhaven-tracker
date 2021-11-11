using System;
using System.Text.Json.Serialization;

namespace GloomhavenTracker.Service.Models.Content;

[Serializable]
public class Objective : ContentItem
{
    [JsonPropertyName("health")]
    public string Health { get; set; } = string.Empty;

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
}