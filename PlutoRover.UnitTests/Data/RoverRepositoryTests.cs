using Moq;
using PlutoRover.Api.Data;
using PlutoRover.Api.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace PlutoRover.UnitTests.Data
{
    public class RoverRepositoryTests
    {
        [Fact]
        public void RoverRepository_SaveRoverGetRover_SavedRoverCanBeRetrieved()
        {
            var id = Guid.NewGuid();
            var mockRover = new Mock<IRover>();
            mockRover.SetupGet(a => a.Id).Returns(id);

            var sut = GetSystemUnderTest();

            var result = sut.SaveRover(mockRover.Object);
            Assert.True(result);

            var rover = sut.GetRover(id);

            Assert.Equal(mockRover.Object, rover);
        }

        [Fact]
        public void RoverRepository_SaveRoverGetRover_CannotSaveRoverWithSameIdTwice()
        {
            var id = Guid.NewGuid();
            var mockRover = new Mock<IRover>();
            mockRover.SetupGet(a => a.Id).Returns(id);
            var mockRover2 = new Mock<IRover>();
            mockRover2.SetupGet(a => a.Id).Returns(id);

            var sut = GetSystemUnderTest();

            var result = sut.SaveRover(mockRover.Object);
            Assert.True(result);

            result = sut.SaveRover(mockRover2.Object);
            Assert.False(result);
        }

        [Theory]
        [InlineData(13)]
        [InlineData(232)]
        [InlineData(1)]
        public void RoverRepositoy_GetRovers_ReturnsAllRovers(int roversToAdd)
        {
            var sut = GetSystemUnderTest();

            for(var i = 0; i < roversToAdd; i++)
            {
                var id = Guid.NewGuid();
                var mockRover = new Mock<IRover>();
                mockRover.SetupGet(a => a.Id).Returns(id);
                var added = sut.SaveRover(mockRover.Object);
                Assert.True(added);
            }

            var result = sut.GetRovers();

            Assert.Equal(roversToAdd, result.Count());
        }

        private RoverRepository GetSystemUnderTest()
        {
            return new RoverRepository();
        }
    }
}
