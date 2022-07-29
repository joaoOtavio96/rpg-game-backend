using Microsoft.AspNetCore.SignalR;
using RPGGame.Game;

namespace RPGGame.Hubs
{
    public class GameHub : Hub
    {
        private readonly CommandProcessor _commands;

        public GameHub(CommandProcessor commands)
        {
            _commands = commands;
        }

        public Task KeyPressed(string key)
        {
            _commands.AddKey(key);

            return Task.CompletedTask;
        }
    }
}
