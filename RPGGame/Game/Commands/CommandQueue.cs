namespace RPGGame.Game
{
    public class CommandQueue
    {
        public CommandQueue()
        {
            KeysPressed = new Queue<Key>();
            //CommandMap = new CommandKeyMap()
            //    .AddMap(new KeyValuePair<Key, CommandIntent>(Key.W, new MoveUpCommandIntent(this)))
            //    .AddMap(new KeyValuePair<Key, CommandIntent>(Key.S, new MoveDownCommandIntent(this)))
            //    .AddMap(new KeyValuePair<Key, CommandIntent>(Key.A, new MoveLeftCommandIntent(this)))
            //    .AddMap(new KeyValuePair<Key, CommandIntent>(Key.D, new MoveRightCommandIntent(this)))
            //    .AddMap(new KeyValuePair<Key, CommandIntent>(Key.Default, new IdleCommandIntent(this)));

        }

        public Queue<Key> KeysPressed { get; private set; }
        public Key CurrentKey { get; private set; }
        public CommandKeyMap CommandMap { get; set; }

        public void AddKey(string key)
        {
            Enum.TryParse(key.ToUpper(), out Key keyPressed);

            if (keyPressed != Key.Default)
                KeysPressed.Enqueue(keyPressed);
        }

        public void GetKey()
        {
            if(KeysPressed.Count <= 0)
            {
                CurrentKey = Key.Default;
                return;
            }             

            CurrentKey = KeysPressed.Dequeue();
        }

        public void ClearKeys()
        {
            KeysPressed.Clear();
        }
    }
}
