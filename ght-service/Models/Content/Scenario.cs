using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GloomhavenTracker.Service.Models.Content;

public class ScenarioSummary: ContentSummary
{

    [JsonPropertyName("scenarioNumber")]
    public int ScenarioNumber { get; set; }    
}

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

    [JsonPropertyName("goal")]
    public string Goal { get; set; } = string.Empty;

    [JsonPropertyName("cityMapLocation")]
    public string CityMapLocation { get; set; } = string.Empty;

    [JsonPropertyName("scenarioBook")]
    public List<int> ScenarioBook { get; set; } = new List<int>();

    [JsonPropertyName("supplementalBook")]
    public List<int> SupplementalBook { get; set; } = new List<int>();

    [JsonPropertyName("monsters")]
    public List<Monster> Monsters { get; set; } = new List<Monster>();

    [JsonPropertyName("objectives")]
    public List<Objective> Objectives { get; set; } = new List<Objective>();

    [JsonIgnore]
    public ScenarioSummary Summary
    {
        get
        {
            return new ScenarioSummary()
            {
                ContentCode = ContentCode,
                Name = Name,
                Description = Description,
                ScenarioNumber = ScenarioNumber,
            };
        }
    }
}
