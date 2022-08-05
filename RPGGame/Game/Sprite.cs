namespace RPGGame.Game
{
    public class Sprite
    {
        public Sprite(string path)
        {
            var directory = AppDomain.CurrentDomain.BaseDirectory;
            OriginalImage = File.ReadAllBytes(Path.Combine(directory, path));
            Image = OriginalImage;
            X = 0;
            Y = 0;
            DeltaX = 0;
            DeltaY = 0;
        }

        public Sprite(string path, double x, double y, int width, int height)
        {
            var directory = AppDomain.CurrentDomain.BaseDirectory;
            OriginalImage = File.ReadAllBytes(Path.Combine(directory, path));
            Image = OriginalImage;
            X = x;
            Y = y;
            Width = width;
            Height = height;
            DeltaX = 0;
            DeltaY = 0;
        }

        public double DeltaX { get; set; }
        public double DeltaY { get; set; }
        public double RelativeX { get; set; }
        public double RelativeY { get; set; }
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
