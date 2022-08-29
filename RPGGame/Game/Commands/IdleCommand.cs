namespace RPGGame.Game
{
    public class IdleCommand : Command, IMovementTypeObject
    {
        private readonly Person _person;
        public IdleCommand(Person person)
        {
            _person = person;
            Animation = () => $"Idle{person.LastDirection}";
            Condition = (key) => key is Key.Default && !string.IsNullOrWhiteSpace(person.LastDirection) && !person.DirectionLatch;
            Action = () => { };
        }

        public string Direction { get; set; }
        public Func<string> Animation { get; set; }

        public override IGameObject NextPosition()
        {
            return new Person { RelativeX = _person.RelativeX, RelativeY = _person.RelativeY };
        }

        public override IGameObject CurrentState()
        {
            return _person;
        }
    }
}
