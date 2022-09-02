using RPGGame.Config;
using RPGGame.Game.Cameras;
using RPGGame.Game.Collisions;
using RPGGame.Game.Commands;

namespace RPGGame.Game
{
    public class Map : IGameObject
    {
        public Map(string name, string path, int width, int height, int gridWidth, int gridHeight)
        {
            Name = name;
            Position = new Position
            {
                X = (width / 2) * (-1),
                Y = (height / 2) * (-1),
            };
            Sprite = new Sprite(path, width, height, gridWidth, gridHeight);
            Collision = new Collision(this);
            Collision.Static = true;
            Camera = new Camera(this);
        }

        public string Name { get; set; }
        public Sprite Sprite { get; set; }
        public Collision Collision { get; set; }
        public Command Command { get; set; }
        public Camera Camera { get; set; }
        public Position Position { get; set; }
        public Bounds Bounds => new Bounds(Position.RelativeX, Position.RelativeY);
    }
}
