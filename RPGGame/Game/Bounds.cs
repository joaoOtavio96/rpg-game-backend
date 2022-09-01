using RPGGame.Config;

namespace RPGGame.Game
{
    public class Bounds
    {
        private readonly double _relativeX, _relativeY;
        public Bounds(double relativeX, double relativeY)
        {
            _relativeX = relativeX;
            _relativeY = relativeY;
        }

        public double MinX => _relativeX + 10;
        public double MaxX => MinX + MapConfig.GridSize - 7;
        public double MinY => _relativeY + 18 + (MapConfig.GridSize / 8);
        public double MaxY => MinY + MapConfig.GridSize - 8;
    }
}
