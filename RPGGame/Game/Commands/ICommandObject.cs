namespace RPGGame.Game
{
    public interface ICommandObject
    {
        public CommandMap CommandMap { get; set; }
        void OnProccessing(Command command, Action completed);
    }
}
