using PlutoRover.Api.Enums;
using PlutoRover.Api.Models.Request;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PlutoRover.UnitTests.Models.Request
{
    public class LaunchRoverRequestTests
    {
        [Fact]
        public void LaunchRoverRequest_Validate_ValidRequestReturnsTrue()
        {
            var request = ValidRequest;

            var result = request.Validate();

            Assert.True(result.isValid);
            Assert.Null(result.errorMessage);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-121)]
        [InlineData(-1)]
        public void LaunchRoverRequest_Validate_InvalidIfMaxXLessThan1(int maxX)
        {
            var request = ValidRequest;
            request.MaxX = maxX;

            var result = request.Validate();

            Assert.False(result.isValid);
            Assert.Equal("MaxX must be greater than 0", result.errorMessage);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-121)]
        [InlineData(-1)]
        public void LaunchRoverRequest_Validate_InvalidIfMaxYLessThan1(int maxY)
        {
            var request = ValidRequest;
            request.MaxY = maxY;

            var result = request.Validate();

            Assert.False(result.isValid);
            Assert.Equal("MaxY must be greater than 0", result.errorMessage);
        }


        [Theory]
        [InlineData(2, 1)]
        [InlineData(12, 2)]
        [InlineData(313131, 232)]
        public void LaunchRoverRequest_Validate_InvalidIfStartXGreaterThanMaxX(int startX, int maxX)
        {
            var request = ValidRequest;
            request.StartX = startX;
            request.MaxX = maxX;

            var result = request.Validate();

            Assert.False(result.isValid);
            Assert.Equal("StartX cannot be greater than MaxX", result.errorMessage);
        }

        [Theory]
        [InlineData(2, 1)]
        [InlineData(12, 2)]
        [InlineData(313131, 232)]
        public void LaunchRoverRequest_Validate_InvalidIfStartYGreaterThanMaxY(int startY, int maxY)
        {
            var request = ValidRequest;
            request.StartY = startY;
            request.MaxY = maxY;

            var result = request.Validate();

            Assert.False(result.isValid);
            Assert.Equal("StartY cannot be greater than MaxY", result.errorMessage);
        }


        [Theory]
        [InlineData(19)]
        [InlineData(122)]
        [InlineData(313131232)]
        public void LaunchRoverRequest_Validate_InvalidIfStartDirectionNotDefined(int direction)
        {
            var request = ValidRequest;
            request.StartDirection = (Direction)direction;

            var result = request.Validate();

            Assert.False(result.isValid);
            Assert.Equal("StartDirection must be a defined member of the Direction enum", result.errorMessage);
        }


        private LaunchRoverRequest ValidRequest => new LaunchRoverRequest
        {
            MapName = "TestMap",
            MaxX = 100,
            MaxY = 100,
            RoverName = "TestRover",
            StartDirection = Direction.North,
            StartX = 0,
            StartY = 0,
        };
    }
}
