using RPGGame.Config;
using RPGGame.Game.Commands;

namespace RPGGame.Game
{
    public static class Camera
    {
        public static void SetPositions(List<ICameraObject> cameraObjects)
        {
            var mainSprite = cameraObjects.Single(c => c.Main);
            var otherSprites = cameraObjects.Where(c => !c.Main);

            foreach (var other in otherSprites.Where(o => o is IStaticCollisionObject))
            {
                (other as IStaticCollisionObject).CollisionBodies.ForEach(b =>
                {
                    b.RelativeX = b.X + mainSprite.DeltaX * (-1) + (GameConfig.CanvasWidth / 2);
                    b.RelativeY = b.Y + mainSprite.DeltaY * (-1) + (GameConfig.CanvasHeight / 2);
                });
            }

            foreach (var other in otherSprites)
            {
                other.RelativeX = other.X + mainSprite.DeltaX * (-1) + (GameConfig.CanvasWidth / 2);
                other.RelativeY = other.Y + mainSprite.DeltaY * (-1) + (GameConfig.CanvasHeight / 2);
            }

            mainSprite.RelativeX = (GameConfig.CanvasWidth / 2);
            mainSprite.RelativeY = (GameConfig.CanvasHeight / 2);
        }
    }
}
