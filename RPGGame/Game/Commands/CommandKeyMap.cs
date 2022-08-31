
namespace RPGGame.Game
{
    public class CommandKeyMap
    {
        public CommandKeyMap()
        {
            Map = new Dictionary<Key, CommandIntent>();
        }
        public Dictionary<Key, CommandIntent> Map { get; private set; }

        public CommandKeyMap AddMap(KeyValuePair<Key, CommandIntent> map)
        {
            Map.Add(map.Key, map.Value);

            return this;
        }

        public CommandIntent GetCommand(Key key)
        {
            return Map.GetValueOrDefault(key);
        }
    }
}
