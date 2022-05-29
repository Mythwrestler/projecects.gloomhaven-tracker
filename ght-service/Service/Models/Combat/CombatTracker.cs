using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace GloomhavenTracker.Service.Models.Combat;

[Serializable]
public class CombatTrackerSummary
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("gameCode")]
    public string GameCode { get; set; } = string.Empty;

    [JsonPropertyName("campaign")]
    public string CampaignId { get; set; } = string.Empty;

    [JsonPropertyName("scenarioContentCode")]
    public string ScenarioContentCode { get; set; } = string.Empty;

    [JsonPropertyName("scenarioLevel")]
    public int ScenarioLevel { get; set; } = 0;

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;
}

[Serializable]
public class CombatTrackerDO
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("gameCode")]
    public string GameCode { get; set; } = string.Empty;

    [JsonPropertyName("campaign")]
    public string CampaignId { get; set; } = string.Empty;

    [JsonPropertyName("scenarioContentCode")]
    public string ScenarioContentCode { get; set; } = string.Empty;

    [JsonPropertyName("scenarioLevel")]
    public int ScenarioLevel { get; set; } = 0;

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("elements")]
    public List<ElementDO> Elements { get; set; } = new List<ElementDO>();

    [JsonPropertyName("characters")]
    public List<CharacterDO> Characters { get; set; } = new List<CharacterDO>();

    [JsonPropertyName("monsters")]
    public List<MonsterGroupDO> Monsters { get; set; } = new List<MonsterGroupDO>();

    [JsonPropertyName("objectives")]
    public List<ObjectiveGroupDO> Objectives { get; set; } = new List<ObjectiveGroupDO>();

    [JsonPropertyName("initiative")]
    public InitiativeDO Initiative { get; set; } = new InitiativeDO();

    [JsonPropertyName("MonsterModifierDeck")]
    public AttackModifierDeckDO MonsterModifierDeck { get; set; } = new AttackModifierDeckDO();
}

[Serializable]
public class CombatTrackerDTO
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("gameCode")]
    public string GameCode { get; set; } = string.Empty;

    [JsonPropertyName("campaign")]
    public string CampaignId { get; set; } = string.Empty;

    [JsonPropertyName("scenarioContentCode")]
    public string ScenarioContentCode { get; set; } = string.Empty;

    [JsonPropertyName("scenarioLevel")]
    public int ScenarioLevel { get; set; } = 0;

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("elements")]
    public List<ElementDTO> Elements { get; set; } = new List<ElementDTO>();

    [JsonPropertyName("characters")]
    public List<CharacterDTO> Characters { get; set; } = new List<CharacterDTO>();

    [JsonPropertyName("monsters")]
    public List<MonsterGroupDTO> Monsters { get; set; } = new List<MonsterGroupDTO>();

    [JsonPropertyName("objectives")]
    public List<ObjectiveGroupDTO> Objectives { get; set; } = new List<ObjectiveGroupDTO>();

    [JsonPropertyName("initiative")]
    public InitiativeDTO Initiative { get; set; } = new InitiativeDTO();
}


public class CombatTracker
{
    public Guid Id { get; }
    public string GameCode { get; }
    public Guid CampaignId { get; }
    public string ScenarioContentCode { get; }
    public string Description { get; }
    public int ScenarioLevel { get; }
    public Elements Elements { get; }
    public List<Character> Characters { get; }
    public List<MonsterGroup> Monsters { get; }
    public List<ObjectiveGroup> Objectives { get; }
    public Initiative Initiative { get; }
    public AttackModifierDeck MonsterModifierDeck { get; }


    // instantiate From Data Persistence Object
    public CombatTracker(CombatTrackerDO combat)
    {
        this.Id = new Guid(combat.Id);
        this.GameCode = combat.GameCode;
        this.CampaignId = new Guid(combat.CampaignId);
        this.ScenarioContentCode = combat.ScenarioContentCode;
        this.Description = combat.Description;
        this.ScenarioLevel = combat.ScenarioLevel;
        this.Elements = new Elements(combat.Elements);
        this.Characters = combat.Characters.Select(character => new Character(character)).ToList();
        this.Monsters = combat.Monsters.Select(monsterGroup => new MonsterGroup(monsterGroup)).ToList();
        this.Objectives = combat.Objectives.Select(objectiveGroup => new ObjectiveGroup(objectiveGroup)).ToList();
        this.Initiative = new Initiative(combat.Initiative);
        this.MonsterModifierDeck = new AttackModifierDeck(combat.MonsterModifierDeck);
    }

    // Generate Data Persistence Object
    public CombatTrackerDO DataObject
    {
        get {
            return new CombatTrackerDO()
            {
                Id = this.Id.ToString(),
                GameCode = this.GameCode,
                CampaignId = this.CampaignId.ToString(),
                ScenarioContentCode = this.ScenarioContentCode,
                Description = this.Description,
                ScenarioLevel = this.ScenarioLevel,
                Elements = this.Elements.DataObject,
                Characters = this.Characters.Select(character => character.DataObject).ToList(),
                Monsters = this.Monsters.Select(monsterGroup => monsterGroup.DataObject).ToList(),
                Objectives = this.Objectives.Select(objectiveGroup => objectiveGroup.DataObject).ToList(),
                Initiative = this.Initiative.ToDO(),
                MonsterModifierDeck = this.MonsterModifierDeck.ToDO()
            };
        }
    }

    public CombatTrackerDTO DataTransferObject
    {
        get {
            return new CombatTrackerDTO() 
            {
                Id = this.Id.ToString(),
                GameCode = this.GameCode,
                CampaignId = this.CampaignId.ToString(),
                ScenarioContentCode = this.ScenarioContentCode,
                Description = this.Description,
                ScenarioLevel = this.ScenarioLevel,
                Elements = this.Elements.DataTransferObject,
                Characters = this.Characters.Select(character => character.DataTransferObject).ToList(),
                Monsters = this.Monsters.Select(monsterGroup => monsterGroup.DataTransferObject).ToList(),
                Objectives = this.Objectives.Select(objectiveGroup => objectiveGroup.DataTransferObject).ToList(),
                Initiative = this.Initiative.TurnOrder,
            };
        }
    }

    public CombatTrackerSummary Summary 
    {
        get {
            return new CombatTrackerSummary()
            {
                Id = this.Id.ToString(),
                CampaignId = this.CampaignId.ToString(),
                ScenarioContentCode = this.ScenarioContentCode,
                ScenarioLevel = this.ScenarioLevel,
                Description = this.Description,
                GameCode = this.GameCode
            };
        }
    }

}

[Serializable]
public struct NewCombatTracker
{
    [JsonPropertyName("campaignId")]
    public Guid CampaignId { get; set; }

    [JsonPropertyName("scenarioContentCode")]
    public string ScenarioContentCode { get; set; }
}