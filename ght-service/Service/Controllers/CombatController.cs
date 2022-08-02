using System;
using GloomhavenTracker.Service.Hubs;
using GloomhavenTracker.Service.Models.Combat;
using GloomhavenTracker.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace GloomhavenTracker.Service.Controllers;

[Authorize(Roles="user,superuser")]
[Route("api/combats")]
public class CombatController : Controller
{
    private readonly CombatService combatService;
    private readonly ILogger<CombatController> logger;

    public CombatController(CombatService combatService, ILogger<CombatController> logger)
    {
        this.combatService = combatService;
        this.logger = logger;
    }

    [HttpPost]
    public IActionResult CreateCombat([FromBody] NewCombatRequest body)
    {
        try
        {
            var combat = combatService.NewCombat(body.CampaignId, body.ScenarioContentCode);
            return Ok(combat);
        }
        catch (Exception ex)
        {
            logger.LogError(new EventId(), ex, "Failed to create a combat.");
            return UnprocessableEntity("Issue processing request");
        }
    }

    [Route("{combatId}")]
    [HttpGet]
    public IActionResult GetCombat(Guid combatId)
    {
        try
        {
            var combat = combatService.GetCombatDTO(combatId);
            return Ok(combat);
        }
        catch (Exception ex)
        {
            logger.LogError(new EventId(), ex, "Failed to get combat information.");
            return UnprocessableEntity("Issue processing request");
        }
    }

    [Route("")]
    [HttpGet]
    public IActionResult GetCombatListing()
    {
        try
        {
            var combatListing = combatService.GetCombatListing();
            return Ok(combatListing);
        }
        catch (Exception ex)
        {
            logger.LogError(new EventId(), ex, "Failed to get combat listing.");
            return UnprocessableEntity("Issue processing request");
        }
    }
}

