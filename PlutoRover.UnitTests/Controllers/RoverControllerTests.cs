using Microsoft.AspNetCore.Mvc;
using Moq;
using PlutoRover.Api.Controllers;
using PlutoRover.Api.Interface;
using PlutoRover.Api.Models.Request;
using PlutoRover.Api.Models.Rover;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Xunit;

namespace PlutoRover.UnitTests.Controllers
{
    public class RoverControllerTests
    {
        private readonly Mock<IRoverService> _mockRoverService = new Mock<IRoverService>();

        [Fact]
        public void RoverController_Post_Returns422ForInvalidRequest()
        {
            var request = new MoveRoverRequest { Commands = "SJBDSDJSDSDS" };

            var sut = GetSystemUnderTest();

            var result = sut.Post(Guid.NewGuid(), request);

            Assert.NotNull(result);
            Assert.IsType<ObjectResult>(result);
            var objectResult = (ObjectResult)result;
            Assert.Equal((int)HttpStatusCode.UnprocessableEntity, objectResult.StatusCode);
            _mockRoverService.Verify(a => a.CommandRover(It.IsAny<Guid>(), request.Commands), Times.Never);
        }

        [Fact]
        public void RoverController_Post_Returns404WhenRoverNotFound()
        {
            var request = new MoveRoverRequest { Commands = "FBLR" };
            var id = Guid.NewGuid();
            _mockRoverService.Setup(a => a.CommandRover(id, request.Commands)).Returns((IRover)null);
            var sut = GetSystemUnderTest();

            var result = sut.Post(id, request);

            Assert.NotNull(result);
            Assert.IsType<NotFoundResult>(result);
            var objectResult = (NotFoundResult)result;
            Assert.Equal((int)HttpStatusCode.NotFound, objectResult.StatusCode);
            _mockRoverService.Verify(a => a.CommandRover(id, request.Commands), Times.Once);
        }

        [Fact]
        public void RoverController_Post_ReturnsOkayWhenRoverFound()
        {
            var request = new MoveRoverRequest { Commands = "FBLR" };
            var id = Guid.NewGuid();
            var mockRover = new Mock<IRover>();
            _mockRoverService.Setup(a => a.CommandRover(id, request.Commands)).Returns(mockRover.Object);
            var sut = GetSystemUnderTest();

            var result = sut.Post(id, request);

            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var objectResult = (OkObjectResult)result;
            Assert.Equal((int)HttpStatusCode.OK, objectResult.StatusCode);
            Assert.Equal(mockRover.Object, objectResult.Value);
            _mockRoverService.Verify(a => a.CommandRover(id, request.Commands), Times.Once);
        }

        [Fact]
        public void RoverController_Post_Return422WhenLaunchRequestInvalid()
        {
            var request = new LaunchRoverRequest { MaxX = -1 };
            var sut = GetSystemUnderTest();

            var result = sut.Post(request);

            Assert.NotNull(result);
            Assert.IsType<ObjectResult>(result);
            var objectResult = (ObjectResult)result;
            Assert.Equal((int)HttpStatusCode.UnprocessableEntity, objectResult.StatusCode);
            _mockRoverService.Verify(a => a.LaunchRover(request), Times.Never);
        }


        [Fact]
        public void RoverController_Post_ReturnsOkayWhenRoverLaunches()
        {
            var request = new LaunchRoverRequest 
            { 
                MaxX = 100, 
                MaxY = 200,                
            };
            var mockRover = new Mock<IRover>();
            _mockRoverService.Setup(a => a.LaunchRover(request)).Returns(mockRover.Object);
            var sut = GetSystemUnderTest();

            var result = sut.Post(request);

            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var objectResult = (OkObjectResult)result;
            Assert.Equal((int)HttpStatusCode.OK, objectResult.StatusCode);
            Assert.Equal(mockRover.Object, objectResult.Value);
            _mockRoverService.Verify(a => a.LaunchRover(request), Times.Once);
        }

        [Fact]
        public void RoverController_Get_Returns404WhenNoRoversFound()
        {
            _mockRoverService.Setup(a => a.GetRovers()).Returns(Array.Empty<IRover>());

            var sut = GetSystemUnderTest();

            var result = sut.Get();

            Assert.NotNull(result);
            Assert.IsType<NotFoundResult>(result);
            var objectResult = (NotFoundResult)result;
            Assert.Equal((int)HttpStatusCode.NotFound, objectResult.StatusCode);
            _mockRoverService.Verify(a => a.GetRovers(), Times.Once);
        }


        [Fact]
        public void RoverController_Get_ReturnsOkayWhenOneOrMoreRoversFound()
        {
            var mockRover = new Mock<IRover>();
            _mockRoverService.Setup(a => a.GetRovers()).Returns(new[] {mockRover.Object});

            var sut = GetSystemUnderTest();

            var result = sut.Get();

            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var objectResult = (OkObjectResult)result;
            Assert.Equal((int)HttpStatusCode.OK, objectResult.StatusCode);
            Assert.Equal(new[] { mockRover.Object }, objectResult.Value);
            _mockRoverService.Verify(a => a.GetRovers(), Times.Once);
        }

        [Fact]
        public void RoverController_Get_Returns404WhenNoRoverFound()
        {
            var id = Guid.NewGuid();
            _mockRoverService.Setup(a => a.GetRover(id)).Returns((IRover)null);

            var sut = GetSystemUnderTest();

            var result = sut.Get(id);

            Assert.NotNull(result);
            Assert.IsType<NotFoundResult>(result);
            var objectResult = (NotFoundResult)result;
            Assert.Equal((int)HttpStatusCode.NotFound, objectResult.StatusCode);
            _mockRoverService.Verify(a => a.GetRover(id), Times.Once);
        }


        [Fact]
        public void RoverController_Get_ReturnsOkayWhenRoverFound()
        {
            var id = Guid.NewGuid();
            var mockRover = new Mock<IRover>();
            _mockRoverService.Setup(a => a.GetRover(id)).Returns(mockRover.Object);

            var sut = GetSystemUnderTest();

            var result = sut.Get(id);

            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var objectResult = (OkObjectResult)result;
            Assert.Equal((int)HttpStatusCode.OK, objectResult.StatusCode);
            Assert.Equal(mockRover.Object, objectResult.Value);
            _mockRoverService.Verify(a => a.GetRover(id), Times.Once);
        }

        private RoverController GetSystemUnderTest()
        {
            return new RoverController(_mockRoverService.Object);
        }
    }
}
