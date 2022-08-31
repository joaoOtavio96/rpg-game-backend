using RPGGame.Config;
using RPGGame.Game.Commands;

namespace RPGGame.Game.Cameras
{
    public static class CameraService
    {
        public static void SetPositions(List<ObjectToProcess> objectToProccess)
        {
            var mainSprite = objectToProccess.Single(c => c.GameObject.Camera.Main);
            var otherSprites = objectToProccess.Where(c => !c.GameObject.Camera.Main);

            foreach (var other in otherSprites)
            {
                other.GameObject.Collision.CollisionBodies?.ForEach(b =>
                {
                    b.RelativeX = b.X + mainSprite.GameObject.Position.DeltaX * -1 + GameConfig.CanvasWidth / 2;
                    b.RelativeY = b.Y + mainSprite.GameObject.Position.DeltaY * -1 + GameConfig.CanvasHeight / 2;
                });
            }

            foreach (var other in otherSprites)
            {
                other.GameObject.Position.RelativeX = other.GameObject.Position.X + mainSprite.GameObject.Position.DeltaX * -1 + GameConfig.CanvasWidth / 2;
                other.GameObject.Position.RelativeY = other.GameObject.Position.Y + mainSprite.GameObject.Position.DeltaY * -1 + GameConfig.CanvasHeight / 2;
            }

            mainSprite.GameObject.Position.RelativeX = GameConfig.CanvasWidth / 2;
            mainSprite.GameObject.Position.RelativeY = GameConfig.CanvasHeight / 2;
        }
    }
}
