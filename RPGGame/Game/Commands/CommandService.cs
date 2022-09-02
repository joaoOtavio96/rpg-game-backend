using RPGGame.Game.Commands.Intents;
using RPGGame.Infrastructure;

namespace RPGGame.Game.Commands
{
    public class CommandService
    {
        private readonly MainCommandQueue _commandQueue;
        public CommandService(MainCommandQueue commandQueue)
        {
            _commandQueue = commandQueue;
        }

        public List<CommandIntent> ExecuteCommand(List<ObjectToProcess> objectsToProcess)
        {
            var commandsProcessed = new List<CommandIntent>();

            foreach (var objectToProccess in objectsToProcess)
            {
                var commandIntent = _commandQueue.GetCommand(objectToProccess.GameObject);
                
                if (commandIntent is null)
                    continue;   

                if ((objectToProccess.GameObject.Collision.ObjectsWithCollision.Any() && objectToProccess.GameObject.Command.DirectionLatch))
                {
                    objectToProccess.Completed();
                    objectToProccess.GameObject.Collision.ObjectsWithCollision.Clear();
                    continue;
                }

                if (commandIntent.Condition(_commandQueue.CurrentKey))
                {
                    commandIntent.Action();
                }

                commandIntent.KeyToProccess = _commandQueue.CurrentKey;
                objectToProccess.GameObject.Command?.OnProccessing(commandIntent, objectToProccess.Completed);

                commandsProcessed.Add(commandIntent);
            }

            return commandsProcessed;
        }

        public List<CommandIntent> ExecuteCommand(List<IGameObject> objectsToProcess, Action completed)
        {
            var commandsProcessed = new List<CommandIntent>();

            foreach (var objectToProccess in objectsToProcess)
            {
                var commandIntent = _commandQueue.GetCommand(objectToProccess);

                if (commandIntent is null)
                    continue;

                if ((objectToProccess.Collision.ObjectsWithCollision.Any() && objectToProccess.Command.DirectionLatch))
                {
                    if(objectToProccess.Camera.Main)
                        completed();

                    objectToProccess.Collision.ObjectsWithCollision.Clear();
                    continue;
                }

                if (commandIntent.Condition(_commandQueue.CurrentKey))
                {
                    commandIntent.Action();
                }

                commandIntent.KeyToProccess = _commandQueue.CurrentKey;
                objectToProccess.Command?.OnProccessing(commandIntent, completed);

                commandsProcessed.Add(commandIntent);
            }

            return commandsProcessed;
        }
    }
}
