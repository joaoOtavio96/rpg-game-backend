namespace RPGGame.Game
{
    public interface IMovementTypeObject
    {
        public string Direction { get; set; }
        public Func<string> Animation { get; set; }
    }
}
