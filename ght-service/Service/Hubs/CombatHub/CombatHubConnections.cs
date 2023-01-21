using GloomhavenTracker.Service.Models;
using GloomhavenTracker.Service.Models.Combat;
using Microsoft.AspNetCore.SignalR;

namespace GloomhavenTracker.Service.Hubs;

//[Authorize(Roles="user,superuser")]
public partial class CombatHub : Hub
{
    public async Task JoinCombat(Guid combatId)
    {

        try
        {
            combatService.RegisterClient(Context, combatId.ToString());
            await Groups.AddToGroupAsync(Context.ConnectionId, combatId.ToString());

            CombatDTO combat = combatService.GetCombatDTO(combatId);

            await Clients.Caller.SendAsync(
                "JoinCombatResult",
                new HubRequestResult()
                {
                    data = combat
                }
            );

            ParticipantsDTO participants = combatService.GetCombatParticipants(combatId);

            if(participants.Participants.Count() > 0)
            {
                await Clients.Group(combatId.ToString()).SendAsync(
                        "ActiveUsers",
                        new HubRequestResult()
                        {
                            data = participants
                        }
                );
            }
        }
        catch (Exception ex)
        {
            await Clients.Caller.SendAsync(
                "JoinCombatResult",
                new HubRequestResult()
                {
                    errorMessage = $"Error Joining Combat: {ex.Message}"
                }
            );
        }

    }


    public async Task LeaveCombat(Guid combatId)
    {
        try
        {
            string clientId = Context.ConnectionId;
            string groupId = combatId.ToString();

            combatService.UnregisterClient(clientId);
            await Groups.RemoveFromGroupAsync(clientId, groupId);

            await Clients.Caller.SendAsync(
                "LeaveCombatResult",
                new HubRequestResult()
                {
                    data = combatId.ToString()
                }
            );

            ParticipantsDTO participants = combatService.GetCombatParticipants(combatId);

            if(participants.Participants.Count() > 0)
            {
                await Clients.Group(combatId.ToString()).SendAsync(
                        "ActiveUsers",
                        new HubRequestResult()
                        {
                            data = participants
                        }
                );
            }
        }

        catch (Exception ex)
        {
            await Clients.Caller.SendAsync(
                "JoinCombatResult",
                new HubRequestResult()
                {
                    errorMessage = $"Error Joining Combat: {ex.Message}"
                }
            );
        }
    }
}

