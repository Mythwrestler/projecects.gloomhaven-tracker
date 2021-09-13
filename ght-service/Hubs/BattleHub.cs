using System.Threading.Tasks;
using GloomhavenTracker.Service.Models;
using GloomhavenTracker.Service.Services;
using Microsoft.AspNetCore.SignalR;

namespace GloomhavenTracker.Service.Hubs
{

    public class BattleHub : Hub
    {
        // private IBattleService _service;

        // public BattleHub (IBattleService service) => _service = service;

        public async Task TakeBattleAction(BattleAction action)
        {
            // Battle battle = _service.GetBattle();
            BattleActionResult result = new BattleActionResult(){
                Affect = action.Damage.ToString(),
                Affected = action.Target.ToString()
                };
            await Clients.All.SendAsync("battleActionReceived", result);
        }

    }


}