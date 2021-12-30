using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace GloomhavenTracker.Service.Models.Campaign;

public class Scenarios {
    private Dictionary<string, Scenario> scenarios;

    public Scenarios(ScenariosDTO scenarios)
    {
        this.scenarios = scenarios.Scenarios.ToDictionary(scn => scn.ContentCode, scn => new Scenario(scn));
    }
    public Scenarios(ScenariosDO scenarios)
    {
        this.scenarios = scenarios.Scenarios.ToDictionary(scn => scn.ContentCode, scn => new Scenario(scn));
    }

    public Scenario AddScenario(Scenario newScenario)
    {
        if(scenarios.ContainsKey(newScenario.ContentCode)) throw new ArgumentException("Scenario Already In Campaign");
        scenarios.Add(newScenario.ContentCode, newScenario);
        return scenarios[newScenario.ContentCode];
    }

    public Scenario UpdateScenario(Scenario updateScenario)
    {
        if(!scenarios.ContainsKey(updateScenario.ContentCode)) throw new ArgumentException("Scenario Not In Campaign");
        scenarios[updateScenario.ContentCode] = updateScenario;
        return scenarios[updateScenario.ContentCode];
    }

    public ScenariosDTO ToDTO()
    {
        return new ScenariosDTO(){
          Scenarios = this.scenarios.Select(kvp => kvp.Value.ToDTO()).ToList()
        };
    }

    public ScenariosDO ToDO()
    {
        return new ScenariosDO(){
          Scenarios = this.scenarios.Select(kvp => kvp.Value.ToDO()).ToList()
        };
    }

}

public struct ScenariosDTO {
    public List<ScenarioDTO> Scenarios {get; set;} = new List<ScenarioDTO>();
}

public struct ScenariosDO {
    public List<ScenarioDO> Scenarios {get; set;} = new List<ScenarioDO>();
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


public struct ScenarioDTO
{
    [JsonPropertyName("contentCode")]
    public string ContentCode {get; set;}
    
    [JsonPropertyName("isClosed")]
    public bool IsClosed {get; set;}

    [JsonPropertyName("isCompleted")]
    public bool IsCompleted {get; set;}
}

public struct ScenarioDO
{
    [JsonPropertyName("contentCode")]
    public string ContentCode {get; set;}
    
    [JsonPropertyName("isClosed")]
    public bool IsClosed {get; set;}

    [JsonPropertyName("isCompleted")]
    public bool IsCompleted {get; set;}
}