using RPGGame.Config;

namespace RPGGame.Game
{
    public class MoveLeftCommand : Command, IMovementTypeObject
    {
        private readonly Person _person;
        public MoveLeftCommand(Person person)
        {
            _person = person;
            Direction = "Left";
            Animation = () => "WalkLeft";
            Condition = (key) => key is Key.A || (person.LastKey is Key.A && person.DirectionLatch);
            Action = () => MoveLeft();
        }

        public string Direction { get; set; }
        public Func<string> Animation { get; set; }

        public void MoveLeft()
        {
            _person.X -= _person.MovementProgress;
            _person.DeltaX -= _person.MovementProgress;
        }
        public override IGameObject NextPosition()
        {
            return new Person { RelativeX = _person.RelativeX - MapConfig.GridSize };
        }
    }
}
