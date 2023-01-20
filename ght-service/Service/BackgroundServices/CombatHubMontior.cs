using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GloomhavenTracker.Service.Hubs;
using GloomhavenTracker.Service.Models;
using GloomhavenTracker.Service.Models.Combat;
using GloomhavenTracker.Service.Models.Combat.Hub;
using GloomhavenTracker.Service.Services;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GloomhavenTracker.Service.BackgroundServices
{
    public class CombatHubMonitor : IHostedService, IDisposable
    {
        private readonly IHubContext<CombatHub> combatHubContext;
        private readonly IServiceScopeFactory scopeFactory;
        private const int SyncIntervalSeconds = 10;
        private Timer? combatHubClientAuditTimer;
        private Boolean syncInProgress = false;

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

        private async void DoWork(object? state)
        {
            if (syncInProgress) return;
            syncInProgress = true;
            try
            {
                using var scope = scopeFactory.CreateScope();
                CombatService? service = scope.ServiceProvider.GetService<CombatService>();
                if (service is null) return;

                await Task.Run(() => service.SyncClients(SyncIntervalSeconds * 2));

                service.GetGroupIds().ForEach(async (group) =>
                {
                    ParticipantsDTO participants = service.GetCombatParticipants(new Guid(group));
                    if (participants.Participants.Count() > 0)
                        await combatHubContext.Clients.Group(group).SendAsync(
                            "ActiveUsers",
                            new HubRequestResult()
                            {
                                data = participants
                            }
                        );
                });
            }
            finally
            {
                syncInProgress = false;
            }
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
    }
}