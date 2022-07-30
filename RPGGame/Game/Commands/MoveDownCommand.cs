namespace RPGGame.Game
{
    public class MoveDownCommand : Command, IMovementTypeObject
    {
        public MoveDownCommand(Person person)
        {
            Direction = "Down";
            Animation = () => "WalkDown";
            Condition = (key) => key is Key.S || (person.LastKey is Key.S && person.DirectionLatch);
            Action = () => person.MoveDown();
        }

        public string Direction { get; set; }
        public Func<string> Animation { get; set; }
    }
}
