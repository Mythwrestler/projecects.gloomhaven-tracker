using System.Threading.Tasks;
using System.Text;
using GloomhavenTracker.Service.Models;
using GloomhavenTracker.Service.Services;
using Microsoft.AspNetCore.SignalR;
using System;

namespace GloomhavenTracker.Service.Hubs
{

    public class CombatHub : Hub
    {
        private readonly ICombatService _service;
        public CombatHub(ICombatService service) => _service = service;

        public async Task CombatAction(CombatAction action)
        {   
            await Clients.All.SendAsync("battleActionReceived", new CombatActionResult());
        }

        public async Task JoinCombatSpace(Guid combatId)
        {
            if(_service.CombatExists(combatId))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, combatId.ToString());
                _service.RegisterHubClient(combatId, Context.ConnectionId);
                await Clients.Caller.SendAsync("joinedCombat", combatId.ToString());
            }
            await Clients.Caller.SendAsync("hubRequestFailed", $"Could Not Find Combat Id {combatId.ToString()}");
        }

        public async Task LeaveCombatSpace(Guid combatId)
        {
            if(_service.CombatExists(combatId))
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, combatId.ToString());
                _service.RemoveHubClient(combatId, Context.ConnectionId);
            }
            await Clients.Caller.SendAsync("hubRequestFailed", $"Could Not Find Combat Id {combatId.ToString()}");
        }

    }

}