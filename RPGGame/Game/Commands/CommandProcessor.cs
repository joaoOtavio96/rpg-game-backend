using RPGGame.Infrastructure;

namespace RPGGame.Game.Commands
{
    public static class CommandProcessor
    {
        public static List<Command> Process(List<ObjectToProcess> objectsToProcess)
        {
            var commandsProcessed = new List<Command>();

            foreach (var objectToProccess in objectsToProcess)
            {
                var command = objectToProccess.GameObject.CommandMap?.GetCommand(objectToProccess.Key);
                
                if (command is null)
                    continue;   

                if ((objectToProccess.GameObject.ObjectsWithCollision.Any() && objectToProccess.GameObject.DirectionLatch))
                {
                    objectToProccess.Completed();
                    objectToProccess.GameObject.ObjectsWithCollision.Clear();
                    continue;
                }

                if (command.Condition(objectToProccess.Key))
                {
                    command.Action();
                }

                command.KeyToProccess = objectToProccess.Key;
                objectToProccess.GameObject.OnProccessing(command, objectToProccess.Completed);

                commandsProcessed.Add(command);
            }

            return commandsProcessed;
        }
    }
}
