using System;
using System.Collections.Generic;
using GloomhavenTracker.Service.Models.Campaign;
using GloomhavenTracker.Service.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GloomhavenTracker.Service.Controllers;


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



    [Route("new")]
    [HttpPost]
    [Consumes("application/json")]
    public IActionResult CreateNewCampaign([FromBody] NewCampaignRequestBody body)
    {
        try
        {
            var campaign = service.NewCampaign(
                body.Id,
                body.Game,
                body.Description
            );
            return Ok(campaign);
        }
        catch (Exception ex)
        {
            logger.LogError(new EventId(), ex, "Failed to create new campaign");
            return UnprocessableEntity("Issue processing request");
        }
    }

    [Route("{campaignId}/characters")]
    [HttpPost]
    public IActionResult AddCharacterToCampaign(Guid campaignId, [FromBody] CharacterDTO characterForAdd)
    {
        try
        {
            var characterForReturn = service.AddCharacterToCampaign(campaignId, characterForAdd);
            return Ok(characterForReturn);
        }
        catch (Exception ex)
        {
            logger.LogError(new EventId(), ex, "Failed to add a character to the campaign");
            return UnprocessableEntity("Issue processing request");
        }
    }

    [Route("{campaignId}/characters/{characterCode}")]
    [HttpPut]
    public IActionResult UpdateCharacterInCampaign(Guid campaignId, string characterCode, [FromBody] CharacterDTO characterForAdd)
    {
        try
        {
            var characterForReturn = service.UpdateCharacter(campaignId, characterForAdd);
            return Ok(characterForReturn);
        }
        catch (Exception ex)
        {
            logger.LogError(new EventId(), ex, "Failed to update a character in the campaign");
            return UnprocessableEntity("Issue processing request");
        }
    }
    

    
    [Route("{campaignId}/characters")]
    [HttpGet]
    public IActionResult GetCharacterForCampaign(Guid campaignId, string characterCode)
    {
        try
        {
            var characterForReturn = service.GetCharacterForCampaign(campaignId, characterCode);
            return Ok(characterForReturn);
        }
        catch (Exception ex)
        {
            logger.LogError(new EventId(), ex, "Failed to get character for the campaign");
            return UnprocessableEntity("Issue processing request");
        }
    }


    [Route("{campaignId}/characters")]
    [HttpGet]
    public IActionResult GetCharactersForCampaign(Guid campaignId)
    {
        try
        {
            var charactersForReturn = service.GetCharactersForCampaign(campaignId);
            return Ok(charactersForReturn);
        }
        catch (Exception ex)
        {
            logger.LogError(new EventId(), ex, "Failed to get characters for the campaign");
            return UnprocessableEntity("Issue processing request");
        }
    }


    [Route("{campaignId}/scenarios")]
    [HttpPut]
    public IActionResult AddScenarioToCampaign(Guid campaignId, [FromBody] ScenarioDTO scenarioForAdd)
    {
        try
        {
            var scenarioForReturn = service.AddScenarioToCampaign(campaignId, scenarioForAdd);
            return Ok(scenarioForReturn);
        }
        catch (Exception ex)
        {
            logger.LogError(new EventId(), ex, "Failed to add scenario to the campaign");
            return UnprocessableEntity("Issue processing request");
        }
    }
    
    [Route("{campaignId}/scenarios/{scenarioCode}")]
    [HttpPut]
    public IActionResult UpdateScenarioInCampaign(Guid campaignId, string scenarioCode, [FromBody] ScenarioDTO scenarioForUpdate)
    {
        try
        {
            if(scenarioCode != scenarioForUpdate.ContentCode) throw new ArgumentException("Content Code Mismatch between request and body.");
            var scenarioForReturn = service.UpdateScenarioInCampaign(campaignId, scenarioForUpdate);
            return Ok(scenarioForReturn);
        }
        catch (Exception ex)
        {
            logger.LogError(new EventId(), ex, "Failed to add scenario to the campaign");
            return UnprocessableEntity("Issue processing request");
        }
    }

    [Route("{campaignId}/scenarios")]
    [HttpGet]
    public IActionResult GetScenariosForCampaign(Guid campaignId)
    {
        
        try
        {
            throw new NotImplementedException();
        }
        catch (Exception ex)
        {
            logger.LogError(new EventId(), ex, "Failed to get scenarios for the campaign");
            return UnprocessableEntity("Issue processing request");
        }
    }





}