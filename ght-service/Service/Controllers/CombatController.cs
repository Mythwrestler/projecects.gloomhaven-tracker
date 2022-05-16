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

    [Route("new")]
    [HttpPost]
    public IActionResult CreateCombat([FromBody] NewCombatTracker body)
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

    [Route("")]
    [HttpGet]
    public IActionResult GetCombatListing()
    {
        try
        {
            var combatListing = combatService.GetCombatList();
            return Ok(combatListing);
        }
        catch (Exception ex)
        {
            logger.LogError(new EventId(), ex, "Failed to create a combat.");
            return UnprocessableEntity("Issue processing request");
        }
    }


    [Route("{combatId}")]
    [HttpGet]
    public IActionResult GetCombatById(Guid combatId)
    {
        try
        {
            var combat = combatService.GetCombatDTO(combatId);
            return Ok(combat);
        }
        catch (Exception ex)
        {
            logger.LogError(new EventId(), ex, "Failed to create a combat.");
            return UnprocessableEntity("Issue processing request");
        }
    }


    // [Route("{combatId}")]
    // [HttpGet]
    // public IActionResult GetCombat(Guid combatId)
    // {
    //     var combat = _service.GetCombat(combatId);
    //     var result = new JsonResult(combat);
    //     result.StatusCode = 200;
    //     return result;
    // }

    // [Route("{combatId}/battle-action")]
    // [HttpPatch]
    // public IActionResult TakeBattleAction(Guid combatId, [FromBody] CombatAction action)
    // {
    //     var result = _service.ProcessCombatAction(combatId, action);
    //     _hubContext.Clients.Group(combatId.ToString()).SendAsync("battleActionTaken", result);
    //     return new OkResult();
    // }


    // [Route("{combatId}/new-round")]
    // [HttpPost]
    // public IActionResult NewRound(Guid combatId)
    // {
    //     var combat = _service.NextRound(combatId);
    //     var result = new JsonResult(combat);
    //     result.StatusCode = 200;
    //     _hubContext.Clients.All.SendAsync("newRound", combat);
    //     return result;
    // }

    // [Route("{combatId}/actors")]
    // [HttpPost]
    // public IActionResult AddActors(Guid combatId, [FromBody]ActorsDTO actors)
    // {
    //     var combat = _service.AddActors(combatId, actors);
    //     var result = new JsonResult(combat.Actors);
    //     result.StatusCode = 200;
    //     return result;
    // }

    // [Route("{combatId}/actors/player/{playerId}/initiative")]
    // [HttpPost]
    // public IActionResult SetInitiative(Guid combatId, Guid playerId, [FromQuery] bool isLongRest = false, [FromQuery] int initiative = 0)
    // {
    //     var combat = _service.ProcessActorInitiative(
    //         combatId,
    //         new CombatInitiative{
    //             PlayerId=playerId,
    //             IsLongRest=isLongRest,
    //             Initiative=initiative
    //         }
    //     );
    //     var result = new JsonResult(combat);
    //     result.StatusCode = 200;
    //     return result;
    // }

    // [Route("{combatId}/actors/monster/{monsterType}/initiative")]
    // [HttpPost]
    // public IActionResult SetInitiative(Guid combatId, string monsterType, [FromQuery] int initiative)
    // {
    //     var combat = _service.ProcessActorInitiative(
    //         combatId,
    //         new CombatInitiative
    //         {
    //             MonsterType=monsterType,
    //             Initiative=initiative
    //         }
    //     );
    //     var result = new JsonResult(combat);
    //     result.StatusCode = 200;
    //     return result;
    // }

}

