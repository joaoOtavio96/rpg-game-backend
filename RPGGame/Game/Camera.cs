using RPGGame.Config;
using RPGGame.Game.Commands;

namespace RPGGame.Game
{
    public static class Camera
    {
        public static void SetPositions(List<ObjectToProcess> objectToProccess)
        {
            var mainSprite = objectToProccess.Single(c => c.GameObject.Main);
            var otherSprites = objectToProccess.Where(c => !c.GameObject.Main);

            foreach (var other in otherSprites)
            {
                other.GameObject.Collision.CollisionBodies?.ForEach(b =>
                {
                    b.RelativeX = b.X + mainSprite.GameObject.DeltaX * (-1) + (GameConfig.CanvasWidth / 2);
                    b.RelativeY = b.Y + mainSprite.GameObject.DeltaY * (-1) + (GameConfig.CanvasHeight / 2);
                });
            }

            foreach (var other in otherSprites)
            {
                other.GameObject.RelativeX = other.GameObject.X + mainSprite.GameObject.DeltaX * (-1) + (GameConfig.CanvasWidth / 2);
                other.GameObject.RelativeY = other.GameObject.Y + mainSprite.GameObject.DeltaY * (-1) + (GameConfig.CanvasHeight / 2);
            }

            mainSprite.GameObject.RelativeX = (GameConfig.CanvasWidth / 2);
            mainSprite.GameObject.RelativeY = (GameConfig.CanvasHeight / 2);
        }
    }
}
