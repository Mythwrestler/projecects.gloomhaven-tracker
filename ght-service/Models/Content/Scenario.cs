using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GloomhavenTracker.Service.Models.Content;

public class ScenarioContent : ContentItem
{
    [JsonPropertyName("scenarioNumber")]
    public int ScenarioNumber {get; set;}

    [JsonPropertyName("monsters")]
    public List<Monster> Monsters {get; set;} = new List<Monster>();

    [JsonPropertyName("objectives")]
    public List<Objective> Objectives {get; set;} = new List<Objective>();

    [JsonIgnore]
    public ContentSummary Summary { get {
        return new ContentSummary(){
            ContentCode = ContentCode,
            Name = Name,
            Description = Description
        };
    }}
}
