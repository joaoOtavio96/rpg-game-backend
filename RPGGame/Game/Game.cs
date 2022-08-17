using RPGGame.Config;
using RPGGame.Game.Commands;
using RPGGame.Infrastructure;
using SixLabors.ImageSharp;

namespace RPGGame.Game
{
    public class Game : IGame
    {
        private readonly CommandQueue _commands;
        private NpcCommadQueue _npcCommands;
        private Person Hero, Npc;
        private Map Map;
        private object State;


        public Game(CommandQueue commands)
        {
            _commands = commands;
        }

        public void Init()
        {
            Map = new Map("Map", @"Assets\maps\DemoLower.png", 192, 192, 16, 16);

            Hero = new Person("Hero", @"Assets\characters\people\hero.png", 6, 7, 128, 128, 32, 32);
            Hero.Main = true;
            Hero.Sprite.Animation = new Animation()
                .AddAnimation("IdleUp", new List<Point> { new Point(0, 2) })
                .AddAnimation("IdleDown", new List<Point> { new Point(0, 0) })
                .AddAnimation("IdleLeft", new List<Point> { new Point(0, 3) })
                .AddAnimation("IdleRight", new List<Point> { new Point(0, 1) })
                .AddAnimation("WalkUp", new List<Point> { new Point(0, 2), new Point(1, 2), new Point(2, 2), new Point(3, 2) })
                .AddAnimation("WalkDown", new List<Point> { new Point(0, 0), new Point(1, 0), new Point(2, 0), new Point(3, 0) })
                .AddAnimation("WalkLeft", new List<Point> { new Point(0, 3), new Point(1, 3), new Point(2, 3), new Point(3, 3) })
                .AddAnimation("WalkRight", new List<Point> { new Point(0, 1), new Point(1, 1), new Point(2, 1), new Point(3, 1) });

            Npc = new Person("Npc", @"Assets\characters\people\npc1.png", 7, 9, 128, 128, 32, 32);
            Npc.Sprite.Animation = new Animation()
                .AddAnimation("IdleUp", new List<Point> { new Point(0, 2) })
                .AddAnimation("IdleDown", new List<Point> { new Point(0, 0) })
                .AddAnimation("IdleLeft", new List<Point> { new Point(0, 3) })
                .AddAnimation("IdleRight", new List<Point> { new Point(0, 1) })
                .AddAnimation("WalkUp", new List<Point> { new Point(0, 2), new Point(1, 2), new Point(2, 2), new Point(3, 2) })
                .AddAnimation("WalkDown", new List<Point> { new Point(0, 0), new Point(1, 0), new Point(2, 0), new Point(3, 0) })
                .AddAnimation("WalkLeft", new List<Point> { new Point(0, 3), new Point(1, 3), new Point(2, 3), new Point(3, 3) })
                .AddAnimation("WalkRight", new List<Point> { new Point(0, 1), new Point(1, 1), new Point(2, 1), new Point(3, 1) });

            _npcCommands = new NpcCommadQueue(new List<Key> { Key.Default });
        }

        public void Update(double deltaTime)
        {
            var objectsToProcess = new List<Tuple<IGameObject, Key, Action>>
            {
                new Tuple<IGameObject, Key, Action>(Hero, _commands.CurrentKey, () => _commands.GetKey()),
                new Tuple<IGameObject, Key, Action>(Npc, (Key)_npcCommands.Current, () => _npcCommands.MoveNext()),
                new Tuple<IGameObject, Key, Action>(Map, Key.Default, () => { })
            };

            CommandProcessor.Proccess(objectsToProcess);
            Camera.SetPositions(new List<ICameraObject> { Hero, Map, Npc });

            State = new
            {
                GameObjects = CreateGameObjetcs(Map, Npc, Hero),
                CommandsPressed = _commands.KeysPressed.Count()
            };
        }

        private List<GameObjectDto> CreateGameObjetcs(params IGameObject[] gameObjects)
        {
            var gameObjectsDto = new List<GameObjectDto>();
            foreach (var gameObject in gameObjects)
            {
                var gameObjectDto = new GameObjectDto
                {
                    Name = gameObject.Name,
                    Sprite = gameObject.Sprite.Image,
                    X = gameObject.RelativeX,
                    Y = gameObject.RelativeY,
                    MinX = gameObject.MinX,
                    MinY = gameObject.MinY,
                    MaxX = gameObject.MaxX,
                    MaxY = gameObject.MaxY,
                    HasCollision = gameObject.HasCollision,
                };

                gameObjectsDto.Add(gameObjectDto);
            }

            return gameObjectsDto;
        }

        public object GetState()
        {
            return State;
        }
    }
}
