using RPGGame.Config;

namespace RPGGame.Game
{
    public class MoveLeftCommandIntent : CommandIntent, IMovementTypeObject
    {
        public MoveLeftCommandIntent(IGameObject gameObject)
        {
            GameObject = gameObject;
            Direction = "Left";
            Animation = () => "WalkLeft";
            Condition = (key) => key is Key.A || (gameObject.Command.LastKey is Key.A && gameObject.Command.DirectionLatch);
            Action = () => MoveLeft();
        }

        public string Direction { get; set; }
        public Func<string> Animation { get; set; }

        public void MoveLeft()
        {
            GameObject.Position.X -= GameObject.Command.MovementProgress;
            GameObject.Position.DeltaX -= GameObject.Command.MovementProgress;
        }
        public override IGameObject NextPosition()
        {
            return new Person 
            { 
                Position = new Position
                {
                    RelativeY = GameObject.Position.RelativeY,
                    RelativeX = GameObject.Position.RelativeX - MapConfig.GridSize
                }              
            };
        }
    }
}
