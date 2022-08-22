namespace RPGGame.Game
{
    public interface IStaticCollisionObject
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double RelativeX { get; set; }
        public double RelativeY { get; set; }
        public double MinX { get; }
        public double MaxX { get; }
        public double MinY { get; }
        public double MaxY { get; }
        public bool HasCollision { get; set; }
        public List<CollisionBody> CollisionBodies { get; set; }
        public void AddCollisionBody(double x, double y);
    }
}
