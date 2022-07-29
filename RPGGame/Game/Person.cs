using RPGGame.Config;

namespace RPGGame.Game
{
    public class Person : Sprite, ICommandObject
    {
        public Person(string path, int width, int height) : base(path, width, height)
        {
            LastDirection = "Down";
            LastKey = Key.Default;
            DirectionLatch = false;
            MovementProgress = MovementConfig.MovementProgress;
            MovementLimit = MovementConfig.MovementLimit;
            Width = width;
            Height = height;
            CommandMap = new CommandMap
            {
                Map = new Dictionary<Key, Command>
                {
                    { Key.W, new MoveUpCommand(this) },
                    { Key.S, new MoveDownCommand(this) },
                    { Key.A, new MoveLeftCommand(this) },
                    { Key.D, new MoveRightCommand(this) },
                    { Key.Default, new IdleCommand(this) }
                }
            };
        }

        public CommandMap CommandMap { get; set; }
        public double MovementLimit { get; private set; }
        public double MovementProgress { get; private set; }
        public string LastDirection { get; private set; }
        public Key LastKey { get; private set; }
        public bool DirectionLatch { get; private set; }
        public bool MovementCompleted => MovementLimit <= 0;

        public void OnProccessing(Command command, Action completed)
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
            UpdateAnimation(movement.Animation());
        }

        public void MoveUp()
        {
            Y -= MovementProgress;
        }

        public void MoveDown()
        {
            Y += MovementProgress;
        }

        public void MoveLeft()
        {
            X -= MovementProgress;
        }

        public void MoveRight()
        {
            X += MovementProgress;
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
            Animation.ResetFrame();
        }

        public void Idle()
        {
           
        }
    }
}
