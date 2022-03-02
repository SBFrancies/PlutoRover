using PlutoRover.Api.Enums;
using PlutoRover.Api.Models.Rover;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PlutoRover.UnitTests.Models.Rover
{
    public class RoverTests
    {
       [Theory]
       [InlineData(Direction.North, Direction.East)]
        [InlineData(Direction.East, Direction.South)]
        [InlineData(Direction.South, Direction.West)]
        [InlineData(Direction.West, Direction.North)]
        public void Rover_Move_RCommandMovesRight(Direction initialDirection, Direction directionAfterCommand)
        {
            var sut = GetSystemUnderTest();
            sut.Direction = initialDirection;
            var startX = sut.X;
            var startY = sut.Y;

            sut.Move(RoverCommand.R);

            Assert.Equal(startX, sut.X);
            Assert.Equal(startY, sut.Y);

            Assert.Equal(directionAfterCommand, sut.Direction);
        }

        [Theory]
        [InlineData(Direction.North, Direction.West)]
        [InlineData(Direction.East, Direction.North)]
        [InlineData(Direction.South, Direction.East)]
        [InlineData(Direction.West, Direction.South)]
        public void Rover_Move_LCommandMovesLeft(Direction initialDirection, Direction directionAfterCommand)
        {
            var sut = GetSystemUnderTest();
            sut.Direction = initialDirection;
            var startX = sut.X;
            var startY = sut.Y;

            sut.Move(RoverCommand.L);

            Assert.Equal(startX, sut.X);
            Assert.Equal(startY, sut.Y);

            Assert.Equal(directionAfterCommand, sut.Direction);
        }


        [Theory]
        [InlineData(Direction.North, 50, 50, 50, 51)]
        [InlineData(Direction.East, 50, 50, 51, 50)]
        [InlineData(Direction.South, 50, 50, 50, 49)]
        [InlineData(Direction.West, 50, 50, 49, 50)]
        public void Rover_Move_FCommandMovesForward(Direction initialDirection, int startX, int startY, int endX, int endY)
        {
            var sut = GetSystemUnderTest();
            sut.Direction = initialDirection;
            sut.X = startX;
            sut.Y = startY;

            sut.Move(RoverCommand.F);
            Assert.Equal(initialDirection, sut.Direction);
            Assert.Equal(endX, sut.X);
            Assert.Equal(endY, sut.Y);
        }

        [Theory]
        [InlineData(Direction.North, 50, 50, 50, 49)]
        [InlineData(Direction.East, 50, 50, 49, 50)]
        [InlineData(Direction.South, 50, 50, 50, 51)]
        [InlineData(Direction.West, 50, 50, 51, 50)]
        public void Rover_Move_BCommandMovesBackwards(Direction initialDirection, int startX, int startY, int endX, int endY)
        {
            var sut = GetSystemUnderTest();
            sut.Direction = initialDirection;
            sut.X = startX;
            sut.Y = startY;

            sut.Move(RoverCommand.B);

            Assert.Equal(initialDirection, sut.Direction);
            Assert.Equal(endX, sut.X);
            Assert.Equal(endY, sut.Y);
        }

        [Theory]
        [InlineData(Direction.North, RoverCommand.F, 0, 100)]
        [InlineData(Direction.East, RoverCommand.F, 100, 0)]
        [InlineData(Direction.South, RoverCommand.F, 0, 0)]
        [InlineData(Direction.West, RoverCommand.F, 0, 0)]
        [InlineData(Direction.North, RoverCommand.B, 0, 0)]
        [InlineData(Direction.East, RoverCommand.B, 0, 0)]
        [InlineData(Direction.South, RoverCommand.B, 0, 100)]
        [InlineData(Direction.West, RoverCommand.B, 100, 0)]
        public void Rover_Move_CannotMoveOutOfBounds(Direction initialDirection, RoverCommand command, int startX, int startY)
        {
            var sut = GetSystemUnderTest();
            sut.Direction = initialDirection;
            sut.X = startX;
            sut.Y = startY;

            sut.Move(command);

            Assert.Equal(initialDirection, sut.Direction);
            Assert.Equal(startX, sut.X);
            Assert.Equal(startY, sut.Y);
        }

        private Api.Models.Rover.Rover GetSystemUnderTest()
        {
            return new Api.Models.Rover.Rover
            {
                Direction = Direction.North,
                Id = Guid.NewGuid(),
                Name = "Test Rover",
                X = 0,
                Y = 0,
                Map = new PlutoMap
                {
                    Id = Guid.NewGuid(),
                    Name = "Test Map",
                    MaxX = 100,
                    MaxY = 100,
                }
            };

        }
    }
}
