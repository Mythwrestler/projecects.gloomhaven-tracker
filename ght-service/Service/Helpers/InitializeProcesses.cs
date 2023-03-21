using GloomhavenTracker.Service.Services;

namespace GloomhavenTracker.Service.Helpers;

public static class InitializeProcesses 
{
    public static void LoadCombatHubTracker(IServiceProvider services) 
    {
        using IServiceScope startUpScope = services.CreateScope();
        try{
            var combatHubService = startUpScope.ServiceProvider.GetService<CombatService>();
            combatHubService?.SyncClients(30);
        }
        finally
        {
            startUpScope.Dispose();
        }
    }
}