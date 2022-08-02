using System;
using System.Text;
using System.Threading.Tasks;
using GloomhavenTracker.Service.Models;
using GloomhavenTracker.Service.Models.Combat;
using GloomhavenTracker.Service.Services;
using Microsoft.AspNetCore.SignalR;

namespace GloomhavenTracker.Service.Hubs;


public class CombatHub : Hub
{
    private readonly CombatService service;
    public CombatHub(CombatService service) => this.service = service;

    public async Task JoinCombatSpace(Guid combatId)
    {
         if (service.CombatExists(combatId))
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, combatId.ToString());
            service.RegisterHubClientToCombat(combatId, Context.ConnectionId);
            await Clients.Caller.SendAsync(
                "joinCombatSpaceResult", 
                new HubRequestResult()
                {
                    data = combatId.ToString()
                }
            );
            await Clients.Group(combatId.ToString()).SendAsync(
                "joinCombatSpaceResult", 
                new HubRequestResult()
                {
                    data = "User Joined"
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
            service.RemoveHubClientFromCombat(combatId, Context.ConnectionId);
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

