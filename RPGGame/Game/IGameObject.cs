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
        public bool HasCollision { get; set; }
        public string Name { get; set; }
        public Sprite Sprite { get; set; }
    }
}
