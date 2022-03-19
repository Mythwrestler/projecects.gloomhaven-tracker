using System;
using System.Text;
using System.Threading.Tasks;
using GloomhavenTracker.Service.Models.Combat;
using GloomhavenTracker.Service.Models;
using GloomhavenTracker.Service.Services;
using Microsoft.AspNetCore.SignalR;

namespace GloomhavenTracker.Service.Hubs
{

    public class CombatHub : Hub
    {
        private readonly CombatService service;
        public CombatHub(CombatService service) => this.service = service;

        public async Task CombatAction(CombatAction action)
        {
            await Clients.All.SendAsync("battleActionReceived", new CombatActionResult());
        }

        public async Task JoinCombatSpace(Guid combatId)
        {
            if (service.CombatExists(combatId))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, combatId.ToString());
                service.RegisterHubClient(combatId, Context.ConnectionId);
                await Clients.Caller.SendAsync(
                    "joinCombatSpaceResult", 
                    new HubRequestResult()
                    {
                        data = combatId.ToString()
                    }
                );
            } else {
                await Clients.Caller.SendAsync(
                    "joinCombatSpaceResult",
                    new HubRequestResult()
                    {
                        errorMessage = $"Could Not Find Combat Id {combatId.ToString()}"
                    }
                );
            }
        }

        public async Task LeaveCombatSpace(Guid combatId)
        {
            if (service.CombatExists(combatId))
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, combatId.ToString());
                service.RemoveHubClient(combatId, Context.ConnectionId);
                await Clients.Caller.SendAsync(
                    "leaveCombatSpaceResult", 
                    new HubRequestResult()
                    {
                        data = combatId.ToString()
                    }
                );
            }
            else
            {
                await Clients.Caller.SendAsync(
                    "leaveCombatSpaceResult",
                    new HubRequestResult()
                    {
                        errorMessage = $"Could Not Find Combat Id {combatId.ToString()}"
                    }
                );
            }
        }

    }

}