using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using GloomhavenTracker.Service.Models.Campaign;
using GloomhavenTracker.Service.Models.Content;
using GloomhavenTracker.Service.Repos;
using Microsoft.Extensions.Logging;

namespace GloomhavenTracker.Service.Services;

public interface CampaignService
{
    public List<CampaignSummary> GetCampaignList();
    public CampaignDTO NewCampaign(NewCampaignRequestBody requestBody);
    public CampaignDTO GetCampaign(Guid campaignId);
    public CharacterDTO AddCharacterToCampaign(Guid campaignId, CharacterRequestBody newCharacterRequest);
    public CharacterDTO GetCharacterFromCampaign(Guid campaignId, string characterContentCode);
    public CharacterDTO UpdateCharacterInCampaign(Guid campaignId, string characterContentCode, CharacterRequestBody updateCharacterRequest);
    public ScenarioDTO AddScenarioToCampaign(Guid campaignId, ScenarioRequestBody newScenarioRequest);
    public ScenarioDTO GetScenarioFromCampaign(Guid campaignId, string scenarioContentCode);
    public ScenarioDTO UpdateScenarioInCampaign(Guid campaignId, string scenarioContentCode, ScenarioRequestBody updateScenarioRequest);
}

public class CampaignServiceImplementation : CampaignService
{
    private readonly CampaignRepo repo;
    private readonly ContentService contentService;
    private readonly IMapper mapper;
    private readonly ILogger<CampaignServiceImplementation> logger;

    public CampaignServiceImplementation(CampaignRepo repo, ContentService contentService, IMapper mapper, ILogger<CampaignServiceImplementation> logger)
    {
        this.logger = logger;
        this.repo = repo;
        this.mapper = mapper;
        this.contentService = contentService;
    }

    private Campaign GetCampaignById(Guid campaignId)
    {
        return repo.GetCampaign(campaignId);
    }

    public List<CampaignSummary> GetCampaignList()
    {
        List<Campaign> campaigns = repo.GetCampaignList();
        return mapper.Map<List<CampaignSummary>>(campaigns);
    }

    public CampaignDTO GetCampaign(Guid campaignId)
    {
        Campaign campaign = GetCampaignById(campaignId);
        return mapper.Map<CampaignDTO>(campaign);
    }

    public CampaignDTO NewCampaign(NewCampaignRequestBody requestBody)
    {

        Models.Content.Game game = contentService.GetGameDefaults(
            GameUtils.GameType(requestBody.GameContentCode)
        );

        var campaign = new Campaign
        (
            Guid.NewGuid(),
            requestBody.Name,
            requestBody.Description,
            game,
            new Dictionary<string, Models.Campaign.Scenario>(),
            new Dictionary<string, Models.Campaign.Character>()
        );

        var savedCampaign = repo.SaveCampaign(campaign);

        return mapper.Map<CampaignDTO>(savedCampaign);

    }

    public CharacterDTO AddCharacterToCampaign(Guid campaignId, CharacterRequestBody newCharacterRequest)
    {

        if (newCharacterRequest.CharacterContentCode is null)
            throw new ArgumentException("Character Content Code is in null");

        if (newCharacterRequest.Name is null)
            throw new ArgumentException("Character Name is in null");

        if (newCharacterRequest.Experience is null)
            throw new ArgumentException("Character Experience is in null");

        if (newCharacterRequest.Gold is null)
            throw new ArgumentException("Character Gold is in null");

        if (newCharacterRequest.PerkPoints is null)
            throw new ArgumentException("Character Perk Points is in null");

        var campaign = GetCampaignById(campaignId);

        if (campaign.Party.ContainsKey(newCharacterRequest.CharacterContentCode))
            throw new ArgumentException("Character Content Code is in use");

        var characterContent = contentService.GetCharacterDefaults(GameUtils.GameType(campaign.Game.ContentCode), newCharacterRequest.CharacterContentCode);

        var character = new Models.Campaign.Character(
            id: Guid.NewGuid(),
            newCharacterRequest.Name,
            characterContent: characterContent,
            experience: newCharacterRequest.Experience ?? 0,
            gold: newCharacterRequest.Gold ?? 0,
            perkPoints: newCharacterRequest.PerkPoints ?? 0
        );

        var savedCampaign = repo.CreateCharacterForCampaign(campaignId, character);

        var savedCharacter = savedCampaign.Party
            .Select(kvp => kvp.Value)
            .Where(chr => chr.CharacterContent.ContentCode == newCharacterRequest.CharacterContentCode)
            .First();

        return mapper.Map<CharacterDTO>(character);
    }

    public CharacterDTO GetCharacterFromCampaign(Guid campaignId, string characterContentCode)
    {
        var campaign = GetCampaignById(campaignId);

        var character = campaign.Party.GetValueOrDefault(characterContentCode);

        if (character is null)
            throw new ArgumentException("Character content was not found");

        return mapper.Map<CharacterDTO>(character);
    }

    public CharacterDTO UpdateCharacterInCampaign(Guid campaignId, string characterContentCode, CharacterRequestBody updateCharacterRequest)
    {

        if (updateCharacterRequest.CharacterContentCode is not null)
            throw new ArgumentException("Character Content Code cannot be updated");

        if (updateCharacterRequest.Name is not null)
            throw new ArgumentException("Character Name cannot be updated");

        var campaign = GetCampaignById(campaignId);

        var character = campaign.Party.GetValueOrDefault(characterContentCode);

        if (character is null)
            throw new ArgumentException("Character Content was not found");

        character.Experience = updateCharacterRequest.Experience ?? character.Experience;
        character.Gold = updateCharacterRequest.Gold ?? character.Gold;
        character.PerkPoints = updateCharacterRequest.PerkPoints ?? character.PerkPoints;

        var savedCampaign = repo.UpdateCharacterForCampaign(campaignId, character);

        var savedCharacter = savedCampaign.Party
            .Select(kvp => kvp.Value)
            .Where(chr => chr.Id == character.Id)
            .First();

        return mapper.Map<CharacterDTO>(character);
    }

    public ScenarioDTO AddScenarioToCampaign(Guid campaignId, ScenarioRequestBody newScenarioRequest)
    {

        if (newScenarioRequest.ScenarioContentCode is null)
            throw new ArgumentException("Scenario Content Code is in null");

        if (newScenarioRequest.isClosed is null)
            throw new ArgumentException("Scenario isClosed is in null");

        if (newScenarioRequest.isCompleted is null)
            throw new ArgumentException("Scenario isCompleted is in null");

        var campaign = GetCampaignById(campaignId);

        if (campaign.Scenarios.ContainsKey(newScenarioRequest.ScenarioContentCode))
            throw new ArgumentException("Scenario Content Code is in use");

        var scenarioContent = contentService.GetScenarioDefaults(GameUtils.GameType(campaign.Game.ContentCode), newScenarioRequest.ScenarioContentCode);

        var scenario = new Models.Campaign.Scenario(
            Guid.NewGuid(),
            scenarioContent,
            newScenarioRequest.isClosed ?? false,
            newScenarioRequest.isCompleted ?? false
        );

        var savedCampaign = repo.CreateScenarioForCampaign(campaignId, scenario);

        var savedScenario = savedCampaign.Scenarios
            .Select(kvp => kvp.Value)
            .Where(scn => scn.Id == scenario.Id)
            .First();

        return mapper.Map<ScenarioDTO>(savedScenario);
    }

    public ScenarioDTO GetScenarioFromCampaign(Guid campaignId, string scenarioContentCode)
    {
        var campaign = GetCampaignById(campaignId);

        var scenario = campaign.Scenarios.GetValueOrDefault(scenarioContentCode);

        if (scenario is null)
            throw new ArgumentException("Scenario content code was not found");

        return mapper.Map<ScenarioDTO>(scenario);
    }

    public ScenarioDTO UpdateScenarioInCampaign(Guid campaignId, string scenarioContentCode, ScenarioRequestBody updateScenarioRequest)
    {

        if (updateScenarioRequest.ScenarioContentCode is not null)
            throw new ArgumentException("Scenario Content Code cannot be updated");

        var campaign = GetCampaignById(campaignId);

        var scenario = campaign.Scenarios.GetValueOrDefault(scenarioContentCode);

        if (scenario is null)
            throw new ArgumentException("Character Content was not found");

        scenario.IsClosed = updateScenarioRequest.isClosed ?? scenario.IsClosed;
        scenario.IsCompleted = updateScenarioRequest.isCompleted ?? scenario.IsCompleted;

        var savedCampaign = repo.UpdateScenarioForCampaign(campaignId, scenario);

        var savedScenario = savedCampaign.Scenarios
            .Select(kvp => kvp.Value)
            .Where(chr => chr.Id == scenario.Id)
            .First();

        return mapper.Map<ScenarioDTO>(savedScenario);
    }
}