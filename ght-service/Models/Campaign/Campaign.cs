using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using GloomhavenTracker.Service.Models.Content;

namespace GloomhavenTracker.Service.Models.Campaign;

public class Campaign
{
    public Guid Id {get;}
    public string Description {get;}
    public string Game {get;}
    public Scenarios Scenarios {get;}
    public Party Party {get;}

    public CampaignSummary Summary => new CampaignSummary()
    {
        Id = this.Id.ToString(),
        Game = this.Game.ToString(),
        Description = this.Description
    };

    public Campaign(CampaignDO campaign)
    {
        this.Id = new Guid(campaign.Id);
        this.Description = campaign.Description;
        this.Game = campaign.Game;
        this.Scenarios = new Scenarios(campaign.Scenarios);
        this.Party = new Party(campaign.Party);
    }

    public CampaignDO ToDO()
    {
        return new CampaignDO()
        {
            Id = this.Id.ToString(),
            Description = this.Description,
            Game = this.Game,
            Scenarios = this.Scenarios.ToDO(),
            Party = this.Party.ToDO()
        };
    }
    public CampaignDTO ToDTO()
    {
        return new CampaignDTO()
        {
            Id = this.Id.ToString(),
            Description = this.Description,
            Game = this.Game,
            Scenarios = this.Scenarios.ToDTO(),
            Party = this.Party.ToDTO()
        };
    }

}

[Serializable]
public struct CampaignDO
{
    [JsonPropertyName("id")]
    public string Id {get; set;}

    [JsonPropertyName("description")]
    public string Description {get; set;}
    
    [JsonPropertyName("game")]
    public string Game {get; set;}
    
    [JsonPropertyName("scenarios")]
    public ScenariosDO Scenarios {get; set;}

    [JsonPropertyName("completedScenarios")]
    public List<string> CompletedScenarios {get; set;}

    [JsonPropertyName("closedScenarios")]
    public List<string> ClosedScenarios {get; set;}

    [JsonPropertyName("availableScenarios")]
    public List<string> AvailableScenarios {get; set;}

    [JsonPropertyName("party")]
    public PartyDO Party {get; set;}
}

[Serializable]
public struct CampaignDTO
{
    [JsonPropertyName("id")]
    public string Id {get; set;}

    [JsonPropertyName("description")]
    public string Description {get; set;}
    
    [JsonPropertyName("game")]
    public string Game {get; set;}
    
    [JsonPropertyName("scenarios")]
    public ScenariosDTO Scenarios {get; set;}

    [JsonPropertyName("party")]
    public PartyDTO Party {get; set;}
}


[Serializable]
public struct CampaignSummary
{
    [JsonPropertyName("id")]
    public string Id {get; set;}

    [JsonPropertyName("description")]
    public string Description {get; set;}

    [JsonPropertyName("game")]
    public string Game {get; set;}
}


[Serializable]
public struct NewCampaignRequestBody
{
    [JsonPropertyName("id")]
    public Guid Id {get; set;}

    [JsonPropertyName("description")]
    public string Description {get; set;}

    [JsonPropertyName("game")]
    public string Game {get; set;}
}