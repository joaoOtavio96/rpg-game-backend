using RPGGame.Config;
using RPGGame.Game.Cameras;
using RPGGame.Game.Collisions;
using RPGGame.Game.Commands;

namespace RPGGame.Game
{
    public class Person : IGameObject
    {
        public Person()
        {
        }
        public Person(string name, string path, double x, double y, int width, int height, int gridWidth, int gridHeight)
        {

            var calculatedX = MapConfig.ConvertToPixel(x) + MapConfig.ConvertToPixel(6) * (-1) - (MapConfig.GridSize / 2);
            var calculatedY = MapConfig.ConvertToPixel(y) + MapConfig.ConvertToPixel(7) * (-1) - (MapConfig.GridSize / 8);
            Name = name;
            Position = new Position
            {
                DeltaX = calculatedX,
                DeltaY = calculatedY,
                X = calculatedX,
                Y = calculatedY
            };
            Collision = new Collision(this);
            Command = new Command(this);
            Camera = new Camera(this);
            Sprite = new Sprite(path, width, height, gridWidth, gridHeight);
        }

        public string Name { get; set; }
        public Sprite Sprite { get; set; }
        public Camera Camera { get; set; }
        public Collision Collision { get; set; }
        public Command Command { get; set; }
        public Position Position { get; set; }
        public Bounds Bounds => new Bounds(Position.RelativeX, Position.RelativeY);
    }
}
