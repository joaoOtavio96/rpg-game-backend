using System.Collections;

namespace RPGGame.Game.Commands
{
    public class NpcCommadQueue : IEnumerator
    {
        public NpcCommadQueue(IList<Key> keys)
        {
            _keys = keys;
        }

        private IList<Key> _keys;
        private int _index;

        public object Current => _keys[_index];

        public bool MoveNext()
        {
            _index++;
            if (_keys.ElementAtOrDefault(_index) == Key.Default)
            {
                Reset();
                return false;
            }     

            return true;
        }

        public void Reset()
        {
            _index = 0;
        }
    }
}
