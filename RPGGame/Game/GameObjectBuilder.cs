using RPGGame.Game.Animations.Frames;
using RPGGame.Game.Collisions;
using RPGGame.Game.Config;
using System.Reflection;
using System.Text.Json;

namespace RPGGame.Game
{
    public class GameObjectBuilder
    {
        public GameObjectBuilder(string configPath)
        {
            var jsonString = File.ReadAllText(configPath);
            Configuration = JsonSerializer.Deserialize<JsonConfig>(jsonString);

            Builders = new Dictionary<Type, IGameObject>
            {
                { typeof(Map), MapBuilder.Build(Configuration.Name, Configuration.Sprite.Width, Configuration.Sprite.Height) },
                { typeof(Person), PersonBuilder.Build(Configuration.Name, Configuration.InitialX, Configuration.InitialY) }
            };
        }

        public JsonConfig Configuration { get; set; }
        public Dictionary<Type, IGameObject> Builders { get; set; }

        public T Build<T>(Action<IGameObject> options = null) where T : IGameObject
        {
            var gameObject = Builders.GetValueOrDefault(typeof(T));

            if(Configuration.Sprite != null)
            {
                gameObject.Sprite = new Sprite(Configuration.Sprite.Path, Configuration.Sprite.Width, Configuration.Sprite.Height, Configuration.Sprite.GridWidth, Configuration.Sprite.GridHeight);
            }

            if(Configuration.Animations != null)
            {
                gameObject.Sprite.Animation = new Animations.Animation();

                foreach (var animation in Configuration.Animations)
                {
                    var path = typeof(Frame).Namespace;
                    var frames = animation.Frames.Select(f => (Frame)Activator.CreateInstance(Type.GetType($"{path}.{f}"))).ToArray();
                    gameObject.Sprite.Animation.AddAnimation(animation.Name, frames);
                }               
            }

            if(Configuration.CollisionPoints != null)
            {
                gameObject.Collision = new Collision(gameObject);
                gameObject.Collision.Static = true;

                foreach (var collision in Configuration.CollisionPoints)
                {
                    gameObject.Collision.AddCollisionBody(collision.X, collision.Y);
                }
            }

            if(options != null)
            {
                options(gameObject);
            }

            return (T)gameObject;
        }
    }

    public static class MapBuilder
    {
        public static Map Build(string name, int width, int height)
        {
            return new Map(name, width, height);
        }
    }

    public static class PersonBuilder
    {
        public static Person Build(string name, double x, double y)
        {
            return new Person(name, x, y);
        }
    }
}
