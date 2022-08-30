using RPGGame.Config;

namespace RPGGame.Game
{
    public class MoveLeftCommand : Command, IMovementTypeObject
    {
        public MoveLeftCommand(Person person)
        {
            GameObject = person;
            Direction = "Left";
            Animation = () => "WalkLeft";
            Condition = (key) => key is Key.A || (person.LastKey is Key.A && person.DirectionLatch);
            Action = () => MoveLeft();
        }

        public string Direction { get; set; }
        public Func<string> Animation { get; set; }

        public void MoveLeft()
        {
            GameObject.X -= GameObject.MovementProgress;
            GameObject.DeltaX -= GameObject.MovementProgress;
        }
        public override IGameObject NextPosition()
        {
            return new Person 
            { 
                RelativeY = GameObject.RelativeY,
                RelativeX = GameObject.RelativeX - MapConfig.GridSize 
            };
        }
    }
}
