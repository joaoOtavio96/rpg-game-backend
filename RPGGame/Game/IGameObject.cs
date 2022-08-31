using RPGGame.Config;
using RPGGame.Game.Commands;

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
        public Collision Collision { get; set; }
        public Command Command { get; set; }

        // Camera
        public bool Main { get; set; }
    }
}
