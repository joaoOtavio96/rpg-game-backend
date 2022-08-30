using RPGGame.Config;

namespace RPGGame.Game
{
    public class Person : IGameObject, ICommandObject, ICameraObject, ICollisionObject
    {
        public Person()
        {
            CollisionBodies = new List<CollisionBody>();
            ObjectsWithCollision = new List<CollisionBody>();
        }
        public Person(string name, string path, double x, double y, int width, int height, int gridWidth, int gridHeight)
        {
            CollisionBodies = new List<CollisionBody>();
            ObjectsWithCollision = new List<CollisionBody>();
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
            Name = name;
            CommandMap = new CommandKeyMap()
                .AddMap(new KeyValuePair<Key, Command>(Key.W, new MoveUpCommand(this)))
                .AddMap(new KeyValuePair<Key, Command>(Key.S, new MoveDownCommand(this)))
                .AddMap(new KeyValuePair<Key, Command>(Key.A, new MoveLeftCommand(this)))
                .AddMap(new KeyValuePair<Key, Command>(Key.D, new MoveRightCommand(this)))
                .AddMap(new KeyValuePair<Key, Command>(Key.Default, new IdleCommand(this)));

            Sprite = new Sprite(path, width, height, gridWidth, gridHeight);
        }

        public CommandKeyMap CommandMap { get; set; }
        public double MovementLimit { get; set; }
        public double MovementProgress { get; set; }
        public string LastDirection { get; set; }
        public Key LastKey { get; set; }
        public bool DirectionLatch { get; set; }
        public bool MovementCompleted => MovementLimit <= 0;
        public bool Main { get; set; }
        public double DeltaX { get; set; }
        public double DeltaY { get; set; }
        public double RelativeX { get; set; }
        public double RelativeY { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public Sprite Sprite { get; set; }
        public string Name { get; set; }
        public bool HasCollision { get; set; }
        public double MinX => RelativeX + 10;
        public double MaxX => MinX + MapConfig.GridSize - 7;
        public double MinY => RelativeY + 18 + (MapConfig.GridSize / 8);
        public double MaxY => MinY + MapConfig.GridSize - 8;
        public List<CollisionBody> CollisionBodies { get; set; }
        public List<CollisionBody> ObjectsWithCollision { get; set; }

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
            Sprite.UpdateAnimation(movement.Animation());
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
            HasCollision = false;
            LastKey = Key.Default;
            MovementLimit = MovementConfig.MovementLimit;
            Sprite.Animation.ResetFrame();
        }

        public void AddCollisionBody(double x, double y)
        {
            throw new NotImplementedException();
        }

        public void UpdateColisionBody()
        {
            CollisionBodies.Clear();
            CollisionBodies.Add(new CollisionBody
            {
                GameObject = this,
                RelativeX = RelativeX,
                RelativeY = RelativeY,
                X = X,
                Y = Y
            });
        }
    }
}
