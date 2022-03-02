using PlutoRover.Api.Models.Request;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PlutoRover.UnitTests.Models.Request
{
    public class MoveRoverRequestTests
    {
        [Fact]
        public void MoveRoverRequest_Valdate_ValidRequestReturnsTrue()
        {
            var request = ValidRequest;

            var result = request.Validate();

            Assert.True(result.isValid);
            Assert.Null(result.errorMessage);
        }

        [Theory]
        [InlineData("akdskdNKDCD")]
        [InlineData("FBBLRlfRBLxrLLR")]
        [InlineData("123131")]
        public void MoveRoverRequest_Valdate_InvalidCommandReturnsFalse(string commands)
        {
            var request = ValidRequest;
            request.Commands = commands;

            var result = request.Validate();

            Assert.False(result.isValid);
            Assert.Equal("Command string is invalid, only F, B, L and R are valid characters", result.errorMessage);
        }

        private MoveRoverRequest ValidRequest => new MoveRoverRequest
        {
            Commands = "FBBLRlfRBLrLLR"
        };
    }
}
