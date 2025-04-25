using System;
using System.Collections.Generic;
using Conundrum.Enigma;
using Conundrum.Model;
using Xunit;


namespace Conundrum.Enigma.Test.Unit
{
    public class PlugBoardTests
    {
        [Fact]
        public void PlugBoard_ShouldReturnCorrectMapping()
        {
            // Arrange
            var dic = new Dictionary<char, char>
            {
                {'A', 'B'}
            };
            var plugBoard = new PlugBoard(dic);
            // Act
            var result = plugBoard.Map('A');
            // Assert
            Assert.Equal('B', result);
        }

        [Fact]
        public void PlugBoard_ShouldReturnSameCharacterIfNotMapped()
        {
            // Arrange
            var dic = new Dictionary<char, char>
            {
                {'A', 'B'}
            };
            var plugBoard = new PlugBoard(dic);
            // Act
            var result = plugBoard.Map('C');
            // Assert
            Assert.Equal('C', result);
        }


        [Fact]
        public void PlugBoard_ShouldReturnCorrectSetting()
        {
            // Arrange
            var dic = new Dictionary<char, char>
            {
                {'A', 'B'}
            };
            var setting = new PlugBoardSetting(dic);
            var plugBoard = new PlugBoard(setting);
            // Act
            var result = plugBoard.Map('A');
            // Assert
            Assert.Equal('B', result);
        }

        [Fact]
        public void PlugBoard_ShouldReturnCorrectSettingWithEmptyMap()
        {
            // Arrange
            var dic = new Dictionary<char, char>();
            var setting = new PlugBoardSetting(dic);
            var plugBoard = new PlugBoard(setting);
            // Act
            var result = plugBoard.Map('A');
            // Assert
            Assert.Equal('A', result);
        }

        [Fact]
        public void PlugBoard_ShouldThrowExceptionForDuplicateEntry()
        {
            // Arrange
            var dic = new Dictionary<char, char>
            {
                {'A', 'B'},
                {'B', 'C'}
            };
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new PlugBoard(dic));
        }

        [Fact]
        public void PlugBoard_ShouldThrowExceptionForSelfMapping()
        {
            // Arrange
            var dic = new Dictionary<char, char>
            {
                {'A', 'A'}
            };
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new PlugBoard(dic));
        }
    }
}
