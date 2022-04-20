using System;
using GloomhavenTracker.Service.Models.Campaign;
using GloomhavenTracker.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GloomhavenTracker.Service.Controllers;

[Authorize(Policy = "authenticated")]
[Authorize(Policy = "superuser")]
[Route("api/campaigns")]
public class CampaignController : Controller
{
    private CampaignService service;
    private ILogger<CampaignController> logger;

    public CampaignController(CampaignService service, ILogger<CampaignController> logger)
    {
        this.service = service;
        this.logger = logger;
    }

    [HttpGet]
    public IActionResult GetListOfAvailableCampaigns()
    {
        try
        {
            var campaigns = service.GetCampaignList();
            return Ok(campaigns);
        }
        catch (Exception ex)
        {
            logger.LogError(new EventId(), ex, "Failed to get list of campaigns");
            return UnprocessableEntity("Issue processing request");
        }
    }

    [HttpPost]
    [Consumes("application/json")]
    public IActionResult CreateNewCampaign([FromBody] NewCampaignRequestBody requestBody)
    {
        try
        {
            var campaign = service.NewCampaign(requestBody);
            return Ok(campaign);
        }
        catch (Exception ex)
        {
            logger.LogError(new EventId(), ex, "Failed to create new campaign");
            return UnprocessableEntity("Issue processing request");
        }
    }

    [Route("{campaignId}")]
    [HttpGet]
    public IActionResult GetCampaign(Guid campaignId)
    {
        try
        {
            var campaign = service.GetCampaign(campaignId);
            return Ok(campaign);
        }
        catch (Exception ex)
        {
            logger.LogError(new EventId(), ex, $"Failed to get campaign {campaignId}");
            return UnprocessableEntity("Issue processing request");
        }
    }

    [Route("{campaignId}/characters")]
    [HttpPut]
    [Consumes("application/json")]
    public IActionResult AddCharacterToCampaign(Guid campaignId, [FromBody] CharacterRequestBody newCharacterRequest)
    {
        try
        {
            var character = service.AddCharacterToCampaign(campaignId, newCharacterRequest);
            return Ok(character);
        }
        catch (Exception ex)
        {
            logger.LogError(new EventId(), ex, $"Failed to get list of chracters for campaign {campaignId}");
            return UnprocessableEntity("Issue processing request");
        }
    }

    [Route("{campaignId}/characters/{characterContentCode}")]
    [HttpGet]
    public IActionResult GetCharacter(Guid campaignId, string characterContentCode)
    {
        try
        {
            var character = service.GetCharacterFromCampaign(campaignId, characterContentCode);
            return Ok(character);
        }
        catch (Exception ex)
        {
            logger.LogError(new EventId(), ex, $"Failed to get character {characterContentCode}");
            return UnprocessableEntity("Issue processing request");
        }
    }
    
    [Route("{campaignId}/characters/{characterContentCode}")]
    [HttpPatch]
    [Consumes("application/json")]
    public IActionResult UpdateCharacterInCampaign(Guid campaignId, string characterContentCode, [FromBody] CharacterRequestBody updateCharacterRequest)
    {
        try
        {
            var character = service.UpdateCharacterInCampaign(campaignId, characterContentCode, updateCharacterRequest);
            return Ok(character);
        }
        catch (Exception ex)
        {
            logger.LogError(new EventId(), ex, $"Failed to update character {characterContentCode} in {campaignId}");
            return UnprocessableEntity("Issue processing request");
        }
    }

    [Route("{campaignId}/scenarios/")]
    [HttpPut]
    public IActionResult AddScenarioToCampaign(Guid campaignId, string scenarioContentCode, [FromBody] ScenarioRequestBody newScenarioRequest)
    {
        try
        {
            var scenario = service.AddScenarioToCampaign(campaignId, newScenarioRequest);
            return Ok(scenario);
        }
        catch (Exception ex)
        {
            logger.LogError(new EventId(), ex, $"Failed to update scenario {scenarioContentCode} in {campaignId}");
            return UnprocessableEntity("Issue processing request");
        }
    }

    [Route("{campaignId}/scenarios/{scenarioContentCode}")]
    [HttpGet]
    public IActionResult GetScenario(Guid campaignId, string scenarioContentCode)
    {
        try
        {
            var scenario = service.GetScenarioFromCampaign(campaignId, scenarioContentCode);
            return Ok(scenario);
        }
        catch (Exception ex)
        {
            logger.LogError(new EventId(), ex, $"Failed to get scenario {scenarioContentCode} in {campaignId}");
            return UnprocessableEntity("Issue processing request");
        }
    }

    [Route("{campaignId}/scenarios/{scenarioContentCode}")]
    [HttpPatch]
    public IActionResult UpdateScenario(Guid campaignId, string scenarioContentCode, [FromBody] ScenarioRequestBody scenarioUpdateRequest)
    {
        try
        {
            var scenario = service.UpdateScenarioInCampaign(campaignId, scenarioContentCode, scenarioUpdateRequest);
            return Ok(scenario);
        }
        catch (Exception ex)
        {
            logger.LogError(new EventId(), ex, $"Failed to update scenario {scenarioContentCode} in {campaignId}");
            return UnprocessableEntity("Issue processing request");
        }
    }
}