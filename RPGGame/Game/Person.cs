using RPGGame.Config;

namespace RPGGame.Game
{
    public class Person : Sprite, ICommandObject, ICameraObject
    {
        public Person(string path, double x, double y, int width, int height) : base(path, x, y, width, height)
        {
            LastDirection = "Down";
            LastKey = Key.Default;
            DirectionLatch = false;
            MovementProgress = MovementConfig.MovementProgress;
            MovementLimit = MovementConfig.MovementLimit;
            var calculatedX = MapConfig.ConvertToPixel(x) + MapConfig.ConvertToPixel(6) * (-1) - (MapConfig.GridSize / 2);
            var calculatedY = MapConfig.ConvertToPixel(y) + MapConfig.ConvertToPixel(7) * (-1) - (MapConfig.GridSize / 8);
            DeltaX += calculatedX;
            DeltaY += calculatedY;
            X = calculatedX;
            Y = calculatedY;
            CommandMap = new CommandKeyMap()
                .AddMap(new KeyValuePair<Key, Command>(Key.W, new MoveUpCommand(this)))
                .AddMap(new KeyValuePair<Key, Command>(Key.S, new MoveDownCommand(this)))
                .AddMap(new KeyValuePair<Key, Command>(Key.A, new MoveLeftCommand(this)))
                .AddMap(new KeyValuePair<Key, Command>(Key.D, new MoveRightCommand(this)))
                .AddMap(new KeyValuePair<Key, Command>(Key.Default, new IdleCommand(this)));
        }

        public CommandKeyMap CommandMap { get; set; }
        public double MovementLimit { get; private set; }
        public double MovementProgress { get; private set; }
        public string LastDirection { get; private set; }
        public Key LastKey { get; private set; }
        public bool DirectionLatch { get; private set; }
        public bool MovementCompleted => MovementLimit <= 0;
        public bool Main { get; set; }

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
            DeltaY -= MovementProgress;
        }

        public void MoveDown()
        {
            Y += MovementProgress;
            DeltaY += MovementProgress;
        }

        public void MoveLeft()
        {
            X -= MovementProgress;
            DeltaX -= MovementProgress;
        }

        public void MoveRight()
        {
            X += MovementProgress;
            DeltaX += MovementProgress;
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
    }
}
