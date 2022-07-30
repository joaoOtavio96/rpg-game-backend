namespace RPGGame.Game
{
    public class MoveRightCommand : Command, IMovementTypeObject
    {
        public MoveRightCommand(Person person)
        {
            Direction = "Right";
            Animation = () => "WalkRight";
            Condition = (key) => key is Key.D || (person.LastKey is Key.D && person.DirectionLatch);
            Action = () => person.MoveRight();
        }

        public string Direction { get; set; }
        public Func<string> Animation { get; set; }
    }
}
