namespace RPGGame.Game.Config
{
    public class JsonConfig
    {
        public string Name { get; set; }
        public double InitialX { get; set; }
        public double InitialY { get; set; }
        public Sprite Sprite { get; set; }
        public List<Animation> Animations { get; set; }
        public List<CollisionPoint> CollisionPoints { get; set; }
    }
}
