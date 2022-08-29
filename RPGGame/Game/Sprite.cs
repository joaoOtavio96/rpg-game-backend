namespace RPGGame.Game
{
    public class Sprite
    {
        public Sprite(string path, int width, int height, int gridWidth, int gridHeight)
        {
            var directory = AppDomain.CurrentDomain.BaseDirectory;
            OriginalImage = File.ReadAllBytes(Path.Combine(directory, path));
            Image = OriginalImage;
            Width = width;
            Height = height;
            GridWidth = gridWidth;
            GridHeight = gridHeight;
        }

        public int Width { get; set; }
        public int Height { get; set; }
        public int GridWidth { get; set; }
        public int GridHeight { get; set; }
        public byte[] OriginalImage { get; private set; }
        public byte[] Image { get; private set; }
        public Animation Animation { get; set; }

        public void UpdateAnimation(string name)
        {
            Image = Animation.GetAnimationImage(OriginalImage, name, GridWidth, GridHeight);
        }
    }
}
