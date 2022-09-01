using RPGGame.Infrastructure;

namespace RPGGame.Game.Commands
{
    public class CommandService
    {
        public List<CommandIntent> ExecuteCommand(List<ObjectToProcess> objectsToProcess)
        {
            var commandsProcessed = new List<CommandIntent>();

            foreach (var objectToProccess in objectsToProcess)
            {
                var command = objectToProccess.GameObject.Command?.CommandMap?.GetCommand(objectToProccess.Key);
                
                if (command is null)
                    continue;   

                if ((objectToProccess.GameObject.Collision.ObjectsWithCollision.Any() && objectToProccess.GameObject.Command.DirectionLatch))
                {
                    objectToProccess.Completed();
                    objectToProccess.GameObject.Collision.ObjectsWithCollision.Clear();
                    continue;
                }

                if (command.Condition(objectToProccess.Key))
                {
                    command.Action();
                }

                command.KeyToProccess = objectToProccess.Key;
                objectToProccess.GameObject.Command.OnProccessing(command, objectToProccess.Completed);

                commandsProcessed.Add(command);
            }

            return commandsProcessed;
        }
    }
}
