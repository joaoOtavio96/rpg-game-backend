namespace RPGGame.Game
{
    public interface ICommandObject
    {
        public CommandKeyMap CommandMap { get; set; }
        void OnProccessing(CommandIntent command, Action completed);
        public bool DirectionLatch { get; set; }
    }
}
