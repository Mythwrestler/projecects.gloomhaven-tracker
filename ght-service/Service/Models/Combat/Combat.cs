using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GloomhavenTracker.Service.Models.Combat;

public class Combat
{
    public Guid Id { get; }
    public string Description { get => $"{Campaign.Name} - {Scenario.Name} - Level {ScenarioLevel}"; }
    public Campaign.Campaign Campaign { get; }
    public Content.Scenario Scenario { get; }
    public int ScenarioLevel { get; }
    public AttackModifierDeck MonsterModifierDeck { get; }
    public List<string> RegisteredClients { get; } = new List<string>();

    public Combat
    (
        Guid id,
        Campaign.Campaign campaign,
        Content.Scenario scenario,
        int scenarioLevel,
        AttackModifierDeck monsterModifierDeck
    )
    {
        Id = id;
        Campaign = campaign;
        Scenario = Scenario;
        ScenarioLevel = scenarioLevel;
        MonsterModifierDeck = monsterModifierDeck;
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
}

[Serializable]
public struct NewCombatRequest
{
    [JsonPropertyName("campaignId")]
    public Guid CampaignId { get; set; }
    public String ScenarioContentCode { get; set; }
}