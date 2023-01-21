using GloomhavenTracker.Service.Models.Combat.Hub;
using Microsoft.AspNetCore.SignalR;

namespace GloomhavenTracker.Service.Hubs;

public partial class CombatHub : Hub
{
  // public async Task RegisterCharacter (List<string> characterContentCodes)
  // {
  //   try
  //   {
  //     HubClient client = combatService.GetRegisteredClient(Context);
  //     Guid combatId = new Guid(client.GroupId);

  //     if(!combatService.CombatExists(combatId))
  //       throw new ArgumentException("Combat Does not exist");

      

  //   }
  //   catch (Exception ex)
  //   {

  //   }
  // }
}