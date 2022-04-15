using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using GloomhavenTracker.Service.Models.Content;

namespace GloomhavenTracker.Service.Models.Campaign;

public class Campaign
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Game Game { get; set;}
    public Dictionary<Guid, Scenario> Scenarios { get; set; } = new Dictionary<Guid, Scenario>();
    public Dictionary<Guid, Character> Party { get; set; } = new Dictionary<Guid, Character>();

    public CampaignSummary Summary => new CampaignSummary()
    {
        Id = this.Id.ToString(),
        Game = this.Game.ContentCode.ToString(),
        Name = this.Name
    };

    // public Campaign(CampaignDO campaign)
    // {
    //     this.Id = new Guid(campaign.Id);
    //     this.Description = campaign.Description;
    //     this.Game =  GameUtils.GameType(campaign.Game);
    //     this.Scenarios = new Scenarios(campaign.Scenarios);
    //     this.Party = new Party(campaign.Party);
    // }

    // public CampaignDO ToDO()
    // {
    //     return new CampaignDO()
    //     {
    //         Id = this.Id.ToString(),
    //         Description = this.Description,
    //         Game = GameUtils.GameTypeString(this.Game),
    //         Scenarios = this.Scenarios.ToDO(),
    //         Party = this.Party.ToDO()
    //     };
    // }
    // public CampaignDTO ToDTO()
    // {
    //     return new CampaignDTO()
    //     {
    //         Id = this.Id.ToString(),
    //         Name = this.Name,
    //         Game = this.Game.ContentCode,
    //         Scenarios = this.Scenarios.ToDTO(),
    //         Party = this.Party.ToDTO()
    //     };
    // }

}

// [Serializable]
// public struct CampaignDO
// {
//     [JsonPropertyName("id")]
//     public string Id {get; set;}

//     [JsonPropertyName("description")]
//     public string Description {get; set;}
    
//     [JsonPropertyName("game")]
//     public string Game {get; set;}
    
//     [JsonPropertyName("scenarios")]
//     public List<ScenarioDO> Scenarios {get; set;}

//     [JsonPropertyName("party")]
//     public List<CharacterDO> Party {get; set;}
// }

[Serializable]
public struct CampaignDTO
{
    [JsonPropertyName("id")]
    public string Id {get; set;}

    [JsonPropertyName("name")]
    public string Name {get; set;}
    
    [JsonPropertyName("game")]
    public string Game {get; set;}
    
    [JsonPropertyName("scenarios")]
    public List<ScenarioDTO> Scenarios {get; set;}

    [JsonPropertyName("party")]
    public List<CharacterDTO> Party {get; set;}
}


[Serializable]
public struct CampaignSummary
{
    [JsonPropertyName("id")]
    public string Id {get; set;}

    [JsonPropertyName("name")]
    public string Name {get; set;}

    [JsonPropertyName("game")]
    public string Game {get; set;}
}


[Serializable]
public struct NewCampaignRequestBody
{
    [JsonPropertyName("id")]
    public Guid Id {get; set;}

    [JsonPropertyName("name")]
    public string Name {get; set;}

    [JsonPropertyName("game")]
    public string Game {get; set;}
}