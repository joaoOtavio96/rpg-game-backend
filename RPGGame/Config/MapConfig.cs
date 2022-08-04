namespace RPGGame.Config
{
    public static class MapConfig
    {
        public static double GridSize => 16;

        public static double ConvertToGridSize(double size)
        {
           return size / GridSize;
        }

        public static double ConvertToPixel(double size)
        {
            return size * GridSize;
        }
    }
}
