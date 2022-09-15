using Microsoft.Extensions.Options;
using RPGGame.Config;
using RPGGame.Game.Animations;
using RPGGame.Game.Animations.Frames;
using RPGGame.Game.Cameras;
using RPGGame.Game.Collisions;
using RPGGame.Game.Commands;
using RPGGame.Game.Config;
using RPGGame.Infrastructure;
using SixLabors.ImageSharp;
using System.Text.Json;

namespace RPGGame.Game
{
    public class Game : IGame
    {
        private readonly MainCommandQueue _commands;
        private readonly CollisionService _collisionService;
        private readonly CommandService _commandService;
        private readonly CameraService _cameraService;
        private Person Hero, Npc;
        private Map Map;
        private object State;

        public Game(MainCommandQueue commands, CollisionService collisionService, CommandService commandService, CameraService cameraService)
        {
            _commands = commands;
            _collisionService = collisionService;
            _commandService = commandService;
            _cameraService = cameraService;           
        }

        public void Init()
        {
            Map = new GameObjectBuilder(@"Assets\config\MapGameObjectConfig.json")
                .Build<Map>();

            Hero = new GameObjectBuilder(@"Assets\config\HeroGameObjectConfig.json")
                .Build<Person>(o => o.Camera.Main = true);

            Npc = new GameObjectBuilder(@"Assets\config\NpcGameObjectConfig.json")
                .Build<Person>();
        }

        public void Update(double deltaTime)
        {
            var gameObjects = new List<IGameObject> { Hero, Npc, Map };

            _collisionService.CheckCollision(gameObjects);
            _commandService.ExecuteCommand(gameObjects, () => _commands.GetKey());
            _cameraService.SetPositions(gameObjects);

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
                    X = gameObject.Position.RelativeX,
                    Y = gameObject.Position.RelativeY,
                    MinX = gameObject.Bounds.MinX,
                    MinY = gameObject.Bounds.MinY,
                    MaxX = gameObject.Bounds.MaxX,
                    MaxY = gameObject.Bounds.MaxY,
                    CollisionBodies = gameObject.Collision.CollisionBodies.Select(c => new CollisionBodyDto
                    {
                        Id = c.Id,
                        RelativeX = c.Position.RelativeX,
                        RelativeY = c.Position.RelativeY,
                    }).ToList()
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
