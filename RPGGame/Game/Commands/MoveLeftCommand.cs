namespace RPGGame.Game
{
    public class MoveLeftCommand : Command, IMovementTypeObject
    {
        public MoveLeftCommand(Person person)
        {
            Direction = "Left";
            Animation = () => "WalkLeft";
            Condition = (key) => key is Key.A || (person.LastKey is Key.A && person.DirectionLatch);
            Action = () => person.MoveLeft();
        }
    }
}
