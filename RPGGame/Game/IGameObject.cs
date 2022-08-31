using RPGGame.Config;
using RPGGame.Game.Cameras;
using RPGGame.Game.Collisions;
using RPGGame.Game.Commands;

namespace RPGGame.Game
{
    public interface IGameObject
    {
       
        public double MinX { get; }
        public double MaxX { get; }
        public double MinY { get; }
        public double MaxY { get; }
        public string Name { get; set; }
        public Position Position { get; set; }
        public Sprite Sprite { get; set; }
        public Collision Collision { get; set; }
        public Command Command { get; set; }
        public Camera Camera { get; set; }
    }
}
