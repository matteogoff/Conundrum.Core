using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Conundrum.Model;

namespace Conundrum.Enigma
{
    /// <summary>
    /// Provides a cipher rotor for the Enigma machine.  A highly configurable ceasar cipher using a key, starting position, and rotor placement positional rotational differences.
    /// </summary>
    public class Rotor
    {
        // The mapping of characters for this rotor
        private readonly char[] rotorMap;

        // The positions of the notches on the rotor
        private readonly char[] notches;

        // The length of the rotor (number of characters in the mapping)
        private readonly int length;

        // The current position of the rotor
        private int position;

        // The original starting position of the rotor
        private char startingPosition;

        // The name of the rotor
        private readonly string name;

        #region cTor

        /// <summary>
        /// Create a new rotor for an Enigma machine.
        /// </summary>
        /// <param name="wiring">The wiring configuration of the rotor.</param>
        /// <param name="notches">The positions of the notches on the rotor.</param>
        /// <param name="startingPosition">The initial position of the rotor (default is 'A').</param>
        /// <param name="name">The name of the rotor (default is "Rotor").</param>
        public Rotor(string wiring, string notches, char startingPosition = 'A', string name = "Rotor")
        {
            this.length = wiring.Length; // Set the rotor length based on the wiring
            this.startingPosition = startingPosition; // Set the starting position
            this.rotorMap = wiring.ToCharArray(); // Convert the wiring string to a character array
            this.position = startingPosition - 'A'; // Calculate the initial position as an integer
            this.notches = notches.ToCharArray(); // Convert the notches string to a character array
            this.name = name; // Set the rotor name
        }

        /// <summary>
        /// Create a new rotor for an Enigma machine using JSON settings.
        /// </summary>
        /// <param name="json"> The Json document passed as a dynamic object.</param>
        public Rotor(dynamic json)
        {             
            // Parse the JSON object to initialize the rotor properties
            this.startingPosition = json.StartingPosition ?? 'A';
            this.position = this.startingPosition - 'A';
            this.name = json.Name ?? "Rotor";
            this.rotorMap = json.RotorSettings.ToCharArray();
            this.length = rotorMap.Length;
            this.notches = json.Notches.ToCharArray();
        }

        public Rotor(RotorSetting settings)
        {
            // Initialize the rotor properties based on the provided settings
            this.startingPosition = settings.StartingPosition;
            this.position = settings.Position;
            this.name = settings.Name;
            this.rotorMap = settings.Map.ToCharArray();
            this.length = rotorMap.Length;
            this.notches = settings.Notches.ToCharArray();
        }

        #endregion cTor

        #region Properties

        /// <summary>
        /// Gets the notches of the rotor.
        /// </summary>
        internal char[] Notches
        {
            get { return this.notches; }
        }

        /// <summary>
        /// Gets the wiring configuration of the rotor.
        /// </summary>
        internal string RotorSettings
        {
            get { return new string(this.rotorMap); }
        }

        /// <summary>
        /// Gets the original starting position of the rotor.
        /// </summary>
        internal char StartingPosition
        {
            get { return this.startingPosition; }
        }

        /// <summary>
        /// Gets the current position of the rotor.
        /// </summary>
        internal char Position
        {
            get { return (char)('A' + this.position); }
        }

        /// <summary>
        /// Gets the name of the rotor.
        /// </summary>
        public string Name
        {
            get { return this.name; }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Forward encodes a character through the rotor.
        /// </summary>
        /// <param name="ch">The character to encode.</param>
        /// <returns>The encoded character.</returns>
        public char Forward(char ch)
        {
            // Calculate the index after applying the rotor's position
            int index = (ch - 'A' + this.position) % this.length;

            // Get the encoded character from the rotor map
            char result = this.rotorMap[index];

            // Log the encoding process
            Debug.WriteLine($"Rotor: [{this.name}] forward encodes {ch} to {result} at position {this.position}");

            return result;
        }

        /// <summary>
        /// Backward encodes a character through the rotor.
        /// </summary>
        /// <param name="ch">The character to encode.</param>
        /// <returns>The encoded character.</returns>
        public char Backward(char ch)
        {
            // Find the index of the character in the rotor map
            int index = Array.IndexOf(this.rotorMap, ch);

            // Calculate the decoded character based on the rotor's position
            char result = (char)('A' + (index - this.position + this.length) % this.length);

            // Log the decoding process
            Debug.WriteLine($"Rotor: [{this.name}] backward encodes {ch} to {result} at position {this.position}");

            return result;
        }

        /// <summary>
        /// Rotates the rotor one position forward.
        /// </summary>
        /// <returns>True if a notch has been reached, indicating the next rotor should rotate.</returns>
        public bool Rotate()
        {
            // Increment the rotor's position
            this.position = (this.position + 1) % this.length;

            // Log the rotation
            Debug.WriteLine($"Rotor: [{this.name}] rotated to position {this.position}");

            // Check if the current position matches any of the notches
            bool result = this.notches.Any(n => n == this.Position);

            if (result)
                Debug.WriteLine($"Rotor: [{this.name}] has reached notch {this.Position}");

            return result;
        }

        /// <summary>
        /// Lists the rotor's mapping and positions.
        /// </summary>
        /// <returns>A collection of strings representing the rotor's mapping and positions.</returns>
        public IEnumerable<string> ListRotors()
        {
            List<string> rotors = new List<string>();

            // Iterate through each position in the rotor
            for (int i = 0; i < this.length; i++)
            {
                // Calculate the position after applying the rotor's current position
                int pos = ((i + this.position) % this.length);

                string log = $"Rotor: [{this.name}] list Map[{i}] = {this.rotorMap[i]}:{(char)('A' + pos)}";
                // Add the mapping and position to the list
                Debug.WriteLine(log);
                rotors.Add(log);
            }

            return rotors;
        }

        /// <summary>
        /// Returns a string representation of the rotor.
        /// </summary>
        /// <returns>A string representing the rotor's mapping and positions.</returns>
        public override string ToString()
        {
            return string.Join(Environment.NewLine, this.ListRotors());
        }

        /// <summary>
        /// Resets the rotor to its original position.
        /// </summary>
        internal void Reset()
        {
            // Reset the rotor's position to the starting position
            this.position = startingPosition - 'A';
        }


        //Add a method to return a RotorSetting object with the current state of the Rotor
        public RotorSetting GetRotorSetting()
        {
            return new RotorSetting
            {
                Name = this.name,
                StartingPosition = this.startingPosition,
                Position = this.position,
                RingSetting = 0, // Assuming no ring setting for simplicity
                Map = new string(this.rotorMap),
                Notches = new string(this.notches)
            };
        }



        #endregion Methods
    }
}
