using System;
using System.Threading;
using System.Threading.Tasks;
using GloomhavenTracker.Service.Hubs;
using GloomhavenTracker.Service.Services;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GloomhavenTracker.Service.BackgroundServices
{
    public class BattleHubMonitor : BackgroundService
    {
        private readonly IHubContext<CombatHub> _context;

        private readonly IServiceScopeFactory scopeFactory;

        public BattleHubMonitor(IHubContext<CombatHub> context, IServiceScopeFactory  scopeFactory)
        {
            _context = context;
            this.scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var scope = scopeFactory.CreateScope();
            var service = scope.ServiceProvider.GetService<CombatService>();

            while (!stoppingToken.IsCancellationRequested)
            {
                await _context.Clients.All.SendAsync("serverMessage", $"Its Been 5 Seconds... Time for another message from the server. messageId: {Guid.NewGuid()}");

                await Task.Delay(5000);
            }
        }
    }
    
}