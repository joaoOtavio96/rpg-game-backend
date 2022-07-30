namespace RPGGame.Game.Commands
{
    public static class CommandProcessor
    {
        public static Command Proccess(ICommandObject commandObject, Key keyToProccess, Action completed)
        {
            var command = commandObject.CommandMap.GetCommand(keyToProccess);

            if (command.Condition(keyToProccess))
            {
                command.Action();
            }

            command.KeyToProccess = keyToProccess;
            commandObject.OnProccessing(command, completed);

            return command;
        }
    }
}
