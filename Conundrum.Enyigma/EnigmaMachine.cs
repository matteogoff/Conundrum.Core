
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Conundrum.Crypto;
using Conundrum.Model;

namespace Conundrum.Enigma
{

    // Represents an Enigma machine, a cipher device used for encoding and decoding messages.
    public class EnigmaMachine : CipherBase, ICipherMachine
    {
        private readonly string _name;

        // List of rotors used in the Enigma machine for encoding.
        private readonly List<Rotor> _rotors;

        // Reflector used to reverse the signal during encoding.
        private readonly Reflector _reflector;

        // Plugboard configuration for swapping characters before and after encoding.
        private readonly PlugBoard _plugboard;

        // Constructor to initialize the Enigma machine with rotors, reflector, and plugboard settings.
        public EnigmaMachine(List<Rotor> rotors, Reflector reflector, PlugBoard plugboard)
        {
            if(rotors == null || rotors.Count == 0)
            {
                throw new ArgumentNullException(nameof(rotors));
            }
            else if (reflector == null)
            {
                throw new ArgumentNullException(nameof(reflector));
            }

            _rotors = rotors;
            _reflector = reflector;

            if (plugboard != null)
            {
                _plugboard = plugboard;
            }
            else
            {
                _plugboard = new PlugBoard();
            }
        }

        public EnigmaMachine(EnigmaMachineSetting settings)
        {
            if(settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }
            else if(settings.Rotors == null || settings.Rotors.Count == 0)
            {
                throw new ArgumentNullException(nameof(settings.Rotors));
            }
            else if (settings.Reflector == null)
            {
                throw new ArgumentNullException(nameof(settings.Reflector));
            }
            else if (String.IsNullOrEmpty(settings.Type) || settings.Type != "Enigma")
            {
                throw new ArgumentNullException(nameof(settings.Type));
            }

            this._name = settings.Name;
            this._rotors = new List<Rotor>();
            foreach (var rotor in settings.Rotors)
            {
                this._rotors.Add(new Rotor(rotor));
            }

            this._reflector = new Reflector(settings.Reflector);

            this._plugboard = new PlugBoard(settings.Plugboard);
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
            char ch = _plugboard.Map(input);

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
            char result = _plugboard.Map(ch);

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


        public CipherSettingBase GetSettings()
        {
            var settings = new EnigmaMachineSetting
            {
                Name = this._name,
                Type = "Enigma",
                Rotors = this._rotors.Select(r => r.GetSettings()).ToList(),
                Reflector = this._reflector.GetSettings(),
                Plugboard = this._plugboard.GetSettings() 
            };
            return settings;
        }

    }

}
