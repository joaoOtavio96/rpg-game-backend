using RPGGame.Config;
using RPGGame.Game.Commands.Intents;

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
        }

        public double MovementLimit { get; set; }
        public double MovementProgress { get; set; }
        public string LastDirection { get; set; }
        public Key LastKey { get; set; }
        public bool MovementCompleted => MovementLimit <= 0;
        public bool DirectionLatch { get; set; }

        public virtual void OnProccessing(CommandIntent command, Action completed)
        {
            var movement = command as IMovementTypeObject;
             if (MovementCompleted)
            {
                StopMovement();

                if(command.GameObject.Camera.Main)
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
