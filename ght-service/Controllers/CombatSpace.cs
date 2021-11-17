using GloomhavenTracker.Service.Services;
using GloomhavenTracker.Service.Models;
using Microsoft.AspNetCore.Mvc;
using GloomhavenTracker.Service.Hubs;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;

namespace GloomhavenTracker.Service.Controllers
{
    [Route("api/combatspace")]
    public class CombatSpaceController : Controller
    {
        private readonly CombatService _service;
        private readonly IHubContext<CombatHub> _hubContext;

        public CombatSpaceController(CombatService service, IHubContext<CombatHub> hubContext) {
            _service = service;
            _hubContext = hubContext;
        }

        // [Route("new")]
        // [HttpPost]
        // public IActionResult CreateCombat([FromBody] NewCombatTrackerDescription body)
        // {
        //     var combat = _service.NewCombat(body.GameCode, body.ScenarioCode, body.Description);
        //     var result = new JsonResult(combat);
        //     result.StatusCode = 200;
        //     return result;
        // }

        // [Route("")]
        // [HttpGet]
        // public IActionResult GetCombatListing([FromQuery] string? scenarioCode)
        // {
        //     List<CombatTrackerSummaryDTO> listing;
        //     if(scenarioCode == null)
        //     {
        //         listing = _service.GetCombatList();
        //     }
        //     else
        //     {
        //         listing = _service.GetCombatListForScenario(scenarioCode ?? "");
        //     }

        //     var result = new JsonResult(listing);
        //     result.StatusCode = 200;
        //     return result;
        // }

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
    
}