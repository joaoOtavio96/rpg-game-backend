using Microsoft.AspNetCore.SignalR;
using RPGGame.Game;

namespace RPGGame.Hubs
{
    public class GameHub : Hub
    {
        private readonly CommandQueue _commands;

        public GameHub(CommandQueue commands)
        {
            _commands = commands;
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
    }
}
