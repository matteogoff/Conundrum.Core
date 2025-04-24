using System;
using System.Collections.Generic;
using Conundrum.Model;
using Conundrum.Crypto;
using Conundrum.Enigma;
using Xunit;

namespace Conundrum.Enigma.Test.Unit
{
    public class EnigmaMachineTests
    {

        [Fact]
        public void OneRotor_Encode_ShouldReturnExpectedOutput()
        {
            // Arrange
            var rotors = new List<Rotor>
            {           // "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
                new Rotor("EKMFLGDQVZNTOWYHXUSPAIBRCJ", "A")
            };
                                       // "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
            var reflector = new Reflector("YRUHQSLDPXNGOKMIEBFZCWVJAT");
            var plugboard = new Dictionary<char, char>
            {
                {'A', 'B'},
                {'B', 'A'}
            };
            var enigmaMachine = new EnigmaMachine(rotors, reflector, plugboard);

            // Act
            char result = enigmaMachine.Encode('A');

            // Assert
            Assert.Equal('L', result);
        }

        [Fact]
        public void OneRotor_Rotate_ShouldRotateRotorsCorrectly()
        {
            // Arrange
            var rotors = new List<Rotor>
            {
                new Rotor("EKMFLGDQVZNTOWYHXUSPAIBRCJ", "A", name:"I")
            };
            var reflector = new Reflector("YRUHQSLDPXNGOKMIEBFZCWVJAT");
            var plugboard = new Dictionary<char, char>();
            var enigmaMachine = new EnigmaMachine(rotors, reflector, plugboard);

            // Act
            enigmaMachine.Rotate();

            // Assert
            Assert.Equal('K', rotors[0].Forward('A'));
        }




        [Fact]
        public void TriRotor_Encode_ShouldReturnExpectedOutput()
        {
            // Arrange
            var rotors = new List<Rotor>
            {
                new Rotor("EKMFLGDQVZNTOWYHXUSPAIBRCJ", "A", name:"I"),
                new Rotor("AJDKSIRUXBLHWTMCQGZNPYFVOE", "A", name:"II"),
                new Rotor("BDFHJLCPRTXVZNYEIWGAKMUSQO", "A", name:"III")
            };
            var reflector = new Reflector("YRUHQSLDPXNGOKMIEBFZCWVJAT");
            var plugboard = new Dictionary<char, char>
            {
                {'A', 'B'},
                {'B', 'A'}
            };
            var enigmaMachine = new EnigmaMachine(rotors, reflector, plugboard);

            // Act
            char result = enigmaMachine.Encode('A');

            // Assert
            Assert.Equal('W', result);
        }

        [Fact]
        public void TriRotor_Rotate_ShouldRotateRotorsCorrectly()
        {
            // Arrange
            var rotors = new List<Rotor>
            {
                new Rotor("EKMFLGDQVZNTOWYHXUSPAIBRCJ", "A", name:"I"),
                new Rotor("AJDKSIRUXBLHWTMCQGZNPYFVOE", "A", name:"II"),
                new Rotor("BDFHJLCPRTXVZNYEIWGAKMUSQO", "A", name:"III")
            };
            var reflector = new Reflector("YRUHQSLDPXNGOKMIEBFZCWVJAT");
            var plugboard = new Dictionary<char, char>();
            var enigmaMachine = new EnigmaMachine(rotors, reflector, plugboard);

            // Act
            enigmaMachine.Rotate();

            // Assert
            Assert.Equal('K', rotors[0].Forward('A'));
        }

        [Fact]
        public void CheckReset_EncodeEncode_ShouldReturnSameMessage()
        {
            // Arrange
            var rotors = new List<Rotor>
            {
                new Rotor("EKMFLGDQVZNTOWYHXUSPAIBRCJ", "A", name:"I")
            };
            var reflector = new Reflector("YRUHQSLDPXNGOKMIEBFZCWVJAT");
            var plugboard = new Dictionary<char, char>
            {
                //{'A', 'B'},
                //{'B', 'A'}
            };
            var enigmaMachine = new EnigmaMachine(rotors, reflector, plugboard);
            var message = "HELLO";

            // Act
            var encodedMessage = new string(message.Select(c => enigmaMachine.Encode(c)).ToArray());
            enigmaMachine.Reset();
            var encodedMessage2 = new string(message.Select(c => enigmaMachine.Encode(c)).ToArray());

            // Assert
            Assert.Equal(encodedMessage, encodedMessage2);
        }

        [Fact]
        public void CheckNoReset_EncodeEncode_ShouldNotReturnSameMessage()
        {
            // Arrange
            var rotors = new List<Rotor>
            {
                new Rotor("EKMFLGDQVZNTOWYHXUSPAIBRCJ", "A", name:"I")
            };
            var reflector = new Reflector("YRUHQSLDPXNGOKMIEBFZCWVJAT");
            var plugboard = new Dictionary<char, char>
            {
                //{'A', 'B'},
                //{'B', 'A'}
            };
            var enigmaMachine = new EnigmaMachine(rotors, reflector, plugboard);
            var message = "HELLO";

            // Act
            var encodedMessage = new string(message.Select(c => enigmaMachine.Encode(c)).ToArray());
            var encodedMessage2 = new string(message.Select(c => enigmaMachine.Encode(c)).ToArray());

            // Assert
            Assert.NotEqual(encodedMessage, encodedMessage2);
        }

        [Fact]
        public void OneRotor_EncodeDecode_ShouldReturnOriginalMessage()
        {
            // Arrange
            var rotors = new List<Rotor>
            {
                new Rotor("EKMFLGDQVZNTOWYHXUSPAIBRCJ", "A", name:"I")
            };
            var reflector = new Reflector("YRUHQSLDPXNGOKMIEBFZCWVJAT");
            var plugboard = new Dictionary<char, char>
            {
                //{'A', 'B'},
                //{'B', 'A'}
            };
            var enigmaMachine = new EnigmaMachine(rotors, reflector, plugboard);
            var message = "HELLO";

            // Act
            var encodedMessage = new string(message.Select(c => enigmaMachine.Encode(c)).ToArray());
            enigmaMachine.Reset();
            var decodedMessage = new string(encodedMessage.Select(c => enigmaMachine.Encode(c)).ToArray());

            // Assert
            Assert.Equal(message, decodedMessage);
        }


        [Fact]
        public void OneRotor_EncodeDecode_ShouldReturnAllCHaracters()
        {
            // Arrange
            var rotors = new List<Rotor>
            {
                new Rotor("EKMFLGDQVZNTOWYHXUSPAIBRCJ", "A", name:"I")
            };
            var reflector = new Reflector("YRUHQSLDPXNGOKMIEBFZCWVJAT");
            var plugboard = new Dictionary<char, char>
            {
                //{'A', 'B'},
                //{'B', 'A'}
            };
            var enigmaMachine = new EnigmaMachine(rotors, reflector, plugboard);
            var message = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            // Act
            var encodedMessage = new string(message.Select(c => enigmaMachine.Encode(c)).ToArray());
            enigmaMachine.Reset();
            var decodedMessage = new string(encodedMessage.Select(c => enigmaMachine.Encode(c)).ToArray());

            // Assert
            Assert.Equal(message, decodedMessage);
        }

        [Fact]
        public void TriRotor_EncodeDecode_ShouldReturnOriginalMessage()
        {
            // Arrange
            var rotors = new List<Rotor>
            {
                new Rotor("EKMFLGDQVZNTOWYHXUSPAIBRCJ", "A", name:"I"),
                new Rotor("AJDKSIRUXBLHWTMCQGZNPYFVOE", "A", name:"II"),
                new Rotor("BDFHJLCPRTXVZNYEIWGAKMUSQO", "A", name:"III")
            };
            var reflector = new Reflector("YRUHQSLDPXNGOKMIEBFZCWVJAT");
            var plugboard = new Dictionary<char, char>
            {
                {'A', 'B'},
                {'B', 'A'}
            };
            var enigmaMachine = new EnigmaMachine(rotors, reflector, plugboard);
            var message = "HELLO";

            // Act
            var encodedMessage = new string(message.Select(c => enigmaMachine.Encode(c)).ToArray());
            enigmaMachine.Reset();
            var decodedMessage = new string(encodedMessage.Select(c => enigmaMachine.Encode(c)).ToArray());

            // Assert
            Assert.Equal(message, decodedMessage);
        }


        [Fact]
        public void EnigmaMachine_GetSettings_ShouldReturnInitalState()
        {
            //Arrange
            var settings = new EnigmaMachineSetting()
            {
                Name = "Test",
                Rotors = new List<RotorSetting>
                {
                    new RotorSetting { Map = "EKMFLGDQVZNTOWYHXUSPAIBRCJ", StartingPosition = 'A', Notches = "D", Name = "I" },
                    new RotorSetting { Map = "AJDKSIRUXBLHWTMCQGZNPYFVOE", StartingPosition = 'A', Notches = "K", Name = "II" },
                    new RotorSetting { Map = "BDFHJLCPRTXVZNYEIWGAKMUSQO", StartingPosition = 'A', Name = "III" }
                },
                Reflector = new ReflectorSetting { Map = "YRUHQSLDPXNGOKMIEBFZCWVJAT", Name = "Reflector I" },
                Plugboard = new Dictionary<char, char>
                {
                    {'A', 'B'},
                    {'B', 'A'}
                }
            };
        }
    }
}
