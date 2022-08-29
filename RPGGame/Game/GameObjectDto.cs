using RPGGame.Config;

namespace RPGGame.Game
{
    public class GameObjectDto
    {
        public string Name { get; set; }
        public byte[] Sprite { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double MinX { get; set; }
        public double MaxX { get; set; }
        public double MinY { get; set; }
        public double MaxY { get; set; }
        public bool HasCollision { get; set; }
        public List<CollisionBodyDto> CollisionBodies { get; set; }

    }

    public class CollisionBodyDto
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
    }
}
