using RPGGame.Config;

namespace RPGGame.Game
{
    public class MoveUpCommand : Command, IMovementTypeObject
    {
        private readonly Person _person;
        public MoveUpCommand(Person person)
        {
            _person = person;
            Direction = "Up";
            Animation = () => "WalkUp";
            Condition = (key) => key is Key.W || (person.LastKey is Key.W && person.DirectionLatch);
            Action = () => MoveUp();
        }

        public string Direction { get; set; }
        public Func<string> Animation { get; set; }

        public void MoveUp()
        {
            _person.Y -= _person.MovementProgress;
            _person.DeltaY -= _person.MovementProgress;
        }

        public override IGameObject NextPosition()
        {
            return new Person { RelativeY = _person.RelativeY - _person.RelativeY };
        }
    }
}
