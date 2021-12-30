using System;
using System.Collections.Generic;
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
    public CharacterDTO AddCharacterToCampaign(Guid campaignId, CharacterDTO character);
    public CharacterDTO UpdateCharacter(Guid campaignId, CharacterDTO characterToUpdate);
    public CharacterDTO GetCharacterForCampaign(Guid campaignId, string characterCode);
    public List<CharacterDTO> GetCharactersForCampaign(Guid campaignId);
    public ScenarioDTO AddScenarioToCampaign(Guid campaignId, ScenarioDTO scenarioToAdd);
    public ScenarioDTO UpdateScenarioInCampaign(Guid campaignId, ScenarioDTO scenarioToAdd);
    public List<ScenarioDTO> GetScenariosForCampaign(Guid campaignId);

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

    public CharacterDTO AddCharacterToCampaign(Guid campaignId, CharacterDTO newCharacter)
    {
        Campaign campaign = GetCampaignById(campaignId);

        if(!contentService.IsValidCharacterCode(campaign.Game, newCharacter.CharacterContentCode))
            throw new ArgumentException("Character type is not valid for game");

        if(campaign.Party.Characters.ContainsKey(newCharacter.CharacterContentCode))
            throw new ArgumentException("Character already in campaign");

        CharacterDO characterDO = new CharacterDO() {
            Id = Guid.NewGuid().ToString(),
            AppliedPerks = newCharacter.AppliedPerks,
            CharacterContentCode = newCharacter.CharacterContentCode,
            Experience = newCharacter.Experience,
            Gold = newCharacter.Gold,
            Items = newCharacter.Items,
            Name = newCharacter.Name,
            PerkPoints = newCharacter.PerkPoints
        };

        campaign.Party.AddCharacter(characterDO);

        repo.UpdateCampaign(campaign.ToDO());

        var characterDTO = campaign.Party.Characters.GetValueOrDefault(newCharacter.CharacterContentCode)?.ToDTO();
        if(characterDTO == null)
            throw new Exception("could not find character");
        return characterDTO ?? new CharacterDTO();
    }

    public CharacterDTO UpdateCharacter(Guid campaignId, CharacterDTO characterToUpdate)
    {
        Campaign campaign = GetCampaignById(campaignId);

        if(!contentService.IsValidCharacterCode(campaign.Game, characterToUpdate.CharacterContentCode))
            throw new ArgumentException("Character type is not valid for game");

        if(!campaign.Party.Characters.ContainsKey(characterToUpdate.CharacterContentCode))
            throw new ArgumentException("Character not in campaign");

        CharacterDO characterDO = new CharacterDO() {
            Id = Guid.NewGuid().ToString(),
            AppliedPerks = characterToUpdate.AppliedPerks,
            CharacterContentCode = characterToUpdate.CharacterContentCode,
            Experience = characterToUpdate.Experience,
            Gold = characterToUpdate.Gold,
            Items = characterToUpdate.Items,
            Name = characterToUpdate.Name,
            PerkPoints = characterToUpdate.PerkPoints
        };

        campaign.Party.UpdateCharacter(characterDO);

        repo.UpdateCampaign(campaign.ToDO());

        var characterDTO = campaign.Party.Characters.GetValueOrDefault(characterToUpdate.CharacterContentCode)?.ToDTO();
        if(characterDTO == null)
            throw new Exception("could not find character");
        return characterDTO ?? new CharacterDTO();

    }

    public CharacterDTO GetCharacterForCampaign(Guid campaignId, string characterCode)
    {
        Campaign campaign = GetCampaignById(campaignId);
        return campaign.Party.Characters[characterCode].ToDTO();
    }

    public List<CharacterDTO> GetCharactersForCampaign(Guid campaignId)
    {
        Campaign campaign = GetCampaignById(campaignId);
        return campaign.Party.ToDTO().Characters;
    }


    public ScenarioDTO AddScenarioToCampaign(Guid campaignId, ScenarioDTO scenarioToAdd)
    {
        Campaign campaign = GetCampaignById(campaignId);

        if(!contentService.IsValidScenarioCode(campaign.Game, scenarioToAdd.ContentCode))
            throw new ArgumentException("Character type is not valid for game");

        return campaign.Scenarios.AddScenario(new Models.Campaign.Scenario(scenarioToAdd)).ToDTO();
    }

    public ScenarioDTO UpdateScenarioInCampaign(Guid campaignId, ScenarioDTO scenarioToAdd)
    {
        Campaign campaign = GetCampaignById(campaignId);

        if(!contentService.IsValidScenarioCode(campaign.Game, scenarioToAdd.ContentCode))
            throw new ArgumentException("Character type is not valid for game");

        return campaign.Scenarios.UpdateScenario(new Models.Campaign.Scenario(scenarioToAdd)).ToDTO();
    }

    public List<ScenarioDTO> GetScenariosForCampaign(Guid campaignId)
    {
        throw new NotImplementedException();
    }

}