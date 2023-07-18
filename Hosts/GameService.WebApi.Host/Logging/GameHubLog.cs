using GameService.WebApi.Endpoints.Hubs;
using Microsoft.AspNetCore.SignalR;
using Serilog.Events;
using Serilog.Sinks.PeriodicBatching;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace GameService.WebApi.Host.Logging
{
    public class GameHubLog : PeriodicBatchingSink
    {
        private readonly IHubContext<GameHub> _hubContext;

        public GameHubLog(IHubContext<GameHub> hubContext) : base(batchSizeLimit: 100, TimeSpan.FromSeconds(5))
        {
            _hubContext = hubContext ?? throw new ArgumentNullException(nameof(hubContext));
        }

        protected override async Task EmitBatchAsync(IEnumerable<LogEvent> events)
        {
            foreach (var logEvent in events)
            {
                var message = logEvent.RenderMessage();
                await _hubContext.Clients.All.SendAsync("ReceiveLogEvent", message);
            }
        }
    }
}
