
using RPGGame.Game.Commands.Intents;

namespace RPGGame.Game
{
    public class CommandKeyMap
    {
        public CommandKeyMap()
        {
            Map = new Dictionary<Key, CommandIntent>();
            MapType = new Dictionary<Key, Type>();
        }
        public Dictionary<Key, CommandIntent> Map { get; private set; }
        public Dictionary<Key, Type> MapType { get; private set; }

        public CommandKeyMap AddMap(KeyValuePair<Key, CommandIntent> map)
        {
            Map.Add(map.Key, map.Value);

            return this;
        }

        public CommandKeyMap AddMapType(Key key, Type map)
        {
            MapType.Add(key, map);

            return this;
        }

        public CommandIntent GetCommand(Key key)
        {
            return Map.GetValueOrDefault(key);
        }
    }
}
