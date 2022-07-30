namespace RPGGame.Game
{
    public interface ICommandObject
    {
        public CommandKeyMap CommandMap { get; set; }
        void OnProccessing(Command command, Action completed);
    }
}
