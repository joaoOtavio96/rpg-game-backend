namespace RPGGame.Game
{
    public abstract class Command
    {
        public string Direction { get; set; }
        public Func<string> Animation { get; set; }
        public Key KeyToProccess { get; set; }
        public Func<Key, bool> Condition { get; set; }
        public Action Action { get; set; }
    }
}
