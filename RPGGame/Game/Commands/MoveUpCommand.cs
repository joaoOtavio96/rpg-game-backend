namespace RPGGame.Game
{
    public class MoveUpCommand : Command, IMovementTypeObject
    {
        public MoveUpCommand(Person person)
        {
            Direction = "Up";
            Animation = () => "WalkUp";
            Condition = (key) => key is Key.W || (person.LastKey is Key.W && person.DirectionLatch);
            Action = () => person.MoveUp();
        }

        public string Direction { get; set; }
        public Func<string> Animation { get; set; }
    }
}
