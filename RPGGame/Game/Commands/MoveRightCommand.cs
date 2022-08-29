using RPGGame.Config;

namespace RPGGame.Game
{
    public class MoveRightCommand : Command, IMovementTypeObject
    {
        private readonly Person _person;

        public MoveRightCommand(Person person)
        {
            _person = person;
            Direction = "Right";
            Animation = () => "WalkRight";
            Condition = (key) => key is Key.D || (person.LastKey is Key.D && person.DirectionLatch);
            Action = () => MoveRight();
        }

        public string Direction { get; set; }
        public Func<string> Animation { get; set; }

        public void MoveRight()
        {
            _person.X += _person.MovementProgress;
            _person.DeltaX += _person.MovementProgress;
        }

        public override IGameObject NextPosition()
        {
            return new Person 
            { 
                RelativeY = _person.RelativeY,
                RelativeX = _person.RelativeX + MapConfig.GridSize 
            };
        }
        public override IGameObject CurrentState()
        {
            return _person;
        }
    }
}
