using RPGGame.Config;

namespace RPGGame.Game
{
    public interface IGameObject
    {
        public double DeltaX { get; set; }
        public double DeltaY { get; set; }
        public double RelativeX { get; set; }
        public double RelativeY { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double MinX { get; }
        public double MaxX { get; }
        public double MinY { get; }
        public double MaxY { get; }
        public string Name { get; set; }
        public Sprite Sprite { get; set; }

        // Collision
        public void AddCollisionBody(double x, double y);
        public void UpdateColisionBody();
        public List<CollisionBody> ObjectsWithCollision { get; set; }
        public List<CollisionBody> CollisionBodies { get; set; }

        // Command
        public double MovementLimit { get; set; }
        public double MovementProgress { get; set; }
        public string LastDirection { get; set; }
        public Key LastKey { get; set; }
        public bool MovementCompleted => MovementLimit <= 0;
        public CommandKeyMap CommandMap { get; set; }
        void OnProccessing(Command command, Action completed);
        public bool DirectionLatch { get; set; }

        // Camera
        public bool Main { get; set; }
    }
}
