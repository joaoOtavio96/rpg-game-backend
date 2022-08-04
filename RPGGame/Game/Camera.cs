using RPGGame.Config;

namespace RPGGame.Game
{
    public static class Camera
    {
        public static List<GameObjectDto> SetPositions(Sprite main, List<Sprite> others)
        {
            List<GameObjectDto> gameObjects = new List<GameObjectDto>();

            foreach (var other in others)
            {
                GameObjectDto gameObject = new GameObjectDto()
                {
                    Sprite = other.Image,
                    X = other.X + main.DeltaX * (-1),
                    Y = other.Y + main.DeltaY * (-1)
                };

                gameObjects.Add(gameObject);
            }

            var mainObject = new GameObjectDto()
            {
                Sprite = main.Image,
                X = (GameConfig.CanvasWidth / 2),
                Y = (GameConfig.CanvasHeight / 2)
            };

            gameObjects.Add(mainObject);

            return gameObjects;
        }
    }
}
