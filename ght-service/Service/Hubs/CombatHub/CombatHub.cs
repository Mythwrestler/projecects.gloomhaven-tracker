using GloomhavenTracker.Service.Services;
using Microsoft.AspNetCore.SignalR;
using GloomhavenTracker.Service.Models.Combat.Hub;

namespace GloomhavenTracker.Service.Hubs;

public partial class CombatHub : Hub
{
    private readonly CombatService combatService;
    public CombatHub(CombatService combatService, CombatHubClientTracker hubClientTracker, UserService userService)
    {
        this.combatService = combatService;
    }
}