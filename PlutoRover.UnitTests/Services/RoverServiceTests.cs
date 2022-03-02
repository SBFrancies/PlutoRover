using AutoFixture;
using AutoFixture.AutoMoq;
using Moq;
using PlutoRover.Api.Interface;
using PlutoRover.Api.Models.Request;
using PlutoRover.Api.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace PlutoRover.UnitTests.Services
{
    public class RoverServiceTests
    {
        private readonly Mock<IRoverCommandModule> _mockRoverCommandModule = new Mock<IRoverCommandModule>();
        private readonly Mock<IRoverLauncher> _mockRoverLauncher = new Mock<IRoverLauncher>();
        private readonly Mock<IRoverRepository> _mockRoverRepository = new Mock<IRoverRepository>();
        private readonly Fixture _fixture = new Fixture();

        public RoverServiceTests()
        {
            _fixture.Customize(new AutoMoqCustomization());
        }

        [Fact]
        public void RoverService_CommandRover_ReturnsNullIfRoverNotFound()
        {
            var id = Guid.NewGuid();
            var commands = It.IsAny<string>();
            IRover rover = null;

            _mockRoverRepository.Setup(a => a.GetRover(id)).Returns(rover);
            var sut = GetSystemUnderTest();

            var result = sut.CommandRover(id, commands);

            Assert.Null(result);
            _mockRoverRepository.Verify(a => a.GetRover(id), Times.Once);
            _mockRoverCommandModule.Verify(a => a.CommandRover(It.IsAny<IRover>(), commands), Times.Never);
        }

        [Fact]
        public void RoverService_CommandRover_ReturnsAndCommandsRoverIfRoverFound()
        {
            var id = Guid.NewGuid();
            var commands = It.IsAny<string>();
            var mockRover = new Mock<IRover>();
            mockRover.SetupGet(a => a.Id).Returns(id);

            _mockRoverRepository.Setup(a => a.GetRover(id)).Returns(mockRover.Object);
            _mockRoverCommandModule.Setup(a => a.CommandRover(mockRover.Object, commands)).Returns(mockRover.Object);
            var sut = GetSystemUnderTest();

            var result = sut.CommandRover(id, commands);

            Assert.NotNull(result);
            Assert.Equal(id, result.Id);
            _mockRoverRepository.Verify(a => a.GetRover(id), Times.Once);
            _mockRoverCommandModule.Verify(a => a.CommandRover(mockRover.Object, commands), Times.Once);
        }

        [Fact]
        public void RoverService_GetRover_ReturnsRoverIfFound()
        {
            var id = Guid.NewGuid();
            var mockRover = new Mock<IRover>();
            mockRover.SetupGet(a => a.Id).Returns(id);
            _mockRoverRepository.Setup(a => a.GetRover(id)).Returns(mockRover.Object);

            var sut = GetSystemUnderTest();

            var result = sut.GetRover(id);

            Assert.Equal(id, result.Id);
            _mockRoverRepository.Verify(a => a.GetRover(id), Times.Once);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(200)]
        [InlineData(0)]
        [InlineData(13)]
        public void RoverService_GetRovers_ReturnsAllRovers(int roverCount)
        {
            var rovers = _fixture.CreateMany<IRover>(roverCount);
            _mockRoverRepository.Setup(a => a.GetRovers()).Returns(rovers);

            var sut = GetSystemUnderTest();

            var result = sut.GetRovers();

            Assert.Equal(roverCount, result.Count());
            Assert.True(Enumerable.SequenceEqual(rovers, result));

            _mockRoverRepository.Verify(a => a.GetRovers(), Times.Once);
        }

        [Fact]
        public void RoverService_LaunchRover_TriggersLaunchAndReturnsRover()
        {
            var request = _fixture.Create<LaunchRoverRequest>();
            var mockRover = new Mock<IRover>();
            _mockRoverLauncher.Setup(a => a.LaunchRover(request)).Returns(mockRover.Object);
            var sut = GetSystemUnderTest();

            var result = sut.LaunchRover(request);

            Assert.Equal(mockRover.Object, result);
            _mockRoverLauncher.Verify(a => a.LaunchRover(request), Times.Once);
            _mockRoverRepository.Verify(a => a.SaveRover(mockRover.Object), Times.Once);
        }

        private RoverService GetSystemUnderTest()
        {
            return new RoverService(_mockRoverCommandModule.Object, _mockRoverLauncher.Object, _mockRoverRepository.Object);
        }
    }
}
    