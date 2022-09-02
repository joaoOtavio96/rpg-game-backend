using RPGGame.Config;

namespace RPGGame.Game
{
    public class Position
    {
        public double DeltaX { get; set; }
        public double DeltaY { get; set; }
        public double RelativeX { get; set; }
        public double RelativeY { get; set; }
        public double X { get; set; }
        public double Y { get; set; }

        public void SetRelativePosition(IGameObject gameObject)
        {
            RelativeX = X + gameObject.Position.DeltaX * -1 + GameConfig.CanvasWidth / 2;
            RelativeY = Y + gameObject.Position.DeltaY * -1 + GameConfig.CanvasHeight / 2;
        }

        public void CenterRelativePosition()
        {
            RelativeX = GameConfig.CanvasWidth / 2;
            RelativeY = GameConfig.CanvasHeight / 2;
        }
    }
}
