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

        /// <summary>
        /// Initializes a new instance of the <see cref="PlugBoardSetting"/> class.
        /// </summary>
        /// <remarks>This constructor creates a default instance of the <see cref="PlugBoardSetting"/>
        /// class. Use this to configure plugboard settings for specific applications.</remarks>
        public PlugBoardSetting() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlugBoardSetting"/> class with the specified character
        /// mappings.
        /// </summary>
        /// <remarks>Each key-value pair in the <paramref name="input"/> dictionary is added to the <see
        /// cref="PlugBoardSetting"/> instance. If <paramref name="input"/> is <see langword="null"/>, no mappings are
        /// added.</remarks>
        /// <param name="input">A dictionary containing character mappings where the key represents the input character and the value
        /// represents the corresponding output character.</param>
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

        /// <summary>
        /// Gets the character mapped to the specified key in the plugboard.
        /// </summary>
        /// <remarks>This indexer provides access to the plugboard mapping, which associates input
        /// characters with their corresponding mapped characters. If the specified key is not found, a <see
        /// cref="KeyNotFoundException"/> is thrown.</remarks>
        /// <param name="key">The input character to look up in the plugboard mapping.</param>
        /// <returns>The character mapped to the specified key.</returns>
        /// <exception cref="KeyNotFoundException">Thrown if the specified <paramref name="key"/> does not exist in the plugboard mapping.</exception>
        public char this[char key]
        {
            get
            {
                if (_plugBoard.TryGetValue(key, out char value))
                {
                    return value;
                }
                throw new KeyNotFoundException($"The key '{key}' was not found in the plugboard mapping.");
            }
        }


        /// <summary>
        /// Determines whether the specified key exists in the plugboard mapping.
        /// </summary>
        /// <param name="key">The character key to locate in the plugboard mapping.</param>
        /// <returns><see langword="true"/> if the specified key exists in the plugboard mapping; otherwise, <see
        /// langword="false"/>.</returns>
        public bool ContainsKey(char key)
        {
            return _plugBoard.ContainsKey(key);
        }

        /// <summary>
        /// Adds a mapping between two characters to the plugboard.
        /// </summary>
        /// <remarks>This method establishes a bidirectional mapping between the two specified characters.
        /// Once mapped, neither character can be mapped again.</remarks>
        /// <param name="a">The first character to map.</param>
        /// <param name="b">The second character to map.</param>
        /// <exception cref="ArgumentException">Thrown if <paramref name="a"/> and <paramref name="b"/> are the same character,  or if either character is
        /// already mapped.</exception>
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

        /// <summary>
        /// Returns an enumerator that iterates through the collection of key-value pairs in the plugboard.
        /// </summary>
        /// <remarks>The enumerator provides read-only access to the mappings in the plugboard. Use this
        /// method to iterate over the configured character mappings.</remarks>
        /// <returns>An enumerator for the collection of <see cref="KeyValuePair{TKey, TValue}"/> objects,  where each pair
        /// represents a mapping of characters in the plugboard.</returns>
        public IEnumerator<KeyValuePair<char, char>> GetEnumerator()
        {
            return _plugBoard.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the plugboard settings.
        /// </summary>
        /// <remarks>This method is an explicit implementation of the <see
        /// cref="IEnumerable.GetEnumerator"/> method  and delegates to the type-specific <c>GetEnumerator</c> method of
        /// the collection.</remarks>
        /// <returns>An <see cref="IEnumerator"/> that can be used to iterate through the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Gets the number of elements in the plug board.
        /// </summary>
        public int Count { get { return _plugBoard.Count; } }
    }
}