using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using GloomhavenTracker.Service.Models.Content;

namespace GloomhavenTracker.Service.Models.Combat;

[Serializable]
public class CombatantDTO
{
    [JsonPropertyName("combatantCode")]
    public string CombatantCode { get; set; } = string.Empty;

    [JsonPropertyName("type")]
    public Content.CONTENT_TYPE Type { get; set; }

    [JsonPropertyName("health")]
    public int Health { get; set; }

    [JsonPropertyName("activeEffects")]
    public List<Effect> ActiveEffects { get; set; } = new List<Effect>();
}

[Serializable]
public class CharacterDO
{
    [JsonPropertyName("contentCode")]
    public string ContentCode { get; set; } = string.Empty;

    [JsonPropertyName("combatantCode")]
    public string CombatantCode { get; set; } = string.Empty;

    [JsonPropertyName("health")]
    public int Health { get; set; }

    [JsonPropertyName("activeEffects")]
    public List<Effect> ActiveEffects { get; set; } = new List<Effect>();

    [JsonPropertyName("modifierDeck")]
    public AttackModifierDeckDO ModifierDeck { get; set; } = new AttackModifierDeckDO();
}

[Serializable]
public class CharacterDTO
{
    [JsonPropertyName("contentCode")]
    public string ContentCode { get; set; } = string.Empty;

    [JsonPropertyName("combatantCode")]
    public string CombatantCode { get; set; } = string.Empty;

    [JsonPropertyName("health")]
    public int Health { get; set; }

    [JsonPropertyName("activeEffects")]
    public List<Effect> ActiveEffects { get; set; } = new List<Effect>();
}

public class Character
{
    public string ContentCode { get; }
    public string CombatantCode { get; } = string.Empty;
    public int Health { get; set; } = 0;
    public Effects ActiveEffects { get; }
    public AttackModifierDeck ModifierDeck { get; }
    public Character(CharacterDO character)
    {
        this.ContentCode = character.ContentCode;
        this.CombatantCode = character.CombatantCode;
        this.Health = character.Health;
        this.ActiveEffects = new Effects(character.ActiveEffects);
        this.ModifierDeck = new AttackModifierDeck(character.ModifierDeck);
    }
    public CharacterDO DataObject
    {
        get
        {
            return new CharacterDO()
            {
                ContentCode = this.ContentCode,
                CombatantCode = this.CombatantCode,
                Health = this.Health,
                ActiveEffects = this.ActiveEffects.DataObject,
                ModifierDeck = this.ModifierDeck.ToDO()
            };
        }
    }
    public CharacterDTO DataTransferObject
    {
        get
        {
            return new CharacterDTO()
            {
                ContentCode = this.ContentCode,
                CombatantCode = this.CombatantCode,
                Health = this.Health,
                ActiveEffects = this.ActiveEffects.DataObject
            };
        }
    }
}

[Serializable]

public class MonsterDO
{
    [JsonPropertyName("combatantCode")]
    public string CombatantCode { get; set; } = string.Empty;

    [JsonPropertyName("health")]
    public int Health { get; set; }

    [JsonPropertyName("activeEffects")]
    public List<Effect> ActiveEffects { get; set; } = new List<Effect>();
    [JsonPropertyName("instanceId")]
    public int InstanceId { get; set; }

    [JsonPropertyName("isElite")]
    public bool IsElite { get; set; }
}
[Serializable]

public class MonsterDTO
{
    [JsonPropertyName("combatantCode")]
    public string CombatantCode { get; set; } = string.Empty;

    [JsonPropertyName("health")]
    public int Health { get; set; }

    [JsonPropertyName("activeEffects")]
    public List<Effect> ActiveEffects { get; set; } = new List<Effect>();
    [JsonPropertyName("instanceId")]
    public int InstanceId { get; set; }

    [JsonPropertyName("isElite")]
    public bool IsElite { get; set; }
}
public class Monster
{
    public string CombatantCode { get; } = string.Empty;
    public int Health { get; set; } = 0;
    public Effects ActiveEffects { get; } = new Effects();
    public int InstanceId { get; }
    public bool IsElite { get; }
    public Monster
    (
        string combatantCode,
        int instanceId,
        bool isElite,
        int health,
        List<Effect> effects
    )
    {
        this.CombatantCode = combatantCode;
        this.InstanceId = instanceId;
        this.IsElite = isElite;
        this.Health = health;
        this.ActiveEffects = new Effects(effects);
    }
    public Monster(MonsterDO monster)
    {
        this.CombatantCode = monster.CombatantCode;
        this.InstanceId = monster.InstanceId;
        this.IsElite = monster.IsElite;
        this.Health = monster.Health;
        this.ActiveEffects = new Effects(monster.ActiveEffects);
    }
    public MonsterDO DataObject
    {
        get
        {
            return new MonsterDO
            {
                CombatantCode = this.CombatantCode,
                InstanceId = this.InstanceId,
                IsElite = this.IsElite,
                Health = this.Health,
                ActiveEffects = this.ActiveEffects.DataObject
            };
        }
    }
    public MonsterDTO DataTransferObject
    {
        get
        {
            return new MonsterDTO()
            {
                CombatantCode = this.CombatantCode,
                InstanceId = this.InstanceId,
                IsElite = this.IsElite,
                Health = this.Health,
                ActiveEffects = this.ActiveEffects.DataTransferObject,

            };
        }
    }
}

[Serializable]
public class MonsterGroupDO
{
    [JsonPropertyName("contentCode")]
    public string ContentCode { get; set; } = string.Empty;

    [JsonPropertyName("monsters")]
    public List<MonsterDO> Monsters { get; set; } = new List<MonsterDO>();
}
[Serializable]
public class MonsterGroupDTO
{
    [JsonPropertyName("contentCode")]
    public string ContentCode { get; set; } = string.Empty;

    [JsonPropertyName("monsters")]
    public List<MonsterDTO> Monsters { get; set; } = new List<MonsterDTO>();
}

public class MonsterGroup
{
    public string ContentCode { get; }
    public List<Monster> Monsters { get; }
    public MonsterGroup(MonsterGroupDO monsterGroup)
    {
        this.ContentCode = monsterGroup.ContentCode;
        this.Monsters = monsterGroup.Monsters.Select(monster => new Monster(monster)).ToList();
    }
    public MonsterGroupDO DataObject
    {
        get
        {
            return new MonsterGroupDO()
            {
                ContentCode = this.ContentCode,
                Monsters = this.Monsters.Select(monster => monster.DataObject).ToList()
            };
        }
    }
    public MonsterGroupDTO DataTransferObject
    {
        get
        {
            return new MonsterGroupDTO()
            {
                ContentCode = this.ContentCode,
                Monsters = this.Monsters.Select(monster => monster.DataTransferObject).ToList()
            };
        }
    }
    public void AddMonster(int instanceId, bool isElite, int health, List<Effect> effects)
    {
        Monsters.Add(new Monster($"{this.ContentCode}-{instanceId}", instanceId, isElite, health, effects));
    }
}


[Serializable]
public class ObjectiveDO
{
    [JsonPropertyName("combatantCode")]
    public string CombatantCode { get; set; } = string.Empty;

    [JsonPropertyName("health")]
    public int Health { get; set; }

    [JsonPropertyName("activeEffects")]
    public List<Effect> ActiveEffects { get; set; } = new List<Effect>();

    [JsonPropertyName("objectiveNumber")]
    public int ObjectiveNumber { get; set; }
}
[Serializable]
public class ObjectiveDTO
{
    [JsonPropertyName("combatantCode")]
    public string CombatantCode { get; set; } = string.Empty;

    [JsonPropertyName("health")]
    public int Health { get; set; }

    [JsonPropertyName("activeEffects")]
    public List<Effect> ActiveEffects { get; set; } = new List<Effect>();

    [JsonPropertyName("objectiveNumber")]
    public int ObjectiveNumber { get; set; }
}

public class Objective
{
    public string CombatantCode { get; } = string.Empty;
    public int Health { get; set; } = 0;
    public Effects ActiveEffects { get; } = new Effects();
    public int InstanceId { get; }
    public Objective(ObjectiveDO objective)
    {
        this.CombatantCode = objective.CombatantCode;
        this.InstanceId = objective.ObjectiveNumber;
        this.Health = objective.Health;
        this.ActiveEffects = new Effects(objective.ActiveEffects);
    }
    public Objective
    (
        string combatantCode,
        int instanceId,
        int health,
        List<Effect> effects
    )
    {
        this.CombatantCode = combatantCode;
        this.InstanceId = instanceId;
        this.Health = health;
        this.ActiveEffects = new Effects(effects);
    }
    public ObjectiveDO DataObject
    {
        get
        {
            return new ObjectiveDO()
            {
                CombatantCode = this.CombatantCode,
                ObjectiveNumber = this.InstanceId,
                Health = this.Health,
                ActiveEffects = this.ActiveEffects.DataObject,
            };
        }
    }
    public ObjectiveDTO DataTransferObject
    {
        get
        {
            return new ObjectiveDTO()
            {
                CombatantCode = this.CombatantCode,
                ObjectiveNumber = this.InstanceId,
                Health = this.Health,
                ActiveEffects = this.ActiveEffects.DataTransferObject,
            };
        }
    }
}

[Serializable]
public class ObjectiveGroupDO
{
    [JsonPropertyName("contentId")]
    public string ContentId { get; set; } = string.Empty;
    [JsonPropertyName("contentCode")]
    public string ContentCode { get; set; } = string.Empty;
    [JsonPropertyName("objectives")]
    public List<ObjectiveDO> Objectives { get; set; } = new List<ObjectiveDO>();
}
[Serializable]
public class ObjectiveGroupDTO
{
    [JsonPropertyName("contentId")]
    public string ContentId { get; set; } = string.Empty;
    [JsonPropertyName("contentCode")]
    public string ContentCode { get; set; } = string.Empty;
    [JsonPropertyName("objectives")]
    public List<ObjectiveDTO> Objectives { get; set; } = new List<ObjectiveDTO>();
}

public class ObjectiveGroup
{
    public Guid ContentId { get; }
    public string ContentCode { get; }
    public List<Objective> Objectives { get; }
    public ObjectiveGroup(ObjectiveGroupDO objectiveGroup)
    {
        this.ContentId = new Guid(objectiveGroup.ContentId);
        this.ContentCode = objectiveGroup.ContentCode;
        this.Objectives = objectiveGroup.Objectives.Select(objective => new Objective(objective)).ToList();
    }
    public ObjectiveGroupDO DataObject
    {
        get
        {
            return new ObjectiveGroupDO()
            {
                ContentId = this.ContentId.ToString(),
                ContentCode = this.ContentCode,
                Objectives = this.Objectives.Select(objective => objective.DataObject).ToList()
            };
        }
    }
    public ObjectiveGroupDTO DataTransferObject
    {
        get
        {
            return new ObjectiveGroupDTO()
            {
                ContentId = this.ContentId.ToString(),
                ContentCode = this.ContentCode,
                Objectives = this.Objectives.Select(objective => objective.DataTransferObject).ToList()
            };
        }
    }
    public void AddObjective(int instanceId, bool isElite, int health, List<Effect> effects)
    {
        Objectives.Add(new Objective($"{this.ContentCode}-{instanceId}", instanceId, health, effects));
    }
}