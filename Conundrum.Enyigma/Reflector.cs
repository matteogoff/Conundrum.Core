using Conundrum.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conundrum.Enigma
{
    /// <summary>
    /// Represents a reflector for the Enigma machine.
    /// </summary>
    public class Reflector
    {
        // The name of the rotor
        private readonly string name;

        // Array to store the character mapping for the reflector
        private readonly char[] _map;

        // Constructor to initialize the reflector with a given mapping
        public Reflector(string map, string name="Reflector")
        {
            // Convert the input string to a character array and store it in _map
            _map = map.ToCharArray();
            this.name = name;
            // Log the configuration of the reflector for debugging purposes
            Debug.WriteLine($"Reflector configured with {map}");
        }

        public Reflector(ReflectorSetting setting)
        {
            // Convert the input string to a character array and store it in _map
            _map = setting.Map.ToCharArray();
            // Set the name of the reflector from the provided setting
            this.name = setting.Name;
            // Log the configuration of the reflector for debugging purposes
            Debug.WriteLine($"Reflector configured with {setting.Map}");
        }

        /// <summary>
        /// Gets the name of the reflector.
        /// </summary>
        public string Name
        {
            get { return this.name; }
        }

        // Method to reflect a character based on the configured mapping
        public char Reflect(char ch)
        {
            // Calculate the reflected character using the mapping array
            char result = _map[ch - 'A'];
            // Log the reflection process for debugging purposes
            Debug.WriteLine($"Reflector echos {ch} to {result}");
            // Return the reflected character
            return result;
        }

        public ReflectorSetting GetSetting()
        {
            // Create a new ReflectorSetting object and set its properties
            ReflectorSetting setting = new ReflectorSetting();
            setting.Map = new string(_map);
            setting.Name = this.name;
            // Return the configured ReflectorSetting object
            return setting;
        }
    }
}
