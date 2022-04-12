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

    [JsonPropertyName("rangeAttackable")]
    public bool RangeAttackable { get; set; } = true;

    [JsonPropertyName("meleeAttackable")]
    public bool MeleeAttackable { get; set; } = true;

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