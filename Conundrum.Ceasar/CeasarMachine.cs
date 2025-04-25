using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Conundrum.Crypto;
using Conundrum.Model;

namespace Conundrum.Ceasar
{
    public class CeasarMachine : CipherBase, ICipherMachine
    {
        private readonly char[] keyMap;
        private readonly int length;
        private int position;
        private char startingPosition;
        private readonly string name;

        public CeasarMachine(string key, char startingPosition = 'A', string name = "Rotor")
        {
            this.keyMap = key.ToCharArray();
            this.length = key.Length;
            this.position = startingPosition - 'A';
            this.name = name;
        }

        public char Encode(char input)
        {
            if (this.ByPassCharacters.Contains(input))
            {
                Debug.WriteLine($"Bypassing character {input}");
                return input;
            }

            Debug.WriteLine($"CeasarMachine beginning encoding {input}");

            // Rotate the rotors before encoding
            this.Rotate();

            // Encode the character
            int inputIndex = input - 'A'; // Convert input character to its index in the alphabet
            int encodedIndex = (inputIndex + this.position) % this.length; // Apply the Caesar cipher shift
            char result = this.keyMap[encodedIndex]; // Map the result back to a character using the keyMap

            Debug.WriteLine($"CeasarMachine completed encoding in {input} out {result}");
            return result;
        }

        /// <summary>
        /// Rotates the position of the character in the key string
        /// </summary>
        public void Rotate()
        {
            this.position = (this.position + 1) % this.length;
        }

        /// <summary>
        /// Resets the Enigma machine to its default settings.
        /// </summary>
        public void Reset()
        {
            Debug.WriteLine("Resetting Enigma machine to default settings.");
            this.position = this.startingPosition - 'A';
        }

        /// <summary>
        /// Returns the settings of the cipher machine.
        /// </summary>
        public CipherSettingBase GetSettings()
        {
            // Assuming a basic implementation for demonstration purposes.
            return new CeasarMachineSetting
            {
                Map = new string(this.keyMap),
                StartingPosition = this.startingPosition,
                Name = this.name
            };
        }
    }


}
