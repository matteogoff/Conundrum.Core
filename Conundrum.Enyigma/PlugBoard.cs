using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Conundrum.Model;

namespace Conundrum.Enigma
{
    /// <summary>
    /// Represents the plugboard of the Enigma machine.
    /// </summary>
    public class PlugBoard : IEnumerable<KeyValuePair<char, char>>
    {

        //? Create a way to map a lis of entries where char 1 maps to char 2 and vise versa
        private Dictionary<char, char> _map = new Dictionary<char, char>();

        /// <summary>
        /// Creates a new plugboard for the Enigma machine.
        /// </summary>
        public PlugBoard() { }

        /// <summary>
        /// Creates a new plugboard for the Enigma machine.
        /// </summary>
        /// <param name="settings"></param>
        /// <exception cref="ArgumentException"></exception>
        public PlugBoard(PlugBoardSetting settings) 
        {
            foreach (var entry in settings?.PlugBoard)
            {
                this.Add(entry.Key, entry.Value);
            }
        }

        /// <summary>
        /// Creates a new plugboard for the Enigma machine using a dictionary of mappings.
        /// </summary>
        /// <param name="mappings">A dictionary containing character mappings.</param>
        /// <exception cref="ArgumentException"></exception>
        public PlugBoard(Dictionary<char, char> mappings)
        {
            if (mappings == null)
            {
                throw new ArgumentNullException(nameof(mappings));
            }

            foreach (var entry in mappings)
            {
                this.Add(entry.Key, entry.Value);
            }
        }


        /// <summary>
        /// Adds a new mapping to the plugboard.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <exception cref="ArgumentException"></exception>
        public void Add(char a, char b)
        {
            if (_map.ContainsKey(a) || _map.ContainsValue(a))
            {
                throw new ArgumentException($"Duplicate entry found for {a}");
            }
            else if (_map.ContainsKey(b) || _map.ContainsValue(b))
            {
                throw new ArgumentException($"Duplicate entry found for {b}");
            }
            else if (a == b)
            {
                throw new ArgumentException($"Entry cannot map to itself: {a}");
            }
            _map[a] = b;
            _map[b] = a;
        }

        /// <summary>
        /// Maps a character to its corresponding character in the plugboard.
        /// </summary>
        /// <param name="input">The character to check for a plug board setting</param>
        /// <returns>The orginal char or the mapped one if setting exists</returns>
        public char Map(char input)
        {
            // Check if the character is in the map
            if (_map.ContainsKey(input))
            {
                // Return the mapped character
                return _map[input];
            }
            else if(_map.ContainsValue(input))
            {
                // Find the key that maps to the character
                return _map.FirstOrDefault(x => x.Value == input).Key;
            }
            else
            {
                // If not in the map, return the character itself
                return input;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Model.PlugBoardSetting GetSettings()
        {
            return new Model.PlugBoardSetting(_map);
        }

        public IEnumerator<KeyValuePair<char, char>> GetEnumerator()
        {
            return this._map.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

 
    }
}
