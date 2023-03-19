using GloomhavenTracker.Service.Models;
using GloomhavenTracker.Service.Models.Combat.Hub;
using Microsoft.AspNetCore.SignalR;

namespace GloomhavenTracker.Service.Hubs;

public partial class CombatHub : Hub
{
    public async Task RegisterCharacters(List<string> characterContentCodes)
    {
        try
        {
            HubClient client = combatService.GetRegisteredClient(Context);
            Guid combatId = new Guid(client.GroupId);

            if (!combatService.CombatExists(combatId))
                throw new ArgumentException("Combat Does not exist");

            var participants = combatService.RegisterCharacterSelections(client.ClientId, characterContentCodes);

            if (participants.Participants.Count() > 0)
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
                "RegisterCharacterResult",
                new HubRequestResult()
                {
                    errorMessage = $"Error Saving Character Selection: {ex.Message}"
                }
            );
        }
    }


    public async Task RegisterObserver()
    {
        try
        {
            HubClient client = combatService.GetRegisteredClient(Context);
            Guid combatId = new Guid(client.GroupId);

            if (!combatService.CombatExists(combatId))
                throw new ArgumentException("Combat Does not exist");

            var participants = combatService.RegisterAsObserver(client.ClientId);

            if (participants.Participants.Count() > 0)
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
                "RegisterObserverResult",
                new HubRequestResult()
                {
                    errorMessage = $"Error Joining as Observer: {ex.Message}"
                }
            );
        }
    }
}