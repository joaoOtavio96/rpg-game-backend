namespace RPGGame.Game
{
    public abstract class CommandIntent
    {
        public IGameObject GameObject { get; set; }
        public Key KeyToProccess { get; set; }
        public Func<Key, bool> Condition { get; set; }
        public Action Action { get; set; }

        public abstract IGameObject NextPosition();
    }
}
