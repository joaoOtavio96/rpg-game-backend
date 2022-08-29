using RPGGame.Infrastructure;

namespace RPGGame.Game.Commands
{
    public static class CommandProcessor
    {
        public static List<Command> Process(List<ObjectToProcess> objectsToProcess)
        {
            var commandsProcessed = new List<Command>();

            foreach (var objectToProccess in objectsToProcess.Where(o => o.IsCommandObject()))
            {
                var command = objectToProccess.CommandObject.CommandMap.GetCommand(objectToProccess.Key);

                if ((objectToProccess.CollisionObject.ObjectsWithCollision.Any() && objectToProccess.CommandObject.DirectionLatch))
                {
                    objectToProccess.Completed();
                    objectToProccess.CollisionObject.ObjectsWithCollision.Clear();
                    continue;
                }
                else
                {
                    
                }

                if (command.Condition(objectToProccess.Key))
                {
                    command.Action();
                }

                command.KeyToProccess = objectToProccess.Key;
                objectToProccess.CommandObject.OnProccessing(command, objectToProccess.Completed);

                commandsProcessed.Add(command);
            }

            return commandsProcessed;
        }
    }
}
