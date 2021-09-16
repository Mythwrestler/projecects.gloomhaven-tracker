using GloomhavenTracker.Service.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace GloomhavenTracker.Service.BackgroundServices
{
    public class BattleHubMonitor : BackgroundService
    {
        private readonly IHubContext<BattleHub> _context;

        private readonly ILogger<BattleHubMonitor> _logger;

        public BattleHubMonitor(IHubContext<BattleHub> context, ILogger<BattleHubMonitor> logger)
        {
            _context = context;
            _logger = logger;
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