using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace GloomhavenTracker.Service.Models.Campaign;

public class Scenarios {
    private Dictionary<string, Scenario> ScenarioDictionary;

    public Scenarios(List<ScenarioDO> scenarios) {
        this.ScenarioDictionary = scenarios.ToDictionary(scn => scn.ContentCode, scn => new Scenario(scn));
    }
    
    public Scenarios(List<ScenarioDTO> scenarios) {
        this.ScenarioDictionary = scenarios.ToDictionary(scn => scn.ContentCode, scn => new Scenario(scn));
    }

    public Scenario AddScenario(Scenario newScenario)
    {
        if(ScenarioDictionary.ContainsKey(newScenario.ContentCode)) throw new ArgumentException("Scenario Already In Campaign");
        ScenarioDictionary.Add(newScenario.ContentCode, newScenario);
        return ScenarioDictionary[newScenario.ContentCode];
    }

    public Scenario UpdateScenario(Scenario updateScenario)
    {
        if(!ScenarioDictionary.ContainsKey(updateScenario.ContentCode)) throw new ArgumentException("Scenario Not In Campaign");
        ScenarioDictionary[updateScenario.ContentCode] = updateScenario;
        return ScenarioDictionary[updateScenario.ContentCode];
    }

    public List<ScenarioDTO> ToDTO()
    {
        return this.ScenarioDictionary.Select(kvp => kvp.Value.ToDTO()).ToList();
    }

    public List<ScenarioDO> ToDO()
    {
        return this.ScenarioDictionary.Select(kvp => kvp.Value.ToDO()).ToList();
    }
}


public class Scenario {
    public string ContentCode {get;}
    public bool IsClosed {get;}
    public bool IsCompleted {get;}

    public Scenario(ScenarioDTO scenario)
    {
        this.ContentCode = scenario.ContentCode;
        this.IsClosed = scenario.IsClosed;
        this.IsCompleted = scenario.IsCompleted;
    }

    public Scenario(ScenarioDO scenario)
    {
        this.ContentCode = scenario.ContentCode;
        this.IsClosed = scenario.IsClosed;
        this.IsCompleted = scenario.IsCompleted;
    }

    public ScenarioDTO ToDTO()
    {
        return new ScenarioDTO()
        {
            ContentCode = this.ContentCode,
            IsClosed = this.IsClosed,
            IsCompleted = this.IsCompleted,
        };
    }
    public ScenarioDO ToDO()
    {
        return new ScenarioDO()
        {
            ContentCode = this.ContentCode,
            IsClosed = this.IsClosed,
            IsCompleted = this.IsCompleted,
        };
    }

}


[Serializable]
public struct ScenarioDTO
{
    [JsonPropertyName("contentCode")]
    public string ContentCode {get; set;}
    
    [JsonPropertyName("isClosed")]
    public bool IsClosed {get; set;}

    [JsonPropertyName("isCompleted")]
    public bool IsCompleted {get; set;}
}

[Serializable]
public struct ScenarioDO
{
    [JsonPropertyName("contentCode")]
    public string ContentCode {get; set;}
    
    [JsonPropertyName("isClosed")]
    public bool IsClosed {get; set;}

    [JsonPropertyName("isCompleted")]
    public bool IsCompleted {get; set;}
}