using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GloomhavenTracker.Service.Hubs;
using GloomhavenTracker.Service.Models;
using GloomhavenTracker.Service.Models.Hub;
using GloomhavenTracker.Service.Services;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GloomhavenTracker.Service.BackgroundServices
{
    public class CombatHubMonitor : IHostedService, IDisposable
    {
        private readonly IHubContext<CombatHub> combatHubContext;
        private readonly CombatService combatService;
        private readonly IServiceScopeFactory scopeFactory;
        private const int SyncIntervalSeconds = 30;
        private Timer? combatHubClientAuditTimer;

        public CombatHubMonitor(IHubContext<CombatHub> context, IServiceScopeFactory scopeFactory)
        {
            this.combatHubContext = context;
            this.scopeFactory = scopeFactory;
        }

       public Task StartAsync(CancellationToken stoppingToken)
        {
            combatHubClientAuditTimer = new Timer(
                DoWork,
                null,
                TimeSpan.Zero,
                TimeSpan.FromSeconds(SyncIntervalSeconds));

            return Task.CompletedTask;
        }

        private async void DoWork(object state)
        {
            using var scope = scopeFactory.CreateScope();
            CombatService? service = scope.ServiceProvider.GetService<CombatService>();
            CombatHubClientTracker? clientTracker = scope.ServiceProvider.GetService<CombatHubClientTracker>();
            if(service is null || clientTracker is null) return;

            service.SyncClients();

            clientTracker.HubGroups.ForEach(async group => {
                await combatHubContext.Clients.Group(group).SendAsync(
                    "ActiveUsers",
                     new HubRequestResult()
                     {
                         data = clientTracker.GetClientsForGroup(group)
                     }
                );
            });

            // using var scope = _serviceScopeFactory.CreateScope();
            // var scopedProcessingService = 
            //     scope.ServiceProvider.GetRequiredService<IPresenceDatabaseSyncer>();

            // await scopedProcessingService.UpdateConnectionsOnlineStatus()
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            combatHubClientAuditTimer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            combatHubClientAuditTimer?.Dispose();
        }

        // protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        // {
        //     using var scope = scopeFactory.CreateScope();
        //     CombatService? service = scope.ServiceProvider.GetService<CombatService>();
        //     if (service is null) return;

        //     while (!stoppingToken.IsCancellationRequested)
        //     {
        //         await combatHubContext.Clients.All.SendAsync("serverMessage", $"Its Been 5 Seconds... Time for another message from the server. messageId: {Guid.NewGuid()}");

        //         await CleanUpRegisteredClients(service);

        //         await Task.Delay(5000);
        //     }
        // }

        // public async Task CleanUpRegisteredClients(CombatService service)
        // {
        //     var clientsToUpdate = service.CleanUpOldClients();
        //     clientsToUpdate.Select(kvp => kvp.Key).ToList().ForEach(async combatId =>
        //     {
        //         List<User> clients;
        //         clients = service.GetHubUsersForCombat(combatId).Select(kvp => kvp.Value).ToList();
        //         await combatHubContext.Clients.Group(combatId.ToString()).SendAsync(
        //          "UserLeftCombat",
        //          new HubRequestResult()
        //          {
        //              data = clients
        //          }
        //     );
        //     });

        // }



    }

}