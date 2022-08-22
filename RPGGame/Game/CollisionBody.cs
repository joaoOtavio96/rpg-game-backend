using RPGGame.Config;

namespace RPGGame.Game
{
    public class CollisionBody
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double RelativeX { get; set; }
        public double RelativeY { get; set; }
        public double MinX => RelativeX + 10;
        public double MaxX => MinX + MapConfig.GridSize - 7;
        public double MinY => RelativeY + 18 + (MapConfig.GridSize / 8);
        public double MaxY => MinY + MapConfig.GridSize - 8;
        public bool HasCollision { get; set; }
        public IGameObject GameObject { get; set; }
    }
}
