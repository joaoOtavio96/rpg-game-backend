using RPGGame.Config;

namespace RPGGame.Game
{
    public class MoveUpCommand : Command, IMovementTypeObject
    {
        public MoveUpCommand(Person person)
        {
            GameObject = person;
            Direction = "Up";
            Animation = () => "WalkUp";
            Condition = (key) => key is Key.W || (person.LastKey is Key.W && person.DirectionLatch);
            Action = () => MoveUp();
        }

        public string Direction { get; set; }
        public Func<string> Animation { get; set; }

        public void MoveUp()
        {
            GameObject.Y -= GameObject.MovementProgress;
            GameObject.DeltaY -= GameObject.MovementProgress;
        }

        public override IGameObject NextPosition()
        {
            return new Person 
            { 
                RelativeX = GameObject.RelativeX,
                RelativeY = GameObject.RelativeY - MapConfig.GridSize 
            };
        }
    }
}
