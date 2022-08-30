using RPGGame.Config;

namespace RPGGame.Game
{
    public class Map : IGameObject, ICameraObject, ICollisionObject
    {
        public Map(string name, string path, int width, int height, int gridWidth, int gridHeight)
        {
            CollisionBodies = new List<CollisionBody>();
            ObjectsWithCollision = new List<CollisionBody>();
            X = (width / 2) * (-1);
            Y = (height / 2) * (-1);
            Name = name;
            Sprite = new Sprite(path, width, height, gridWidth, gridHeight);
        }

        public Map()
        {
            CollisionBodies = new List<CollisionBody>();
            ObjectsWithCollision = new List<CollisionBody>();
        }

        public bool Main { get; set; }
        public double DeltaX { get; set; }
        public double DeltaY { get; set; }
        public double RelativeX { get; set; }
        public double RelativeY { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public string Name { get; set; }
        public Sprite Sprite { get; set; }
        public double MinX => RelativeX + 10;
        public double MaxX => MinX + MapConfig.GridSize - 7;
        public double MinY => RelativeY + 18 + (MapConfig.GridSize / 8);
        public double MaxY => MinY + MapConfig.GridSize - 8;
        public bool HasCollision { get; set; }
        public List<CollisionBody> CollisionBodies { get; set; }
        public List<CollisionBody> ObjectsWithCollision { get; set; }
        public CommandKeyMap CommandMap { get; set; }
        public bool DirectionLatch { get; set; }
        public double MovementLimit { get; set; }
        public double MovementProgress { get; set; }
        public string LastDirection { get; set; }
        public Key LastKey { get; set; }

        public void AddCollisionBody(double x, double y)
        {
            var collisionBody = new CollisionBody
            {
                GameObject = this,
                X = MapConfig.ConvertToPixel(x) + MapConfig.ConvertToPixel(6) * (-1) - (MapConfig.GridSize / 2),
                Y = MapConfig.ConvertToPixel(y) + MapConfig.ConvertToPixel(7) * (-1) - (MapConfig.GridSize / 8),
            };

            CollisionBodies.Add(collisionBody);
        }

        public void UpdateColisionBody()
        {
            
        }

        public void OnProccessing(Command command, Action completed)
        {
            throw new NotImplementedException();
        }
    }
}
