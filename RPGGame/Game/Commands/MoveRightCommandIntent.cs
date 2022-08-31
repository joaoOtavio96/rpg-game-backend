using RPGGame.Config;

namespace RPGGame.Game
{
    public class MoveRightCommandIntent : CommandIntent, IMovementTypeObject
    {
        public MoveRightCommandIntent(IGameObject gameObject)
        {
            GameObject = gameObject;
            Direction = "Right";
            Animation = () => "WalkRight";
            Condition = (key) => key is Key.D || (gameObject.Command.LastKey is Key.D && gameObject.Command.DirectionLatch);
            Action = () => MoveRight();
        }

        public string Direction { get; set; }
        public Func<string> Animation { get; set; }

        public void MoveRight()
        {
            GameObject.X += GameObject.Command.MovementProgress;
            GameObject.DeltaX += GameObject.Command.MovementProgress;
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
