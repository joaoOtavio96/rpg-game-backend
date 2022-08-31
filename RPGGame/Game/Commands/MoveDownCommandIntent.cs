using RPGGame.Config;

namespace RPGGame.Game
{
    public class MoveDownCommandIntent : CommandIntent, IMovementTypeObject
    {
        public MoveDownCommandIntent(IGameObject gameObject)
        {
            GameObject = gameObject;
            Direction = "Down";
            Animation = () => "WalkDown";
            Condition = (key) => key is Key.S || (gameObject.Command.LastKey is Key.S && gameObject.Command.DirectionLatch);
            Action = () => MoveDown() ;
        }

        public string Direction { get; set; }
        public Func<string> Animation { get; set; }

        public void MoveDown()
        {
            GameObject.Position.Y += GameObject.Command.MovementProgress;
            GameObject.Position.DeltaY += GameObject.Command.MovementProgress;
        }

        public override IGameObject NextPosition()
        {
            return new Person 
            { 
                Position = new Position()
                {
                    RelativeX = GameObject.Position.RelativeX,
                    RelativeY = GameObject.Position.RelativeY + MapConfig.GridSize
                }               
            };
        }
    }
}
