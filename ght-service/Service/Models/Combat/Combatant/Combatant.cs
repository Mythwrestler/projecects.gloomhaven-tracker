using System;
using System.Text.Json.Serialization;

namespace GloomhavenTracker.Service.Models.Combat.Combatant;

public class Combatant {

    public Guid Id { get;}
    public int Level { get; }
    public int Health { get; }
    public int? Initiative { get; }

    public Combatant (Guid id, int level, int health, int? initiative)
    {
        Id = id;
        Level = level;
        Health = health;
        Initiative = initiative;
    }
}

[Serializable]
public class CombatantDTO
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("level")]
    public int Level { get; set; }

    [JsonPropertyName("health")]
    public int Health { get; set; }

    [JsonPropertyName("initiative")]
    public int? Initiative { get; set; }
}