using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GloomhavenTracker.Service.Models.Content;

[Serializable]
public class BaseCharacterHealth
{
    [JsonPropertyName("level")]
    public int Level { get; set; }
    [JsonPropertyName("health")]
    public int Health { get; set; }
}

[Serializable]
public class CharacterLevel
{
    [JsonPropertyName("experience")]
    public int Experience { get; set; }
    [JsonPropertyName("level")]
    public int Level { get; set; }
}

[Serializable]
public class BaseCharacterStats
{
    [JsonPropertyName("levels")]
    public List<CharacterLevel> Levels { get; set; } = new List<CharacterLevel>();
    [JsonPropertyName("health")]
    public List<BaseCharacterHealth> Health { get; set; } = new List<BaseCharacterHealth>();
}

[Serializable]
public class Character
{
    [JsonPropertyName("contentCode")]
    public string ContentCode { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("baseStats")]
    public BaseCharacterStats BaseStats { get; set; } = new BaseCharacterStats();

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

