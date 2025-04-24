
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Conundrum.Crypto;

namespace Conundrum.Enigma
{

    // Represents an Enigma machine, a cipher device used for encoding and decoding messages.
    public class EnigmaMachine : CipherBase, ICipherMachine
    {
        // List of rotors used in the Enigma machine for encoding.
        private readonly List<Rotor> _rotors;

        // Reflector used to reverse the signal during encoding.
        private readonly Reflector _reflector;

        // Plugboard configuration for swapping characters before and after encoding.
        private readonly Dictionary<char, char> _plugboard;

        // Constructor to initialize the Enigma machine with rotors, reflector, and plugboard settings.
        public EnigmaMachine(List<Rotor> rotors, Reflector reflector, Dictionary<char, char> plugboard)
        {
            _rotors = rotors;
            _reflector = reflector;
            _plugboard = plugboard;
        }

        // Encodes a single character using the Enigma machine's configuration.
        public char Encode(char input)
        {
            if(this.ByPassCharacters.Contains(input))
            {
                Debug.WriteLine($"Bypassing character {input}");
                return input;
            }

            Debug.WriteLine($"EnigmaMachine beginning encoding {input}");

            // Rotate the rotors before encoding.
            this.Rotate();

            // Pass the input character through the plugboard if configured.
            char ch = _plugboard.ContainsKey(input) ? _plugboard[input] : input;

            // Forward pass through all rotors.
            foreach (var rotor in _rotors)
            {
                ch = rotor.Forward(ch);
            }

            // Reflect the character using the reflector.
            ch = _reflector.Reflect(ch);

            // Backward pass through all rotors in reverse order.
            for (int i = _rotors.Count - 1; i >= 0; i--)
            {
                ch = _rotors[i].Backward(ch);
            }

            // Pass the output character through the plugboard if configured.
            char result = _plugboard.ContainsKey(ch) ? _plugboard[ch] : ch;

            Debug.WriteLine($"EnigmaMachine completed encoding in {input} out {result}");
            return result;
        }

        /// <summary>
        /// Rotates the rotors of the Enigma machine.
        /// Each rotor rotates one position, and if a rotor reaches its notch, the next rotor rotates.
        /// </summary>
        public void Rotate()
        {
            for (int i = 0; i < _rotors.Count; i++)
            {
                // Stop rotating if the current rotor does not reach its notch.
                if (!_rotors[i].Rotate())
                    break;
            }
        }

        /// <summary>
        /// Resets the Enigma machine to its default settings.
        /// This includes resetting all rotors to their original positions.
        /// </summary>
        public void Reset()
        {
            Debug.WriteLine("Resetting Enigma machine to default settings.");
            foreach (var rotor in _rotors)
            {
                rotor.Reset();
            }
        }
    }

}
