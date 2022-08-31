using RPGGame.Config;

namespace RPGGame.Game
{
    public class MoveUpCommandIntent : CommandIntent, IMovementTypeObject
    {
        public MoveUpCommandIntent(IGameObject gameObject)
        {
            GameObject = gameObject;
            Direction = "Up";
            Animation = () => "WalkUp";
            Condition = (key) => key is Key.W || (gameObject.Command.LastKey is Key.W && gameObject.Command.DirectionLatch);
            Action = () => MoveUp();
        }

        public string Direction { get; set; }
        public Func<string> Animation { get; set; }

        public void MoveUp()
        {
            GameObject.Position.Y -= GameObject.Command.MovementProgress;
            GameObject.Position.DeltaY -= GameObject.Command.MovementProgress;
        }

        public override IGameObject NextPosition()
        {
            return new Person 
            { 
                Position = new Position
                {
                    RelativeX = GameObject.Position.RelativeX,
                    RelativeY = GameObject.Position.RelativeY - MapConfig.GridSize
                }            
            };
        }
    }
}
