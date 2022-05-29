using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GloomhavenTracker.Service.Models.Content;

public enum GAME_TYPE
{
    original,
    jawsOfTheLion
}

[Serializable]
public struct Game : ContentItem
{
    public Game(Guid id, string contentCode, string name, string description, List<Scenario> scenarios, List<AttackModifier> baseModifierDeck)
    {
        Id = id;
        ContentCode = contentCode;
        Name = name;
        Description = description;
        BaseModifierDeck = baseModifierDeck;
        Scenarios = scenarios;
        GameContentCode = contentCode;
    }

    [JsonIgnore]
    public Guid Id { get; }

    [JsonPropertyName("contentCode")]
    public string ContentCode { get; }

    [JsonPropertyName("name")]
    public string Name { get; }

    [JsonPropertyName("description")]
    public string Description { get; }

    [JsonPropertyName("scenarios")]
    public List<Scenario> Scenarios { get; }

    [JsonPropertyName("baseModifierDeck")]
    public List<AttackModifier> BaseModifierDeck { get; }

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
                Game: GameContentCode
            );
        }
    }
}