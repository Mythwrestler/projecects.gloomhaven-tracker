using System.Threading.Tasks;
using GloomhavenTracker.Service.Models;
using Microsoft.AspNetCore.SignalR;

namespace GloomhavenTracker.Service.Hubs;

public class BattleHub : Hub
{
    public async Task TakeBattleAction(string action)
    {
        await Clients.All.SendAsync("battleActionReceived", action);
    }
}
