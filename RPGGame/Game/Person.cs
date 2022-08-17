﻿using RPGGame.Config;

namespace RPGGame.Game
{
    public class Person : IGameObject, ICommandObject, ICameraObject, ICollisionObject
    {
        public Person()
        {

        }
        public Person(string name, string path, double x, double y, int width, int height, int gridWidth, int gridHeight)
        {
            LastDirection = "Down";
            LastKey = Key.Default;
            DirectionLatch = false;
            MovementProgress = MovementConfig.MovementProgress;
            MovementLimit = MovementConfig.MovementLimit;
            var calculatedX = MapConfig.ConvertToPixel(x) + MapConfig.ConvertToPixel(6) * (-1) - (MapConfig.GridSize / 2);
            var calculatedY = MapConfig.ConvertToPixel(y) + MapConfig.ConvertToPixel(7) * (-1) - (MapConfig.GridSize / 8);
            DeltaX += calculatedX;
            DeltaY += calculatedY;
            X = calculatedX;
            Y = calculatedY;
            Name = name;
            CommandMap = new CommandKeyMap()
                .AddMap(new KeyValuePair<Key, Command>(Key.W, new MoveUpCommand(this)))
                .AddMap(new KeyValuePair<Key, Command>(Key.S, new MoveDownCommand(this)))
                .AddMap(new KeyValuePair<Key, Command>(Key.A, new MoveLeftCommand(this)))
                .AddMap(new KeyValuePair<Key, Command>(Key.D, new MoveRightCommand(this)))
                .AddMap(new KeyValuePair<Key, Command>(Key.Default, new IdleCommand(this)));

            Sprite = new Sprite(path, width, height, gridWidth, gridHeight);
        }

        public CommandKeyMap CommandMap { get; set; }
        public double MovementLimit { get; private set; }
        public double MovementProgress { get; private set; }
        public string LastDirection { get; private set; }
        public Key LastKey { get; private set; }
        public bool DirectionLatch { get; private set; }
        public bool MovementCompleted => MovementLimit <= 0;
        public bool Main { get; set; }
        public double DeltaX { get; set; }
        public double DeltaY { get; set; }
        public double RelativeX { get; set; }
        public double RelativeY { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public Sprite Sprite { get; set; }
        public string Name { get; set; }
        public bool HasCollision { get; set; }
        public double MinX => RelativeX + 8.5;
        public double MaxX => MinX + MapConfig.GridSize - 1;
        public double MinY => RelativeY + 16 + (MapConfig.GridSize / 8);
        public double MaxY => MinY + MapConfig.GridSize - 1;

        public void OnProccessing(Command command, Action completed)
        {
            var movement = command as IMovementTypeObject;
            if (MovementCompleted)
            {
                StopMovement();
                completed();
            }

            if (!string.IsNullOrWhiteSpace(movement.Direction))
                LastDirection = movement.Direction;

            LastKey = command.KeyToProccess;
            UpdateMovement();
            Sprite.UpdateAnimation(movement.Animation());
        } 

        public void UpdateMovement()
        {
            if (MovementCompleted)
                return;
            
            DirectionLatch = true;            
            MovementLimit -= MovementProgress;                       
        }

        public void StopMovement()
        {
            DirectionLatch = false;
            HasCollision = false;
            LastKey = Key.Default;
            MovementLimit = MovementConfig.MovementLimit;
            Sprite.Animation.ResetFrame();
        }
    }
}
