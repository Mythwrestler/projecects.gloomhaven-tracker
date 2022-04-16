using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GloomhavenTracker.Service.Models.Content;

[Serializable]
public struct BaseCharacterHealth
{
    public BaseCharacterHealth(int level, int health)
    {
        Level = level;
        Health = health;
    }

    [JsonPropertyName("level")]
    public int Level { get; set; }
    [JsonPropertyName("health")]
    public int Health { get; set; }
}

[Serializable]
public struct CharacterLevel
{
    public CharacterLevel(int experience, int level)
    {
        Experience = experience;
        Level = level;
    }

    [JsonPropertyName("experience")]
    public int Experience { get; }
    [JsonPropertyName("level")]
    public int Level { get; }
}

[Serializable]
public struct BaseCharacterStats
{
    public BaseCharacterStats(List<CharacterLevel> levels, List<BaseCharacterHealth> health)
    {
        Levels = levels;
        Health = health;
    }

    [JsonPropertyName("levels")]
    public List<CharacterLevel> Levels { get; }
    [JsonPropertyName("health")]
    public List<BaseCharacterHealth> Health { get; }
}

[Serializable]
public struct Character : ContentItem
{
    public Character(Guid id, string contentCode, string name, string description, BaseCharacterStats baseStats, string gameContentCode)
    {
        Id = id;
        ContentCode = contentCode;
        Name = name;
        Description = description;
        BaseStats = baseStats;
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

    [JsonPropertyName("baseStats")]
    public BaseCharacterStats BaseStats { get; }

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

