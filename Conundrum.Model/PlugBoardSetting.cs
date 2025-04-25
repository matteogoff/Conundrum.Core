using System.Collections;

namespace Conundrum.Model
{
    /// <summary>
    /// Represents the settings for a plugboard.
    /// </summary>
    [Serializable]
    public class PlugBoardSetting : IEnumerable<KeyValuePair<char, char>>
    {
        private Dictionary<char, char> _plugBoard = new Dictionary<char, char>();

        public PlugBoardSetting() { }

        public PlugBoardSetting(Dictionary<char, char> input)
        {
            if (input != null)
            {
                foreach (var item in input)
                {
                    this.Add(item.Key, item.Value);
                }
            }
        }

        public void Add(char a, char b)
        {
            if (a == b)
            {
                throw new ArgumentException("Cannot map a character to itself.");
            }
            if (_plugBoard.ContainsKey(a) || _plugBoard.ContainsKey(b))
            {
                throw new ArgumentException("One of the characters is already mapped.");
            }
            _plugBoard.Add(a, b);
        }

        /// <summary>
        /// Gets a readonly copy of the PlugBoard.
        /// </summary>
        public Dictionary<char, char> PlugBoard
        {
            get
            {
                return new Dictionary<char, char>(_plugBoard);
            }
        }

        public IEnumerator<KeyValuePair<char, char>> GetEnumerator()
        {
            return _plugBoard.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}