using RPGGame.Config;

namespace RPGGame.Game
{
    public class MoveDownCommand : Command, IMovementTypeObject
    {
        private readonly Person _person;

        public MoveDownCommand(Person person)
        {
            _person = person;
            Direction = "Down";
            Animation = () => "WalkDown";
            Condition = (key) => key is Key.S || (person.LastKey is Key.S && person.DirectionLatch);
            Action = () => MoveDown() ;
        }

        public string Direction { get; set; }
        public Func<string> Animation { get; set; }

        public void MoveDown()
        {
            _person.Y += _person.MovementProgress;
            _person.DeltaY += _person.MovementProgress;
        }

        public override IGameObject NextPosition()
        {
            return new Person { RelativeY = _person.RelativeY + _person.RelativeY };
        }
    }
}
