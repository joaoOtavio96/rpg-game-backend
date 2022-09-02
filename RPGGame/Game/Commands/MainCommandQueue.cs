using RPGGame.Game.Commands.Intents;

namespace RPGGame.Game
{
    public class MainCommandQueue
    {
        public MainCommandQueue()
        {
            KeysPressed = new Queue<Key>();
            CommandMap = new CommandKeyMap()
                .AddMapType(Key.W, typeof(MoveUpCommandIntent))
                .AddMapType(Key.S, typeof(MoveDownCommandIntent))
                .AddMapType(Key.A, typeof(MoveLeftCommandIntent))
                .AddMapType(Key.D, typeof(MoveRightCommandIntent))
                .AddMapType(Key.Default, typeof(IdleCommandIntent));

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

        public CommandIntent GetCommand(IGameObject gameObject)
        {
            if (!gameObject.Camera.Main)
                return new IdleCommandIntent(gameObject);

            return (CommandIntent)Activator.CreateInstance(CommandMap.MapType[CurrentKey], gameObject);
        }
    }
}
