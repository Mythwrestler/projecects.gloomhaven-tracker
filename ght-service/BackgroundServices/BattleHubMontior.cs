using System;
using System.Threading;
using System.Threading.Tasks;
using GloomhavenTracker.Service.Hubs;
using GloomhavenTracker.Service.Services;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;

namespace GloomhavenTracker.Service.BackgroundServices
{
    public class BattleHubMonitor : BackgroundService
    {
        private readonly IHubContext<CombatHub> _context;

        private readonly ICombatService3 _service;

        public BattleHubMonitor(IHubContext<CombatHub> context, ICombatService3 service)
        {
            _context = context;
            _service = service;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
             while (!stoppingToken.IsCancellationRequested)
            {
                await _context.Clients.All.SendAsync("serverMessage", $"Its Been 5 Seconds... Time for another message from the server. messageId: {Guid.NewGuid()}");

                await Task.Delay(5000);
            }
        }
    }
    
}