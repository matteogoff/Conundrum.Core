using System;
using Xunit;
using Conundrum.Enigma;

namespace Conundrum.Enigma.Test.Unit
{
    public class RotorTests
    {


        [Fact]
        public void Rotor_Construtor_Success_01()
        {
            // Arrange
            const string ROTOR = "EKMFLGDQVZNTOWYHXUSPAIBRCJ";
            var rotor = new Rotor(ROTOR, "A");

            // Act
            var dump = rotor.ListRotors();

            // Assert
            Assert.Equal(ROTOR, rotor.RotorSettings);
            Assert.Equal(26, dump.Count());
            Assert.Equal('A', rotor.Notches[0]);
        }

        [Fact]
        public void Rotor_Construtor_Success_02()
        {
            // Arrange
            const string ROTOR = "EKMFLGDQVZNTOWYHXUSPAIBRCJ";
            var rotor = new Rotor(ROTOR, "AKQ", startingPosition:'H', name: "Test");

            // Act
            var dump = rotor.ListRotors();

            // Assert
            Assert.Equal(ROTOR, rotor.RotorSettings);
            Assert.Equal(26, dump.Count());
            Assert.Equal('A', rotor.Notches[0]);
            Assert.Equal('K', rotor.Notches[1]);
            Assert.Equal('Q', rotor.Notches[2]);

            Assert.Equal('H', rotor.StartingPosition);

            Assert.Equal("Test", rotor.Name);
        }


        /// <summary>
        /// Test the Forward method of the Rotor class.
        /// </summary>
        [Fact]
        public void Forward_ShouldReturnCorrectCharacter()
        {
            // Arrange            "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
            var rotor = new Rotor("EKMFLGDQVZNTOWYHXUSPAIBRCJ", "A");

            // Act
            var result = rotor.Forward('A');

            // Assert
            Assert.Equal('E', result);
        }

        /// <summary>
        /// Test the Backward method of the Rotor class.
        /// </summary>
        [Fact]
        public void Backward_ShouldReturnCorrectCharacter()
        {
            // Arrange            "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
            var rotor = new Rotor("EKMFLGDQVZNTOWYHXUSPAIBRCJ", "A");

            // Act
            var result = rotor.Backward('E');

            // Assert
            Assert.Equal('A', result);
        }

        /// <summary>
        /// Test the Rotate method of the Rotor class.
        /// </summary>
        [Fact]
        public void Rotate_ShouldReturnTrueAfterFullRotation()
        {
            // Arrange
            var rotor = new Rotor("EKMFLGDQVZNTOWYHXUSPAIBRCJ", "A");

            // Act & Assert
            for (int i = 0; i < 25; i++)
            {
                Assert.False(rotor.Rotate());
            }
            Assert.True(rotor.Rotate());
        }

        [Fact]
        public void TestForwardEncoding()
        {
            // Arrange
            var rotor = new Rotor("EKMFLGDQVZNTOWYHXUSPAIBRCJ", "Q", 'A');

            // Act
            var result = rotor.Forward('A');

            // Assert
            Assert.Equal('E', result);
        }

        [Fact]
        public void TestBackwardEncoding()
        {
            // Arrange
            var rotor = new Rotor("EKMFLGDQVZNTOWYHXUSPAIBRCJ", "Q", 'A');

            // Act
            var result = rotor.Backward('E');

            // Assert
            Assert.Equal('A', result);
        }

        [Fact]
        public void TestRotation()
        {
            // Arrange
            var rotor = new Rotor("EKMFLGDQVZNTOWYHXUSPAIBRCJ", "Q", 'A');

            // Act
            var result = rotor.Rotate();

            // Assert
            Assert.False(result);
            Assert.Equal('K', rotor.Forward('A'));
        }

        [Fact]
        public void TestFullRotation()
        {
            // Arrange
            var rotor = new Rotor("EKMFLGDQVZNTOWYHXUSPAIBRCJ", "Z", 'A');

            // Act
            bool result = false;
            for (int i = 0; i < 25; i++)
            {
                result = rotor.Rotate();
            }

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void TestListRotors()
        {
            // Arrange
            var rotor = new Rotor("EKMFLGDQVZNTOWYHXUSPAIBRCJ", "Q", startingPosition: 'A', name:"II");

            // Act
            var result = rotor.ListRotors();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains("Rotor: [II] list Map[0] = E:A", result);

            rotor.Rotate();

            result = rotor.ListRotors();
            Assert.Contains("Rotor: [II] list Map[0] = E:B", result);


        }



        [Fact]
        public void TestListRotorsWithStart()
        {
            // Arra"ABCDEFGHIJKLMNOPQRSTUVWXYZ"
            var rotor = new Rotor("EKMFLGDQVZNTOWYHXUSPAIBRCJ", "Q", startingPosition: 'P', name: "II");

            // Act
            var result = rotor.ListRotors();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains("Rotor: [II] list Map[0] = E:P", result);

            rotor.Rotate();

            result = rotor.ListRotors();
            Assert.Contains("Rotor: [II] list Map[0] = E:Q", result);


        }

        //Create a test for the Reset method
        [Fact]
        public void TestResetRotor01()
        {
            // Arra"ABCDEFGHIJKLMNOPQRSTUVWXYZ"
            var rotor = new Rotor("EKMFLGDQVZNTOWYHXUSPAIBRCJ", "Q", startingPosition: 'P', name: "II");

            char msg1 = rotor.Forward('A');

            rotor.Rotate(); rotor.Rotate(); rotor.Rotate();
            char msg2 = rotor.Forward('A');
            Assert.NotEqual(msg1, msg2);

            rotor.Reset();

            char msg3 = rotor.Forward('A');

            Assert.Equal(msg1, msg3);
            Assert.NotEqual(msg2, msg3);
            Assert.Equal('P', rotor.StartingPosition);
            Assert.Equal('P', rotor.Position);
         
        }

        //Create a test for the Reset method with a different starting position
        [Fact]
        public void TestResetRotor02() {
            // Arra"ABCDEFGHIJKLMNOPQRSTUVWXYZ"
            var rotor = new Rotor("EKMFLGDQVZNTOWYHXUSPAIBRCJ", "Z", startingPosition: 'K', name: "II");

            char msg1 = rotor.Forward('A');

            rotor.Rotate(); rotor.Rotate(); rotor.Rotate();
            char msg2 = rotor.Forward('A');
            Assert.NotEqual(msg1, msg2);

            rotor.Reset();

            char msg3 = rotor.Forward('A');

            Assert.Equal(msg1, msg3);
            Assert.NotEqual(msg2, msg3);
            Assert.Equal('K', rotor.StartingPosition);
            Assert.Equal('K', rotor.Position);
        }


        /// <summary>
        /// Test the persistence of rotor settings.
        /// </summary>
        [Fact]
        public void TestRotorSettingsPersistence()
        {
            // Arrange
            var originalRotor = new Rotor("EKMFLGDQVZNTOWYHXUSPAIBRCJ", "Q", startingPosition: 'A', name: "II");
            string message = "HELLO";
            string encodedMessage = "";

            var startingSettings = originalRotor.GetRotorSetting();
            // Act
            foreach (char c in message)
            {
                encodedMessage += originalRotor.Forward(c);
                originalRotor.Rotate();
            }

            var savedSettings = originalRotor.GetRotorSetting();
            var newRotor = new Rotor(savedSettings);

            encodedMessage = "";
            string reEncodedMessage = "";
            foreach (char c in message)
            {
                encodedMessage += originalRotor.Forward(c);
                originalRotor.Rotate();
                reEncodedMessage += newRotor.Forward(c);
                newRotor.Rotate();
            }

            // Assert
            Assert.Equal(encodedMessage, reEncodedMessage);
        }

        [Fact]
        public void TestRotorSettingsPersistenceWithNotche()
        {
            // Arrange                 "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
            var originalRotor = new Rotor("EKMFLGDQVZNTOWYHXUSPAIBRCJ", "O", startingPosition: 'G', name: "II");
            string message = "ICANSEE";
            string encodedMessage = "";

            var startingSettings = originalRotor.GetRotorSetting();
            // Act
            foreach (char c in message)
            {
                encodedMessage += originalRotor.Forward(c);
                originalRotor.Rotate();
            }

            var savedSettings = originalRotor.GetRotorSetting();
            var newRotor = new Rotor(savedSettings);

            encodedMessage = "";
            string reEncodedMessage = "";

            encodedMessage += originalRotor.Forward('H');
            bool rotate = originalRotor.Rotate();
            reEncodedMessage += newRotor.Forward('H');
            bool alsoRotate = newRotor.Rotate();
            

            // Assert
            Assert.Equal(encodedMessage, reEncodedMessage);
            Assert.True(rotate);
            Assert.True(alsoRotate);
        }

    }
}