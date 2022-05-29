using System;
using System.Text.Json.Serialization;

namespace GloomhavenTracker.Service.Models.Content;

[Serializable]
public struct Objective : ContentItem
{
    public Objective(Guid id, string contentCode, string name, string description, bool rangeAttackable, bool meleeAttackable, string health, string gameContentCode)
    {
        Id = id;
        ContentCode = contentCode;
        Name = name;
        Description = description;
        RangeAttackable = rangeAttackable;
        MeleeAttackable = meleeAttackable;
        Health = health;
        GameContentCode = gameContentCode;
    }

    [JsonIgnore]
    public Guid Id { get; }

    [JsonPropertyName("contentCode")]
    public string ContentCode { get; }

    [JsonPropertyName("name")]
    public string Name { get; }

    [JsonPropertyName("description")]
    public string Description { get; }

    [JsonPropertyName("rangeAttackable")]
    public bool RangeAttackable { get; }

    [JsonPropertyName("meleeAttackable")]
    public bool MeleeAttackable { get; }

    [JsonPropertyName("health")]
    public string Health { get; }

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