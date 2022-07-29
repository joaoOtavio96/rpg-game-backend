using Microsoft.AspNetCore.SignalR;
using RPGGame.Game;
using RPGGame.Hubs;
using RPGGame.Infrastructure;

namespace RPGGame.HostedService
{
    public class GameHostedService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IGame _game;
        private readonly TimeStep _timeStep;

        public GameHostedService(IServiceProvider serviceProvider, IGame game)
        {
            _serviceProvider = serviceProvider;
            _game = game;
            _timeStep = new TimeStep();

            _game.Init();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Update(stoppingToken);
        }

        private async Task Update(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _timeStep.NextTime();
                _game.Update(_timeStep.DeltaTime);
                await UpdateScreen(_game.GetState());
                await Task.Delay(TimeSpan.FromMilliseconds(1));
            }
        }
        private async Task UpdateScreen(object state)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var hubContext = scope.ServiceProvider.GetRequiredService<IHubContext<GameHub>>();
                await hubContext.Clients.All.SendAsync("Update", state);
            }
        }
    }
}
