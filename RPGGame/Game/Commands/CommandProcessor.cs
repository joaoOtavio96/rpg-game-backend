namespace RPGGame.Game
{
    public class CommandProcessor
    {
        public CommandProcessor()
        {
            KeysPressed = new Queue<Key>();
        }

        public Queue<Key> KeysPressed { get; private set; }

        public void AddKey(string key)
        {
            Enum.TryParse(key.ToUpper(), out Key keyPressed);

            if (keyPressed != Key.Default)
                KeysPressed.Enqueue(keyPressed);
        }

        public Key GetKey()
        {
            if(KeysPressed.Count <= 0)
                return Key.Default;

            return KeysPressed.Dequeue();
        }
        
        public Command Proccess(ICommandObject commandObject, Key keyToProccess, Action completed)
        {
            var command = commandObject.CommandMap.Map.GetValueOrDefault(keyToProccess);

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
