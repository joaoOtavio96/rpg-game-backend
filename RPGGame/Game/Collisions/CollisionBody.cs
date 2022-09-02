using RPGGame.Config;

namespace RPGGame.Game.Collisions
{
    public class CollisionBody
    {
        public CollisionBody()
        {
            Id = Guid.NewGuid().ToString().Substring(0, 5);
        }

        public string Id { get; set; }
        public Position Position { get; set; }
        public Bounds Bounds => new Bounds(Position.RelativeX, Position.RelativeY);
        public IGameObject GameObject { get; set; }
    }
}
