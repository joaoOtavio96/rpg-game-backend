using RPGGame.Config;

namespace RPGGame.Game.Collisions
{
    public class Collision
    {
        private readonly IGameObject _gameObject;
        public Collision(IGameObject gameObject)
        {
            ObjectsWithCollision = new List<CollisionBody>();
            CollisionBodies = new List<CollisionBody>();
            _gameObject = gameObject;
        }

        public virtual void UpdateColisionBodyPosition()
        {
            CollisionBodies.Clear();
            CollisionBodies.Add(new CollisionBody
            {
                GameObject = _gameObject,
                RelativeX = _gameObject.RelativeX,
                RelativeY = _gameObject.RelativeY,
                X = _gameObject.X,
                Y = _gameObject.Y
            });
        }

        public virtual void AddCollisionBody(double x, double y)
        {
            var collisionBody = new CollisionBody
            {
                GameObject = _gameObject,
                X = MapConfig.ConvertToPixel(x) + MapConfig.ConvertToPixel(6) * -1 - MapConfig.GridSize / 2,
                Y = MapConfig.ConvertToPixel(y) + MapConfig.ConvertToPixel(7) * -1 - MapConfig.GridSize / 8,
            };

            CollisionBodies.Add(collisionBody);
        }

        public bool Static { get; set; }
        public List<CollisionBody> ObjectsWithCollision { get; set; }
        public List<CollisionBody> CollisionBodies { get; set; }
    }
}
