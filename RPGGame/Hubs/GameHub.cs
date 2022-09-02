using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using RPGGame.Config;
using RPGGame.Game;

namespace RPGGame.Hubs
{
    public class GameHub : Hub
    {
        private readonly MainCommandQueue _commands;
        private readonly GameConfig _gameConfig;

        public GameHub(MainCommandQueue commands, IOptions<GameConfig> gameConfigOption)
        {
            _commands = commands;
            _gameConfig = gameConfigOption.Value;
        }

        public Task KeyPressed(string key)
        {
            _commands.AddKey(key);

            return Task.CompletedTask;
        }

        public Task KeyReleased()
        {
            _commands.ClearKeys();

            return Task.CompletedTask;
        }

        public GameConfig Init()
        {
            return _gameConfig;
        }
    }
}
