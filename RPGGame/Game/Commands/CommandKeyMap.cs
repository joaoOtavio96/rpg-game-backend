
namespace RPGGame.Game
{
    public class CommandKeyMap
    {
        public CommandKeyMap()
        {
            Map = new Dictionary<Key, Command>();
        }
        public Dictionary<Key, Command> Map { get; private set; }

        public CommandKeyMap AddMap(KeyValuePair<Key, Command> map)
        {
            Map.Add(map.Key, map.Value);

            return this;
        }

        public Command GetCommand(Key key)
        {
            return Map.GetValueOrDefault(key);
        }
    }
}
