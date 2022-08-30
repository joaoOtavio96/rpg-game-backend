using RPGGame.Config;

namespace RPGGame.Game
{
    public class MoveRightCommand : Command, IMovementTypeObject
    {
        public MoveRightCommand(Person person)
        {
            GameObject = person;
            Direction = "Right";
            Animation = () => "WalkRight";
            Condition = (key) => key is Key.D || (person.LastKey is Key.D && person.DirectionLatch);
            Action = () => MoveRight();
        }

        public string Direction { get; set; }
        public Func<string> Animation { get; set; }

        public void MoveRight()
        {
            GameObject.X += GameObject.MovementProgress;
            GameObject.DeltaX += GameObject.MovementProgress;
        }

        public override IGameObject NextPosition()
        {
            return new Person 
            { 
                RelativeY = GameObject.RelativeY,
                RelativeX = GameObject.RelativeX + MapConfig.GridSize 
            };
        }
    }
}
