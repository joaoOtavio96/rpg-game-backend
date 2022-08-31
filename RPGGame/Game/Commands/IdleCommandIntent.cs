namespace RPGGame.Game.Commands
{
    public class IdleCommandIntent : CommandIntent, IMovementTypeObject
    {
        public IdleCommandIntent(IGameObject gameObject)
        {
            GameObject = gameObject;
            Animation = () => $"Idle{gameObject.Command.LastDirection}";
            Condition = (key) => key is Key.Default && !string.IsNullOrWhiteSpace(gameObject.Command.LastDirection) && !gameObject.Command.DirectionLatch;
            Action = () => { };
        }

        public string Direction { get; set; }
        public Func<string> Animation { get; set; }

        public override IGameObject NextPosition()
        {
            return new Person
            {
                Position = new Position 
                { 
                    RelativeX = GameObject.Position.RelativeX, 
                    RelativeY = GameObject.Position.RelativeY 
                }
            };
        }
    }
}
