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

    }
}
