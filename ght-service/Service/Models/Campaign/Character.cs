using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace GloomhavenTracker.Service.Models.Campaign;

public class Character
{
    public Character(Guid id, string name, Content.Character characterContent, int experience, int gold, int perkPoints)
    {
        Id = id;
        Name = name;
        CharacterContent = characterContent;
        Experience = experience;
        Gold = gold;
        PerkPoints = perkPoints;
    }

    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Models.Content.Character CharacterContent { get; set; }
    public int Experience { get; set; }
    public int Gold { get; set; }
    public int PerkPoints {get; set; }
}

[Serializable]
public struct CharacterDTO
{
    public CharacterDTO(Guid id, string name, string characterContentCode, int experience, int gold, List<string> items, int perkPoints)
    {
        Id = id;
        Name = name;
        CharacterContentCode = characterContentCode;
        Experience = experience;
        Gold = gold;
        Items = items;
        PerkPoints = perkPoints;
    }

    [JsonPropertyName("id")]
    public Guid Id { get; }

    [JsonPropertyName("name")]
    public string Name { get; }

    [JsonPropertyName("characterContentCode")]
    public string CharacterContentCode { get; }

    [JsonPropertyName("experience")]
    public int Experience { get; }

    [JsonPropertyName("gold")]
    public int Gold { get; }

    [JsonPropertyName("items")]
    public List<string> Items { get; }

    [JsonPropertyName("perkPoints")]
    public int PerkPoints { get; }
}


[Serializable]
public struct CharacterSummary
{
    public CharacterSummary(Guid id, string name, string characterContentCode, int level)
    {
        Id = id;
        Name = name;
        CharacterContentCode = characterContentCode;
        Level = level;
    }

    [JsonPropertyName("id")]
    public Guid Id { get; }

    [JsonPropertyName("name")]
    public string Name { get; }

    [JsonPropertyName("characterContentCode")]
    public string CharacterContentCode { get; }

    [JsonPropertyName("level")]
    public int Level { get; }
}


public struct CharacterRequestBody
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("characterContentCode")]
    public string? CharacterContentCode { get; set; }

    [JsonPropertyName("experience")]
    public int? Experience { get; set; }

    [JsonPropertyName("gold")]
    public int? Gold { get; set; }

    [JsonPropertyName("perkPoints")]
    public int? PerkPoints { get; set; }
}