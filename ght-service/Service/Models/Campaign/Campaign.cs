using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using GloomhavenTracker.Service.Models.Content;

namespace GloomhavenTracker.Service.Models.Campaign;

public class Campaign
{

    public Campaign(Guid id, string name, string description, Game game, Dictionary<string, Scenario> scenarios, Dictionary<string, Character> party, User owner, Dictionary<Guid, User> managers)
    {
        Id = id;
        Name = name;
        Description = description;
        Game = game;
        Scenarios = scenarios;
        Party = party;
        Owner = owner;
        Managers = managers;
    }

    public Guid Id { get; }
    public string Name { get; }
    public string Description { get; }
    public Game Game { get; }
    public Dictionary<string, Scenario> Scenarios { get; }
    public Dictionary<string, Character> Party { get; }
    public User Owner { get; }
    public Dictionary<Guid, User> Managers { get; }
}


[Serializable]
public class CampaignSummary
{
    public CampaignSummary(Guid id, string name, string description, string gameContentCode)
    {
        Id = id;
        Name = name;
        Description = description;
        GameContentCode = gameContentCode;
    }

    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("game")]
    public string GameContentCode { get; set; }

    [JsonPropertyName("editable")]
    public bool Editable { get; set; } = false;
}


[Serializable]
public class CampaignDTO
{
    public CampaignDTO(Guid id, string name, string description, string gameContentCode, List<ScenarioDTO> scenarios, List<CharacterSummary> party)
    {
        Id = id;
        Name = name;
        Description = description;
        GameContentCode = gameContentCode;
        Scenarios = scenarios;
        Party = party;
    }

    [JsonPropertyName("id")]
    public Guid Id { get; }

    [JsonPropertyName("name")]
    public string Name { get; }

    [JsonPropertyName("description")]
    public string Description { get; }

    [JsonPropertyName("game")]
    public string GameContentCode { get; }

    [JsonPropertyName("scenarios")]
    public List<ScenarioDTO> Scenarios { get; }

    [JsonPropertyName("party")]
    public List<CharacterSummary> Party { get; }

    [JsonPropertyName("editable")]
    public bool Editable { get; set; } = false;
}




[Serializable]
public struct NewCampaignRequestBody
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("game")]
    public string GameContentCode { get; set; }
}