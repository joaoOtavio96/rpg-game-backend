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
            GameObject.Y += GameObject.Command.MovementProgress;
            GameObject.DeltaY += GameObject.Command.MovementProgress;
        }

        public override IGameObject NextPosition()
        {
            return new Person 
            { 
                RelativeX = GameObject.RelativeX,
                RelativeY = GameObject.RelativeY + MapConfig.GridSize 
            };
        }
    }
}
