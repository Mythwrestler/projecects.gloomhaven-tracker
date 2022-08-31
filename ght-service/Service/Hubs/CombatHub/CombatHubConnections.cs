using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GloomhavenTracker.Service.Models;
using GloomhavenTracker.Service.Models.Hub;
using Microsoft.AspNetCore.Connections.Features;
using Microsoft.AspNetCore.SignalR;

namespace GloomhavenTracker.Service.Hubs;

//[Authorize(Roles="user,superuser")]
public partial class CombatHub : Hub
{
    public async Task JoinCombat(Guid combatId)
    {
        if (combatService.CombatExists(combatId))
        {
            if (Context.UserIdentifier != null)
            {
                Guid userId = Guid.Parse(Context.UserIdentifier);
                User user = userService.GetUserById(userId);

                string clientId = Context.ConnectionId;
                string groupId = combatId.ToString();

                await Groups.AddToGroupAsync(clientId, groupId);
                hubClientTracker.RegisterClient(groupId, clientId, user);

                var heartbeat = Context.Features.Get<IConnectionHeartbeatFeature>();
                if (heartbeat is not null)
                {
                    heartbeat.OnHeartbeat((state) =>
                    {
                        var clientIdForHeartbeat = (state as string);
                        if (string.IsNullOrEmpty(clientIdForHeartbeat)) return;
                        hubClientTracker.UpdateLastSeen(clientIdForHeartbeat as string);
                    }, clientId);
                }


                await Clients.Caller.SendAsync(
                    "JoinCombatResult",
                    new HubRequestResult()
                    {
                        data = combatId.ToString()
                    }
                );

                List<HubClient> registeredClients = hubClientTracker.GetClientsForGroup(combatId.ToString());

                await Clients.Group(combatId.ToString()).SendAsync(
                    "UserJoinedCombat",
                    new HubRequestResult()
                    {
                        data = registeredClients
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
        if (combatService.CombatExists(combatId))
        {
            string clientId = Context.ConnectionId;
            string groupId = combatId.ToString();

            await Groups.RemoveFromGroupAsync(clientId, groupId);
            hubClientTracker.UnregisterClient(clientId);
            await Clients.Caller.SendAsync(
                "LeaveCombatResult",
                new HubRequestResult()
                {
                    data = combatId.ToString()
                }
            );

            var registeredClients = hubClientTracker.GetClientsForGroup(groupId);
            if (registeredClients.Count > 0)
            {
                await Clients.Group(combatId.ToString()).SendAsync(
                     "UserLeftCombat",
                     new HubRequestResult()
                     {
                         data = registeredClients
                     }
                );
            }
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

