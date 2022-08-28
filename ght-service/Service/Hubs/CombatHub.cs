using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GloomhavenTracker.Service.Models;
using GloomhavenTracker.Service.Models.Combat;
using GloomhavenTracker.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace GloomhavenTracker.Service.Hubs;

//[Authorize(Roles="user,superuser")]
public class CombatHub : Hub
{
    private readonly CombatService service;
    public CombatHub(CombatService service) => this.service = service;

    public async Task JoinCombat(Guid combatId)
    {
        if (service.CombatExists(combatId))
        {
            if (Context.UserIdentifier != null)
            {
                var userId = new Guid(Context.UserIdentifier);
                await Groups.AddToGroupAsync(Context.ConnectionId, combatId.ToString());
                var registeredClients = service.RegisterHubClientToCombat(combatId, Context.ConnectionId, userId);
                await Clients.Caller.SendAsync(
                    "JoinCombatResult",
                    new HubRequestResult()
                    {
                        data = combatId.ToString()
                    }
                );
                await Clients.Group(combatId.ToString()).SendAsync(
                    "UserJoinedCombat",
                    new HubRequestResult()
                    {
                        data = registeredClients.Select(kvp => kvp.Value).ToList()
                    }
                );
            }
        }
        else
        {
            await Clients.Caller.SendAsync(
                "JoinCombatResult",
                new HubRequestResult()
                {
                    errorMessage = $"Could Not Find Combat Id {combatId.ToString()}"
                }
            );
        }
    }

    public async Task LeaveCombat(Guid combatId)
    {
        if (service.CombatExists(combatId))
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, combatId.ToString());
            var registeredClients = service.RemoveHubClientFromCombat(combatId, Context.ConnectionId);
            await Clients.Caller.SendAsync(
                "LeaveCombatResult",
                new HubRequestResult()
                {
                    data = combatId.ToString()
                }
            );
            await Clients.Group(combatId.ToString()).SendAsync(
                 "UserLeftCombat",
                 new HubRequestResult()
                 {
                     data = registeredClients.Select(kvp => kvp.Value).ToList()
                 }
            );
        }
        else
        {
            await Clients.Caller.SendAsync(
                "LeaveCombatResult",
                new HubRequestResult()
                {
                    errorMessage = $"Could Not Find Combat Id {combatId.ToString()}"
                }
            );
        }
    }

}

