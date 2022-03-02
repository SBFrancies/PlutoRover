using PlutoRover.Api.Enums;
using PlutoRover.Api.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace PlutoRover.UnitTests.Services
{
    public class CommandParserTests
    {
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void CommandParser_ParseCommands_NullOrEmptyReturnNoCommands(string commands)
        {
            var sut = GetSystemUnderTest();

            var result  = sut.ParseCommands(commands);

            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Theory]
        [InlineData("L", RoverCommand.L)]
        [InlineData("R", RoverCommand.R)]
        [InlineData("F", RoverCommand.F)]
        [InlineData("B", RoverCommand.B)]
        [InlineData("frlbbbf", RoverCommand.F, RoverCommand.R, RoverCommand.L, RoverCommand.B, RoverCommand.B, RoverCommand.B, RoverCommand.F)]
        [InlineData("LFB", RoverCommand.L, RoverCommand.F, RoverCommand.B)]
        [InlineData("bbFrL", RoverCommand.B, RoverCommand.B, RoverCommand.F, RoverCommand.R, RoverCommand.L)]
        [InlineData("Ll", RoverCommand.L, RoverCommand.L)]
        public void CommandParser_ParseCommands_CorrectlyParsesCommands(string commands, params RoverCommand[] parsedCommands)
        {
            var sut = GetSystemUnderTest();

            var result = sut.ParseCommands(commands);

            Assert.NotNull(result);
            Assert.True(parsedCommands.SequenceEqual(result));
        }


        [Theory]
        [InlineData("LRbxbF")]
        [InlineData("K")]
        [InlineData("BFLLBFR>l")]
        [InlineData("123")]
        public void CommandParser_ParseCommands_ThrowsExceptionIfCommandInvalid(string commands)
        {
            var sut = GetSystemUnderTest();

            var exception = Assert.Throws<ArgumentException>(() => sut.ParseCommands(commands));

            Assert.Equal("Command string could not be parsed", exception.Message);
        }

        private CommandParser GetSystemUnderTest()
        {
            return new CommandParser();
        }
    }
}
