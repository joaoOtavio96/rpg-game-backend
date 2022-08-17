using RPGGame.Config;

namespace RPGGame.Game
{
    public class Map : IGameObject, ICameraObject
    {
        public Map(string name, string path, int width, int height, int gridWidth, int gridHeight)
        {
            X = (width / 2) * (-1);
            Y = (height / 2) * (-1);
            Name = name;
            Sprite = new Sprite(path, width, height, gridWidth, gridHeight);
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
        public double MinX { get; set; }
        public double MaxX { get; set; }
        public double MinY { get; set; }
        public double MaxY { get; set; }
        public bool HasCollision { get; set; }
    }
}
