using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace GloomhavenTracker.Service.Models.Combat;


[Serializable]
public class CombatantInitiativeDTO
{
    [JsonPropertyName("combatantCode")]
    public string CombatantCode { get; set; } = string.Empty;

    [JsonPropertyName("initiative")]
    public int Initiative { get; set; } = -1;

    [JsonPropertyName("order")]
    public int Order { get; set; } = 99;
}

[Serializable]
public class CombatantInitiativeDO
{
    [JsonPropertyName("combatantCode")]
    public string CombatantCode { get; set; } = string.Empty;

    [JsonPropertyName("initiative")]
    public int Initiative { get; set; } = 99;

    [JsonPropertyName("contentCode")]
    public string ContentCode { get; set; } = string.Empty;

    [JsonPropertyName("instanceNumber")]
    public int InstanceNumber { get; set; } = 0;

    [JsonPropertyName("isElite")]
    public bool IsElite { get; set; } = false;
}

public class CombatantInitiative
{
    public string CombatantCode { get; } = string.Empty;
    public int Initiative { get; set; } = 99;
    public string ContentCode { get; } = string.Empty;
    public int InstanceNumber { get; } = 0;
    public bool IsElite { get; } = false;
    public CombatantInitiative(CombatantInitiativeDO initiative)
    {
        this.CombatantCode = initiative.CombatantCode;
        this.Initiative = initiative.Initiative;
        this.ContentCode = initiative.ContentCode;
        this.InstanceNumber = initiative.InstanceNumber;
        this.IsElite = initiative.IsElite;
    }
    public CombatantInitiative(string combatantCode, int initiative, Guid contentId, int instanceNumber, bool isElite)
    {
        this.CombatantCode = combatantCode;
        this.Initiative = initiative;
        this.ContentCode = ContentCode;
        this.InstanceNumber = instanceNumber;
        this.IsElite = isElite;
    }
    public CombatantInitiativeDO ToDO()
    {
        return new CombatantInitiativeDO()
        {
            CombatantCode = this.CombatantCode,
            Initiative = this.Initiative,
            ContentCode = this.ContentCode,
            InstanceNumber = this.InstanceNumber,
            IsElite = this.IsElite
        };
    }


}

public class InitiativeDTO
{
    [JsonPropertyName("turnOrder")]
    public List<CombatantInitiativeDTO>? TurnOrder = new List<CombatantInitiativeDTO>();

    [JsonPropertyName("currentCombatant")]
    public string CurrentCombatant = string.Empty;
}

[Serializable]
public class InitiativeDO
{
    [JsonPropertyName("combatants")]
    public List<CombatantInitiativeDO> Combatants { get; set; } = new List<CombatantInitiativeDO>();

    [JsonPropertyName("completedCombatants")]
    public List<string> CompletedCombatants { get; set; } = new List<string>();

    [JsonPropertyName("currentCombatant")]
    public string CurrentCombatant { get; set; } = string.Empty;
}


public class Initiative
{
    private readonly List<CombatantInitiative> combatants;
    private readonly Dictionary<int, string> turnOrder;
    private int currentCombatant;
    public int CurrentCombatant => currentCombatant;

    public Initiative(InitiativeDO initiative)
    {
        this.combatants = initiative.Combatants.Select(combatant => new CombatantInitiative(combatant)).ToList();
        
        if(String.IsNullOrEmpty(initiative.CurrentCombatant))
        {
            turnOrder = new Dictionary<int, string>();
            ClearInitiative();
        }
        else
        {
        var order = 1;
        var turnOrder = new Dictionary<int, string>();
        combatants
            .Where(combatant => !initiative.CompletedCombatants.Contains(combatant.CombatantCode) && combatant.CombatantCode != initiative.CurrentCombatant)
            .OrderBy(combatant => combatant.Initiative).ThenBy(combatant => combatant.ContentCode).ThenBy(combatant => combatant.IsElite).ThenBy(combatant => combatant.InstanceNumber)
            .ToList().ForEach(combatant =>
            {
                turnOrder.Add(order, combatant.CombatantCode);
                order++;
            });

        turnOrder.Add(order, initiative.CurrentCombatant);
        currentCombatant = order;
        order++;

        combatants
            .Where(combatant => initiative.CompletedCombatants.Contains(combatant.CombatantCode))
            .OrderBy(combatant => combatant.Initiative).ThenBy(combatant => combatant.ContentCode).ThenBy(combatant => combatant.IsElite).ThenBy(combatant => combatant.InstanceNumber)
            .ToList().ForEach(combatant =>
            {
                turnOrder.Add(order, combatant.CombatantCode);
                order++;
            });

        this.turnOrder = turnOrder;
        }
    }

    public bool AddCombatant(string combatantCode, int initiative, Guid contentId, int instanceNumber, bool isElite)
    {
        var combatant = new CombatantInitiative(combatantCode, initiative, contentId, instanceNumber, isElite);
        CalculateInitiative();
        return true;
    }

    public bool RemoveCombatant(string combatantCode)
    {
        var combatant = combatants.Find(combatant => combatant.CombatantCode == combatantCode);
        if (combatant != null) {
            var order = turnOrder.Where(kvp => kvp.Value == combatantCode).Select(kvp => kvp.Key).FirstOrDefault(-1);
            if(currentCombatant == order) currentCombatant = -1;
            combatants.Remove(combatant);
        }
        CalculateInitiative();
        return true;
    }

    private bool ClearInitiative()
    {
        combatants.ForEach(combatant => combatant.Initiative = -1);
        currentCombatant = -1;
        turnOrder.Clear();
        return true;
    }

    private void CalculateInitiative()
    {
        // Do Not Calculate if any combatants do not have an initiative defined.
        if (combatants.Any(combatant => combatant.Initiative == -1) || currentCombatant == -1) return;
        
        // Get List of Completed Combatants
        var completedCombatants = turnOrder
            // Only Completed Combatants
            .Where(kvp => kvp.Key < currentCombatant)
            // The Combatant Code Values
            .Select(kvp => kvp.Value)
            // Only combatants still in the combatants list
            .Where(code => combatants.Any(combatant => combatant.CombatantCode == code))
            .ToList();

        // Get Current Combatant Code (Note: can be not found if current combatant is dead)
        var currentCombatantCode = turnOrder.GetValueOrDefault(currentCombatant, string.Empty);

        // Clear Turn Order
        turnOrder.Clear();
        var order = 1;

        // Process Completed Combatants
        if(completedCombatants.Count > 0)
        {
            combatants
                .Where(combatant => completedCombatants.Contains(combatant.CombatantCode))
                .OrderBy(combatant => combatant.Initiative).ThenBy(combatant => combatant.ContentCode).ThenBy(combatant => combatant.IsElite).ThenBy(combatant => combatant.InstanceNumber)
                .ToList().ForEach(combatant =>
                {
                    turnOrder.Add(order, combatant.CombatantCode);
                    order++;
                });
        }

        // Load Current Combatant
        if(!String.IsNullOrEmpty(currentCombatantCode))
        {
            turnOrder.Add(order, currentCombatantCode);
            currentCombatant = order;
            order++;
        }

        // Load Remaining Combatants
        combatants
            .Where(combatant => !completedCombatants.Contains(combatant.CombatantCode) && combatant.CombatantCode != currentCombatantCode)
            .OrderBy(combatant => combatant.Initiative).ThenBy(combatant => combatant.ContentCode).ThenBy(combatant => combatant.IsElite).ThenBy(combatant => combatant.InstanceNumber)
            .ToList().ForEach(combatant =>
            {
                turnOrder.Add(order, combatant.CombatantCode);
                order++;
            });
    }

    public bool SetInitiative(string contentCode, int initiative)
    {
        combatants.Where(combatant => combatant.ContentCode == contentCode).ToList().ForEach(combatant => combatant.Initiative = initiative);
        CalculateInitiative();
        return true;
    }

    public InitiativeDO ToDO()
    {
        return new InitiativeDO()
        {
            Combatants = this.combatants.Select(combatant => combatant.ToDO()).ToList(),
            CompletedCombatants = this.turnOrder.Where(kvp => kvp.Key < currentCombatant).Select(kvp => kvp.Value).ToList(),
            CurrentCombatant = turnOrder[currentCombatant]
        };
    }

    public InitiativeDTO TurnOrder
    {
        get
        {
            List<CombatantInitiativeDTO> turnOrder = combatants
                .Select(combatant => {
                    return new CombatantInitiativeDTO()
                    {
                        CombatantCode = combatant.CombatantCode,
                        Initiative = combatant.Initiative,
                        Order = this.turnOrder.Where(kvp => kvp.Value == combatant.CombatantCode).First().Key
                    };
                })
                .ToList();

            return new InitiativeDTO()
            {
                TurnOrder = turnOrder,
                CurrentCombatant = this.currentCombatant == -1 ? string.Empty : this.turnOrder[currentCombatant]
            };
        }
    }
}