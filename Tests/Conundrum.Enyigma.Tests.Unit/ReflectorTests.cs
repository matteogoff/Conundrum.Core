using System;
using System.Collections.Generic;
using Conundrum.Enigma;
using Conundrum.Model;
using Xunit;

namespace Conundrum.Enigma.Test.Unit
{
    public class ReflectorTests
    {
        [Fact]
        public void Reflector_ShouldReturnCorrectReflection()
        {
            // Arrange
            var map = "YRUHQSLDPXNGOKMIEBFZCWVJAT";
            var reflector = new Reflector(map);

            // Act
            var result = reflector.Reflect('A');

            // Assert
            Assert.Equal('Y', result);
        }

        [Fact]
        public void Reflector_ShouldHandleAllLetters()
        {
            // Arrange
            var map = "YRUHQSLDPXNGOKMIEBFZCWVJAT";
            var reflector = new Reflector(map);
            var input = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var expectedOutput = "YRUHQSLDPXNGOKMIEBFZCWVJAT";

            // Act & Assert
            for (int i = 0; i < input.Length; i++)
            {
                var result = reflector.Reflect(input[i]);
                Assert.Equal(expectedOutput[i], result);
            }
        }

        [Fact]
        public void Reflector_ShouldThrowExceptionForInvalidCharacter()
        {
            // Arrange
            var map = "YRUHQSLDPXNGOKMIEBFZCWVJAT";
            var reflector = new Reflector(map);

            // Act & Assert
            Assert.Throws<IndexOutOfRangeException>(() => reflector.Reflect('1'));
        }

        [Fact]
        public void Reflector_ShouldReturnCorrectSetting()
        {
            // Arrange
            var map = "YRUHQSLDPXNGOKMIEBFZCWVJAT";
            var name = "Reflector I";
            var setting = new ReflectorSetting { Map = map, Name = name };
            var reflector = new Reflector(setting);
            // Act
            var result = reflector.GetSetting();
            // Assert
            Assert.Equal(map, result.Map);
            Assert.Equal(name, result.Name);
        }

        [Fact]
        public void Reflector_ShouldReturnCorrectValue()
        {
            // Arrange
            var map = "YRUHQSLDPXNGOKMIEBFZCWVJAT";
            var reflector = new Reflector(map);
            var input = 'A';
            var expectedOutput = 'Y';
            // Act
            var result = reflector.Reflect(input);
            // Assert
            Assert.Equal(expectedOutput, result);
        }

        [Fact]
        public void Reflector_ShouldReturnCorrectName()
        {
            // Arrange
            var map = "YRUHQSLDPXNGOKMIEBFZCWVJAT";
            var name = "Reflector I";
            var reflector = new Reflector(map, name);
            // Act
            var result = reflector.Name;
            // Assert
            Assert.Equal(name, result);
        }

        [Fact]
        public void Reflector_TestGetSettings()
        {
            // Arrange
            var map = "YRUHQSLDPXNGOKMIEBFZCWVJAT";
            var name = "Reflector I";
            ReflectorSetting setting = new ReflectorSetting { Map = map, Name = name };
            var reflector = new Reflector(setting);
            // Act
            ReflectorSetting result = reflector.GetSetting();
            // Assert
            Assert.NotNull(result);
            Assert.Equal(setting.Map, result.Map);
            Assert.Equal(setting.Name, result.Name);


        }
    }
}
