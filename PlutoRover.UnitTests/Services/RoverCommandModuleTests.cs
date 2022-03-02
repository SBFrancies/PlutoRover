using Moq;
using PlutoRover.Api.Enums;
using PlutoRover.Api.Interface;
using PlutoRover.Api.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace PlutoRover.UnitTests.Services
{
    public class RoverCommandModuleTests
    {
        private readonly Mock<ICommandParser> _mockCommandParser = new Mock<ICommandParser>();

        [Theory]
        [InlineData(RoverCommand.L)]
        [InlineData(RoverCommand.R)]
        [InlineData(RoverCommand.F)]
        [InlineData(RoverCommand.B)]
        [InlineData(RoverCommand.F, RoverCommand.R, RoverCommand.L, RoverCommand.B, RoverCommand.B, RoverCommand.B, RoverCommand.F)]
        [InlineData(RoverCommand.L, RoverCommand.F, RoverCommand.B)]
        [InlineData(RoverCommand.B, RoverCommand.B, RoverCommand.F, RoverCommand.R, RoverCommand.L)]
        [InlineData(RoverCommand.L, RoverCommand.L)]
        public void RoverCommandModule_CommandRover_CommandsPassedToRover(params RoverCommand[] commands)
        {
            _mockCommandParser.Setup(a => a.ParseCommands(It.IsAny<string>())).Returns(commands);
            var mockRover = new Mock<IRover>();

            var sut = GetSystemUnderTest();
            var result = sut.CommandRover(mockRover.Object, It.IsAny<string>());

            Assert.NotNull(result);
            Assert.Equal(mockRover.Object, result);

            _mockCommandParser.Verify(a => a.ParseCommands(It.IsAny<string>()), Times.Once);

            foreach(var group in  commands.GroupBy(a => a))
            {
                mockRover.Verify(a => a.Move(group.Key), Times.Exactly(group.Count()));
            }
        }

        private RoverCommandModule GetSystemUnderTest()
        {
            return new RoverCommandModule(_mockCommandParser.Object);
        }
    }
}
