using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Conundrum.Enigma.Test.Unit
{
    public enum RotorType
    {
        I,
        II,
        III,
        IV,
        V,
        VI,
        VII,
        VIII
    }



    internal static class EnigmaConstants
    {
        // Constants for the Enigma machine
        const string ALPHABET = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        const string ROTOR_I = "EKMFLGDQVZNTOWYHXUSPAIBRCJ";
        const string ROTOR_II = "AJDKSIRUXBLHWTMCQGZNPYFVOE";
        const string ROTOR_III = "BDFHJLCPRTXVZNYEIWGAKMUSQO";
        const string ROTOR_IV = "ESOVPZJAYQUIXHLCKTNWGFMRB";
        const string ROTOR_V = "VZBRGITYUPSDXNLHKJYTCQEMWA";
        const string ROTOR_VI = "JPGVOUMFYQBENHZRDKASXLICTW";
        const string ROTOR_VII = "NZJHGRCXMYSWBOUFAIVLPEKQDT";
        const string ROTOR_VIII = "FKQHTLXOCBJSPDZRAMEWNIUYGV";
        const string REFLECTOR_A = "EJMZALYXVBWFCRQUONTSPIKGHD";
        const string REFLECTOR_B = "YRUHQSLDPXNGOKMIEBFZCWVJAT";
        const string REFLECTOR_C = "FVPJIAOYEDRZXWGCTKUQSBNMHL";

        private static Dictionary<string, string> CharacterMaps;

        static EnigmaConstants()
        {
            // Constructor for EnigmaConstants
            CharacterMaps = new Dictionary<string, string>()
            {
                { "I", ROTOR_I },
                { "II", ROTOR_II },
                { "III", ROTOR_III },
                { "V", ROTOR_IV },
                { "VI", ROTOR_V },
                { "VII", ROTOR_VI },
                { "VIII", ROTOR_VII },
                { "IX", ROTOR_VIII },
                { "A", REFLECTOR_A },
                { "B", REFLECTOR_B },
                { "C", REFLECTOR_C }
            };
        }


        internal static Rotor GetRotor(string name, string notches, char start='A')
        {

           string wiring = CharacterMaps[name];
            if (string.IsNullOrEmpty(wiring))
            {
                throw new ArgumentException($"Rotor {name} not found.");
            }
            return new Rotor(wiring, notches, start, name);
        }

        internal static Reflector GetReflector(string name)
        {
            string wiring = CharacterMaps[name];
            if (string.IsNullOrEmpty(wiring))
            {
                throw new ArgumentException($"Reflector {name} not found.");
            }
            return new Reflector(wiring, name);
        }

        internal static EnigmaMachine GetEnigmaMachine(List<string> rotors, string reflector, Dictionary<char, char> plugboard)
        {
            var rotorList = new List<Rotor>();
            foreach (var rotor in rotors)
            {
                rotorList.Add(GetRotor(rotor, "A"));
            }
            var reflectorObj = GetReflector(reflector);
            var plugBoard = new PlugBoard(plugboard);
            return new EnigmaMachine(rotorList, reflectorObj, plugBoard);
        }
    }
}
