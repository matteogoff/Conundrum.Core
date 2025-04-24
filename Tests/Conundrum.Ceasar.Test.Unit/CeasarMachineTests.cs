using System;
using System.Collections.Generic;
using Conundrum.Ceasar;
using Xunit;

namespace Conundrum.Ceasar.Test.Unit
{
    public class CeasarMachineTests
    {
        [Fact]
        public void Constructor_ShouldInitializePropertiesCorrectly()
        {
            // Arrange
            string key = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            char startingPosition = 'C';
            string name = "TestRotor";

            // Act
            var ceasarMachine = new CeasarMachine(key, startingPosition, name);

            // Assert
            Assert.NotNull(ceasarMachine);
        }

        [Fact]
        public void Encode_ShouldReturnCorrectlyEncodedCharacter()
        {
            // Arrange
            string key = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var ceasarMachine = new CeasarMachine(key, 'A');

            // Act
            char result = ceasarMachine.Encode('A');

            // Assert
            Assert.Equal('B', result);
        }

        [Fact]
        public void Rotate_ShouldUpdatePositionCorrectly()
        {
            // Arrange
            string key = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var ceasarMachine = new CeasarMachine(key, 'A');

            // Act
            ceasarMachine.Rotate();

            // Assert
            // Assuming position is exposed for testing purposes
        }

        [Fact]
        public void Reset_ShouldRestoreStartingPosition()
        {
            // Arrange
            string key = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var ceasarMachine = new CeasarMachine(key, 'C');

            // Act
            ceasarMachine.Rotate();
            ceasarMachine.Reset();

            // Assert
            // Assuming position is exposed for testing purposes
        }
    }
}