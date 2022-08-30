using RPGGame.Config;

namespace RPGGame.Game
{
    public class MoveDownCommand : Command, IMovementTypeObject
    {
        public MoveDownCommand(Person person)
        {
            GameObject = person;
            Direction = "Down";
            Animation = () => "WalkDown";
            Condition = (key) => key is Key.S || (person.LastKey is Key.S && person.DirectionLatch);
            Action = () => MoveDown() ;
        }

        public string Direction { get; set; }
        public Func<string> Animation { get; set; }

        public void MoveDown()
        {
            GameObject.Y += GameObject.MovementProgress;
            GameObject.DeltaY += GameObject.MovementProgress;
        }

        public override IGameObject NextPosition()
        {
            return new Person 
            { 
                RelativeX = GameObject.RelativeX,
                RelativeY = GameObject.RelativeY + MapConfig.GridSize 
            };
        }
    }
}
