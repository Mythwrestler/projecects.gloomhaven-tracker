using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GloomhavenTracker.Service.Models.Content;

public struct Scenario : ContentItem
{
    public Scenario(Guid id, string contentCode, string name, string description, int scenarioNumber, string goal, string cityMapLocation, List<int> scenarioBook, List<int> supplementalBook, List<Monster> monsters, List<Objective> objectives, string gameContentCode)
    {
        Id = id;
        ContentCode = contentCode;
        Name = name;
        Description = description;
        ScenarioNumber = scenarioNumber;
        Goal = goal;
        CityMapLocation = cityMapLocation;
        ScenarioBook = scenarioBook;
        SupplementalBook = supplementalBook;
        Monsters = monsters;
        Objectives = objectives;
        GameContentCode = gameContentCode;
    }

    [JsonIgnore]
    public Guid Id { get; set; }

    [JsonPropertyName("contentCode")]
    public string ContentCode { get; } 

    [JsonPropertyName("name")]
    public string Name { get; }

    [JsonPropertyName("description")]
    public string Description { get; }

    [JsonPropertyName("scenarioNumber")]
    public int ScenarioNumber { get; }

    [JsonPropertyName("goal")]
    public string Goal { get; }

    [JsonPropertyName("cityMapLocation")]
    public string CityMapLocation { get; }

    [JsonPropertyName("scenarioBook")]
    public List<int> ScenarioBook { get; }

    [JsonPropertyName("supplementalBook")]
    public List<int> SupplementalBook { get; }

    [JsonPropertyName("monsters")]
    public List<Monster> Monsters { get; }

    [JsonPropertyName("objectives")]
    public List<Objective> Objectives { get; }

    [JsonPropertyName("game")]
    public string GameContentCode { get; }

    [JsonIgnore]
    public ContentSummary Summary
    {
        get
        {
            return new ContentSummary(
                ContentCode,
                Name,
                Description,
                Game: GameContentCode,
                SortOrder: ScenarioNumber
            );
        }
    }
}
