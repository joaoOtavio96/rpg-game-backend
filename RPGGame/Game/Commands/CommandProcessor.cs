using RPGGame.Infrastructure;

namespace RPGGame.Game.Commands
{
    public static class CommandProcessor
    {

        public static List<Command> Proccess(List<Tuple<IGameObject, Key, Action>> objectsToProccess)
        {
            var commandsProcessed = new List<Command>();
            foreach (var objectToProccess in objectsToProccess.Where(o => o.Item1 is ICommandObject).Select(o => new Tuple<ICommandObject, Key, Action>(o.Item1 as ICommandObject, o.Item2, o.Item3)))
            {
                var command = objectToProccess.Item1.CommandMap.GetCommand(objectToProccess.Item2);

                var combinations = objectsToProccess
                    .Where(o => o.Item1 is ICollisionObject)
                    .Select(o => new Tuple<ICollisionObject, Key, Action>(o.Item1 as ICollisionObject, o.Item2, o.Item3))
                    .GetPermutations(2);

                foreach (var combination in combinations)
                {
                    var ob1 = (objectsToProccess.FirstOrDefault(o => o.Item1 == combination.First().Item1).Item1 as ICommandObject).CommandMap.GetCommand(objectToProccess.Item2);
                    var ob2 = (objectsToProccess.FirstOrDefault(o => o.Item1 == combination.Last().Item1).Item1 as ICommandObject).CommandMap.GetCommand(combination.Last().Item2);

                    combination.First().Item1.HasCollision = Intersect(ob1.NextPosition(), ob2.NextPosition());
                }

                if ((objectToProccess.Item1 as ICollisionObject).HasCollision)
                {
                    
                }

                if (command.Condition(objectToProccess.Item2))
                {
                    command.Action();
                }

                command.KeyToProccess = objectToProccess.Item2;
                objectToProccess.Item1.OnProccessing(command, objectToProccess.Item3);

                commandsProcessed.Add(command);
            }

            return commandsProcessed;
        }

        public static bool Intersect(IGameObject obj1, IGameObject obj2)
        {
            return (obj1.MinX <= obj2.MaxX && obj1.MaxX >= obj2.MinX) &&
                    (obj1.MinY <= obj2.MaxY && obj1.MaxY >= obj2.MinY);
        }
    }
}
