using System;
using System.Collections.Generic;
using System.Linq;
using GloomhavenTracker.Service.Models.Campaign;
using GloomhavenTracker.Service.Models.Content;
using GloomhavenTracker.Service.Repos;
using Microsoft.Extensions.Logging;

namespace GloomhavenTracker.Service.Services;

public interface CampaignService
{
    public List<CampaignSummary> GetCampaignList();
    public CampaignDTO GetCampaign(Guid campaignId);
    public CampaignSummary NewCampaign(Guid id, string game, string Description);
    public void UpdateCampaign(Guid campaignId, CampaignDTO campaign);
    // public CharacterDTO AddCharacterToCampaign(Guid campaignId, CharacterDTO character);
    // public CharacterDTO UpdateCharacter(Guid campaignId, CharacterDTO characterToUpdate);
    // public CharacterDTO GetCharacterForCampaign(Guid campaignId, string characterCode);
    // public List<CharacterDTO> GetCharactersForCampaign(Guid campaignId);
    // public ScenarioDTO AddScenarioToCampaign(Guid campaignId, ScenarioDTO scenarioToAdd);
    // public ScenarioDTO UpdateScenarioInCampaign(Guid campaignId, ScenarioDTO scenarioToAdd);
    // public List<ScenarioDTO> GetScenariosForCampaign(Guid campaignId);

}

public class CampaignServiceImplementation : CampaignService
{
    private Dictionary<Guid, Campaign> campaigns = new Dictionary<Guid, Campaign>();
    private ContentService contentService;
    private ILogger<CampaignServiceImplementation> logger;
    private CampaignRepo repo;

    public CampaignServiceImplementation(CampaignRepo repo, ContentService contentService, ILogger<CampaignServiceImplementation> logger)
    {
        this.logger = logger;
        this.contentService = contentService;
        this.repo = repo;
    }

    private Campaign GetCampaignById(Guid campaignId)
    {
        if (!campaigns.ContainsKey(campaignId))
        {
            CampaignDO? campaignFromRepo = repo.GetCampaign(campaignId);
            if (campaignFromRepo == null)
            {
                throw new ArgumentException("Could not find campaign with that id.");
            }
            else
            {
                campaigns.Add(campaignId, new Campaign(campaignFromRepo ?? new CampaignDO()));
            }
        }

        var campaign = campaigns.GetValueOrDefault(campaignId);
        if (campaign != null)
        {
            return campaign;
        }
        throw new ArgumentException("Could not find campaign with that id.");
    }


    public List<CampaignSummary> GetCampaignList()
    {
        return repo.GetCampaignList();
    }

    public CampaignDTO GetCampaign(Guid campaignId)
    {
        return GetCampaignById(campaignId).ToDTO();
    }


    public CampaignSummary NewCampaign(Guid id, string game, string Description)
    {

        CampaignDO newCampaignDO = new CampaignDO()
        {
            Game = GameUtils.GameTypeString(GameUtils.GameType(game)),
            Description = Description,
            Id = id.ToString(),
            Scenarios = new ScenariosDO() { Scenarios = new List<ScenarioDO>() },
            AvailableScenarios = new List<string>(),
            ClosedScenarios = new List<string>(),
            CompletedScenarios = new List<string>(),
            Party = new PartyDO() { Characters = new List<CharacterDO>() }
        };

        Campaign campaign = new Campaign(newCampaignDO);

        campaigns.Add(campaign.Id, campaign);

        repo.NewCampaign(campaign.ToDO());

        return campaign.Summary;
    }

    public void UpdateCampaign(Guid campaignId, CampaignDTO campaign)
    {
        ValidateCampaign(campaignId, campaign);
        ValidateCampaignCharacters(campaign);
        CampaignDO updatedCampaignDO = new CampaignDO()
        {
            Game = GameUtils.GameTypeString(GameUtils.GameType(campaign.Game)),
            Description = campaign.Description,
            Id = campaign.Id,
            Scenarios = new ScenariosDO() { 
                Scenarios =
                    campaign.Scenarios.Scenarios.Select(s => new ScenarioDO() {
                        ContentCode = s.ContentCode,
                        IsClosed = s.IsClosed,
                        IsCompleted = s.IsCompleted,
                    }).ToList()
            },
            Party = new PartyDO() {
                Characters = 
                    campaign.Party.Characters.Select(c => new CharacterDO() {
                        CharacterContentCode = c.CharacterContentCode,
                        AppliedPerks = c.AppliedPerks,
                        Experience = c.Experience,
                        Gold = c.Gold,
                        Items = c.Items,
                        Name = c.Name,
                        PerkPoints = c.PerkPoints
                    }).ToList()
            }
        };

        var updatedCampaign = new Campaign(updatedCampaignDO);
        campaigns[campaignId] = updatedCampaign;

        repo.UpdateCampaign(updatedCampaignDO);

    }

    public void ValidateCampaign(Guid campaignId, CampaignDTO campaign)
    {
        if(campaign.Id != campaignId.ToString()) throw new ArgumentException("Campaign Id Mismatch");
        var campaignToReplace = GetCampaignById(campaignId);
    }

    public void ValidateCampaignCharacters(CampaignDTO campaign)
    {
        campaign.Party.Characters.ForEach(character => {
            if(!contentService.IsValidCharacterCode(campaign.Game, character.CharacterContentCode))
                throw new ArgumentException("Character type is not valid for game");
        });

        if(campaign.Party.Characters.Select(c => c.CharacterContentCode).Distinct().Count() != campaign.Party.Characters.Count())
            throw new ArgumentException("Character appears more than once in a campaign");
    }

    public void ValidateCampaignScenarios(CampaignDTO campaign)
    {
        campaign.Scenarios.Scenarios.ForEach(scenario => {
            if(!contentService.IsValidScenarioCode(campaign.Game, scenario.ContentCode))
                throw new ArgumentException("Scenario type is not valid for game");
        });

        if(campaign.Scenarios.Scenarios.Select(c => c.ContentCode).Distinct().Count() != campaign.Scenarios.Scenarios.Count())
            throw new ArgumentException("Scenario appears more than once in a campaign");
    }


    // public CharacterDTO AddCharacterToCampaign(Guid campaignId, CharacterDTO newCharacter)
    // {
    //     Campaign campaign = GetCampaignById(campaignId);

    //     if(!contentService.IsValidCharacterCode(campaign.Game, newCharacter.CharacterContentCode))
    //         throw new ArgumentException("Character type is not valid for game");

    //     if(campaign.Party.Characters.ContainsKey(newCharacter.CharacterContentCode))
    //         throw new ArgumentException("Character already in campaign");

    //     CharacterDO characterDO = new CharacterDO() {
    //         Id = Guid.NewGuid().ToString(),
    //         AppliedPerks = newCharacter.AppliedPerks,
    //         CharacterContentCode = newCharacter.CharacterContentCode,
    //         Experience = newCharacter.Experience,
    //         Gold = newCharacter.Gold,
    //         Items = newCharacter.Items,
    //         Name = newCharacter.Name,
    //         PerkPoints = newCharacter.PerkPoints
    //     };

    //     campaign.Party.AddCharacter(characterDO);

    //     repo.UpdateCampaign(campaign.ToDO());

    //     var characterDTO = campaign.Party.Characters.GetValueOrDefault(newCharacter.CharacterContentCode)?.ToDTO();
    //     if(characterDTO == null)
    //         throw new Exception("could not find character");
    //     return characterDTO ?? new CharacterDTO();
    // }

    // public CharacterDTO UpdateCharacter(Guid campaignId, CharacterDTO characterToUpdate)
    // {
    //     Campaign campaign = GetCampaignById(campaignId);

    //     if(!contentService.IsValidCharacterCode(campaign.Game, characterToUpdate.CharacterContentCode))
    //         throw new ArgumentException("Character type is not valid for game");

    //     if(!campaign.Party.Characters.ContainsKey(characterToUpdate.CharacterContentCode))
    //         throw new ArgumentException("Character not in campaign");

    //     CharacterDO characterDO = new CharacterDO() {
    //         Id = Guid.NewGuid().ToString(),
    //         AppliedPerks = characterToUpdate.AppliedPerks,
    //         CharacterContentCode = characterToUpdate.CharacterContentCode,
    //         Experience = characterToUpdate.Experience,
    //         Gold = characterToUpdate.Gold,
    //         Items = characterToUpdate.Items,
    //         Name = characterToUpdate.Name,
    //         PerkPoints = characterToUpdate.PerkPoints
    //     };

    //     campaign.Party.UpdateCharacter(characterDO);

    //     repo.UpdateCampaign(campaign.ToDO());

    //     var characterDTO = campaign.Party.Characters.GetValueOrDefault(characterToUpdate.CharacterContentCode)?.ToDTO();
    //     if(characterDTO == null)
    //         throw new Exception("could not find character");
    //     return characterDTO ?? new CharacterDTO();

    // }

    // public CharacterDTO GetCharacterForCampaign(Guid campaignId, string characterCode)
    // {
    //     Campaign campaign = GetCampaignById(campaignId);
    //     return campaign.Party.Characters[characterCode].ToDTO();
    // }

    // public List<CharacterDTO> GetCharactersForCampaign(Guid campaignId)
    // {
    //     Campaign campaign = GetCampaignById(campaignId);
    //     return campaign.Party.ToDTO().Characters;
    // }


    // public ScenarioDTO AddScenarioToCampaign(Guid campaignId, ScenarioDTO scenarioToAdd)
    // {
    //     Campaign campaign = GetCampaignById(campaignId);

    //     if(!contentService.IsValidScenarioCode(campaign.Game, scenarioToAdd.ContentCode))
    //         throw new ArgumentException("Character type is not valid for game");

    //     return campaign.Scenarios.AddScenario(new Models.Campaign.Scenario(scenarioToAdd)).ToDTO();
    // }

    // public ScenarioDTO UpdateScenarioInCampaign(Guid campaignId, ScenarioDTO scenarioToAdd)
    // {
    //     Campaign campaign = GetCampaignById(campaignId);

    //     if(!contentService.IsValidScenarioCode(campaign.Game, scenarioToAdd.ContentCode))
    //         throw new ArgumentException("Character type is not valid for game");

    //     return campaign.Scenarios.UpdateScenario(new Models.Campaign.Scenario(scenarioToAdd)).ToDTO();
    // }

    // public List<ScenarioDTO> GetScenariosForCampaign(Guid campaignId)
    // {
    //     throw new NotImplementedException();
    // }

}