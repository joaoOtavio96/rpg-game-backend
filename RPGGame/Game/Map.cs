using RPGGame.Config;
using RPGGame.Game.Commands;

namespace RPGGame.Game
{
    public class Map : IGameObject
    {
        public Map(string name, string path, int width, int height, int gridWidth, int gridHeight)
        {
            X = (width / 2) * (-1);
            Y = (height / 2) * (-1);
            Name = name;
            Sprite = new Sprite(path, width, height, gridWidth, gridHeight);
            Collision = new Collision(this);
            Collision.Static = true;
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
        public Collision Collision { get; set; }
        public Command Command { get; set; }
    }
}
