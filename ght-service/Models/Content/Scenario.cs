using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GloomhavenTracker.Service.Models.Content;

public class Scenario
{

    [JsonPropertyName("contentCode")]
    public string ContentCode { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("scenarioNumber")]
    public int ScenarioNumber { get; set; }

    [JsonPropertyName("monsters")]
    public List<Monster> Monsters { get; set; } = new List<Monster>();

    [JsonPropertyName("objectives")]
    public List<Objective> Objectives { get; set; } = new List<Objective>();

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
