namespace RPGGame.Game
{
    public abstract class Command
    {
        public Key KeyToProccess { get; set; }
        public Func<Key, bool> Condition { get; set; }
        public Action Action { get; set; }

        public abstract IGameObject NextPosition();
        public abstract IGameObject CurrentState();
    }
}
