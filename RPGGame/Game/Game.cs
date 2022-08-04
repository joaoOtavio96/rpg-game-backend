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
        private Sprite Map;
        private object State;


        public Game(CommandQueue commands)
        {
            _commands = commands;
        }

        public void Init()
        {
            Map = new Sprite(@"Assets\maps\DemoLower.png", 
                x: (GameConfig.CanvasWidth / 2) + MapConfig.ConvertToPixel(6) * (-1),
                y: (GameConfig.CanvasHeight / 2) + MapConfig.ConvertToPixel(6) * (-1),
                0,
                0);

            Hero = new Person(@"Assets\characters\people\hero.png",
                (GameConfig.CanvasWidth / 2) + MapConfig.ConvertToPixel(6) + MapConfig.ConvertToPixel(6) * (-1) - (MapConfig.GridSize / 2),
                (GameConfig.CanvasHeight / 2) + MapConfig.ConvertToPixel(7) + MapConfig.ConvertToPixel(7) * (-1) - (MapConfig.GridSize / 8), 32, 32);
            Hero.Animation = new Animation()
                .AddAnimation("IdleUp", new List<Point> { new Point(0, 2) })
                .AddAnimation("IdleDown", new List<Point> { new Point(0, 0) })
                .AddAnimation("IdleLeft", new List<Point> { new Point(0, 3) })
                .AddAnimation("IdleRight", new List<Point> { new Point(0, 1) })
                .AddAnimation("WalkUp", new List<Point> { new Point(0, 2), new Point(1, 2), new Point(2, 2), new Point(3, 2) })
                .AddAnimation("WalkDown", new List<Point> { new Point(0, 0), new Point(1, 0), new Point(2, 0), new Point(3, 0) })
                .AddAnimation("WalkLeft", new List<Point> { new Point(0, 3), new Point(1, 3), new Point(2, 3), new Point(3, 3) })
                .AddAnimation("WalkRight", new List<Point> { new Point(0, 1), new Point(1, 1), new Point(2, 1), new Point(3, 1) });

            Npc = new Person(@"Assets\characters\people\npc1.png",
                 MapConfig.ConvertToPixel(7) + MapConfig.ConvertToPixel(6) * (-1) - (MapConfig.GridSize / 2),
                 MapConfig.ConvertToPixel(9) + MapConfig.ConvertToPixel(7) * (-1) - (MapConfig.GridSize / 8), 32, 32);
            Npc.Animation = new Animation()
                .AddAnimation("IdleUp", new List<Point> { new Point(0, 2) })
                .AddAnimation("IdleDown", new List<Point> { new Point(0, 0) })
                .AddAnimation("IdleLeft", new List<Point> { new Point(0, 3) })
                .AddAnimation("IdleRight", new List<Point> { new Point(0, 1) })
                .AddAnimation("WalkUp", new List<Point> { new Point(0, 2), new Point(1, 2), new Point(2, 2), new Point(3, 2) })
                .AddAnimation("WalkDown", new List<Point> { new Point(0, 0), new Point(1, 0), new Point(2, 0), new Point(3, 0) })
                .AddAnimation("WalkLeft", new List<Point> { new Point(0, 3), new Point(1, 3), new Point(2, 3), new Point(3, 3) })
                .AddAnimation("WalkRight", new List<Point> { new Point(0, 1), new Point(1, 1), new Point(2, 1), new Point(3, 1) });

            _npcCommands = new NpcCommadQueue(new List<Key> { Key.D, Key.D, Key.D, Key.A, Key.A, Key.A });
        }

        public void Update(double deltaTime)
        {
            CommandProcessor.Proccess(Hero, _commands.CurrentKey, () => _commands.GetKey());
            CommandProcessor.Proccess(Npc, (Key)_npcCommands.Current, () => _npcCommands.MoveNext());

            var gameObjects = Camera.SetPositions(Npc, new List<Sprite> { Map, Hero });

            State = new
            {
                GameObjects = gameObjects,
                CommandsPressed = _commands.KeysPressed.Count()
            };
        }

        public object GetState()
        {
            return State;
        }
    }
}
