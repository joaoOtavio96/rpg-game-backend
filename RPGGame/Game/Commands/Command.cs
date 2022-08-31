using RPGGame.Config;

namespace RPGGame.Game.Commands
{
    public class Command
    {
        private readonly IGameObject _gameObject;
        public Command(IGameObject gameObject)
        {
            _gameObject = gameObject;
            LastDirection = "Down";
            LastKey = Key.Default;
            DirectionLatch = false;
            MovementProgress = MovementConfig.MovementProgress;
            MovementLimit = MovementConfig.MovementLimit;
            CommandMap = new CommandKeyMap()
                .AddMap(new KeyValuePair<Key, CommandIntent>(Key.W, new MoveUpCommandIntent(gameObject)))
                .AddMap(new KeyValuePair<Key, CommandIntent>(Key.S, new MoveDownCommandIntent(gameObject)))
                .AddMap(new KeyValuePair<Key, CommandIntent>(Key.A, new MoveLeftCommandIntent(gameObject)))
                .AddMap(new KeyValuePair<Key, CommandIntent>(Key.D, new MoveRightCommandIntent(gameObject)))
                .AddMap(new KeyValuePair<Key, CommandIntent>(Key.Default, new IdleCommandIntent(gameObject)));

        }

        public double MovementLimit { get; set; }
        public double MovementProgress { get; set; }
        public string LastDirection { get; set; }
        public Key LastKey { get; set; }
        public bool MovementCompleted => MovementLimit <= 0;
        public CommandKeyMap CommandMap { get; set; }
        public bool DirectionLatch { get; set; }

        public virtual void OnProccessing(CommandIntent command, Action completed)
        {
            var movement = command as IMovementTypeObject;
            if (MovementCompleted)
            {
                StopMovement();
                completed();
            }

            if (!string.IsNullOrWhiteSpace(movement.Direction))
                LastDirection = movement.Direction;

            LastKey = command.KeyToProccess;
            UpdateMovement();
            _gameObject.Sprite.UpdateAnimation(movement.Animation());
        }

        public void UpdateMovement()
        {
            if (MovementCompleted)
                return;

            DirectionLatch = true;
            MovementLimit -= MovementProgress;
        }

        public void StopMovement()
        {
            DirectionLatch = false;
            LastKey = Key.Default;
            MovementLimit = MovementConfig.MovementLimit;
            _gameObject.Sprite.Animation.ResetFrame();
        }
    }
}
