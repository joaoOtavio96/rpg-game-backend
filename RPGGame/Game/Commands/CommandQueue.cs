namespace RPGGame.Game
{
    public class CommandQueue
    {
        public CommandQueue()
        {
            KeysPressed = new Queue<Key>();
        }

        public Queue<Key> KeysPressed { get; private set; }
        public Key CurrentKey { get; private set; }

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
