using AutoFixture;
using Moq;
using PlutoRover.Api.Interface;
using PlutoRover.Api.Models.Request;
using PlutoRover.Api.Models.Rover;
using PlutoRover.Api.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PlutoRover.UnitTests.Services
{
    public class RoverLauncherTests
    {
        private readonly Mock<IIdentifierGenerator> _mockIdentifierGenerator = new Mock<IIdentifierGenerator>();
        private readonly Fixture _fixture = new Fixture();

        [Fact]
        public void RoverLauncher_LaunchRover_CreatesRover()
        {
            var id = Guid.NewGuid();
            _mockIdentifierGenerator.Setup(a => a.GenerateId()).Returns(id);
            var sut = GetSystemUnderTest();
            var request = _fixture.Create<LaunchRoverRequest>();

            var result = sut.LaunchRover(request);

            Assert.IsType<Rover>(result);
            var rover = (Rover)result;
            Assert.Equal(request.RoverName, rover.Name);
            Assert.Equal(request.StartX, rover.X);
            Assert.Equal(request.StartY, rover.Y);
            Assert.IsType<PlutoMap>(rover.Map);
            var map = (PlutoMap)rover.Map;
            Assert.Equal(request.MapName, map.Name);
            Assert.Equal(request.MaxX, map.MaxX);
            Assert.Equal(request.MaxY, map.MaxY);
            Assert.Equal(id, result.Id);
            _mockIdentifierGenerator.Verify(a => a.GenerateId(), Times.Exactly(2));
        }

        private RoverLauncher GetSystemUnderTest()
        {
            return new RoverLauncher(_mockIdentifierGenerator.Object);
        }
    }
}
