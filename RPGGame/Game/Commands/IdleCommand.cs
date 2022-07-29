namespace RPGGame.Game
{
    public class IdleCommand : Command, IMovementTypeObject
    {
        public IdleCommand(Person person)
        {
            Animation = () => $"Idle{person.LastDirection}";
            Condition = (key) => key is Key.Default && !string.IsNullOrWhiteSpace(person.LastDirection) && !person.DirectionLatch;
            Action = () => person.Idle();
        }
    }
}
