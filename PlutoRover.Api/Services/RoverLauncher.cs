using PlutoRover.Api.Interface;
using PlutoRover.Api.Models.Request;
using PlutoRover.Api.Models.Rover;
using System;

namespace PlutoRover.Api.Services
{
    public class RoverLauncher : IRoverLauncher
    {
        private IIdentifierGenerator IdentifierGenerator { get; }

        public RoverLauncher(IIdentifierGenerator identifierGenerator)
        {
            IdentifierGenerator = identifierGenerator ?? throw new ArgumentNullException(nameof(identifierGenerator));
        }

        public IRover LaunchRover(LaunchRoverRequest request)
        {
            return new Rover
            {
                Id = IdentifierGenerator.GenerateId(),
                Name = request.RoverName,
                Direction = request.StartDirection,
                X = request.StartX,
                Y = request.StartY,
                Map = new PlutoMap
                {
                    Id = IdentifierGenerator.GenerateId(),
                    Name = request.MapName,
                    MaxX = request.MaxX,
                    MaxY = request.MaxY,
                },
            };
        }
    }
}
