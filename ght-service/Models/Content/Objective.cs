using System;
using System.Text.Json.Serialization;

namespace GloomhavenTracker.Service.Models.Content;

[Serializable]
public class Objective
{
    [JsonPropertyName("contentCode")]
    public string ContentCode { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("health")]
    public string Health { get; set; } = string.Empty;

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
}