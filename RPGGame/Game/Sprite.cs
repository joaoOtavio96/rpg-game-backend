namespace RPGGame.Game
{
    public class Sprite
    {
        public Sprite(string path)
        {
            var directory = AppDomain.CurrentDomain.BaseDirectory;
            OriginalImage = File.ReadAllBytes(Path.Combine(directory, path));
            X = 0;
            Y = 0;
        }

        public Sprite(string path, int width, int height)
        {
            var directory = AppDomain.CurrentDomain.BaseDirectory;
            OriginalImage = File.ReadAllBytes(Path.Combine(directory, path));
            X = 0;
            Y = 0;
            Width = width;
            Height = height;
        }

        public double X { get; set; }
        public double Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public byte[] OriginalImage { get; private set; }
        public byte[] Image { get; private set; }
        public Animation Animation { get; set; }

        public void UpdateAnimation(string name)
        {
            Image = Animation.GetAnimationImage(OriginalImage, name, Width, Height);
        }
    }
}
