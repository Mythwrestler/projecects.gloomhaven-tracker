using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using GloomhavenTracker.Service.Models.Combat.Combatant;
using GloomhavenTracker.Service.Models.Combat.Hub;

namespace GloomhavenTracker.Service.Models.Combat;

public class Combat
{
    public Guid Id { get; }
    public string Description { get => $"{Campaign.Name} - {Scenario.Name} - Level {ScenarioLevel}"; }
    public Campaign.Campaign Campaign { get; }
    public Content.Scenario Scenario { get; }
    public int ScenarioLevel { get; }
    public AttackModifierDeck MonsterModifierDeck { get; }
    public List<HubClient> RegisteredClients { get; }
    public List<Character> Characters { get; }

    public Combat
    (
        Guid id,
        Campaign.Campaign campaign,
        Content.Scenario scenario,
        int scenarioLevel,
        AttackModifierDeck monsterModifierDeck,
        List<HubClient> registeredClients,
        List<Character> characters
    )
    {
        Id = id;
        Campaign = campaign;
        Scenario = scenario;
        ScenarioLevel = scenarioLevel;
        MonsterModifierDeck = monsterModifierDeck;
        RegisteredClients = registeredClients;
        Characters = characters;
    }
}


[Serializable]
public class CombatDTO
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("campaignId")]
    public Guid CampaignId { get; set; }

    [JsonPropertyName("scenarioContentCode")]
    public string ScenarioContentCode { get; set; } = string.Empty;

    [JsonPropertyName("scenarioLevel")]
    public int ScenarioLevel { get; set; } = 0;

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("monsterModifierDeck")]
    public AttackModifierDeckDTO MonsterModifierDeck { get; set; } = new AttackModifierDeckDTO();

    [JsonPropertyName("characters")]
    public List<CharacterDTO> Characters { get; set; } = new List<CharacterDTO>();
}

[Serializable]
public struct NewCombatRequest
{
    [JsonPropertyName("campaignId")]
    public Guid CampaignId { get; set; }
    [JsonPropertyName("scenarioContentCode")]
    public String ScenarioContentCode { get; set; }
}


[Serializable]
public class CombatUser
{
    [JsonPropertyName("userId")]
    public Guid UserId { get; set; }

    [JsonPropertyName("username")]
    public string Username { get; set; } = string.Empty;
}