using PlutoRover.Api.Interface;
using PlutoRover.Api.Models.Request;
using PlutoRover.Api.Models.Rover;
using System;
using System.Collections.Generic;

namespace PlutoRover.Api.Services
{
    public class RoverService : IRoverService
    {
        private IRoverRepository RoverRepository { get; }

        private IRoverLauncher RoverLauncher { get; }

        private IRoverCommandModule RoverCommandModule { get; }

        public RoverService(IRoverCommandModule roverCommandModule, IRoverLauncher roverLauncher, IRoverRepository roverRepository)
        {
            RoverCommandModule = roverCommandModule ?? throw new ArgumentNullException(nameof(roverCommandModule));
            RoverLauncher = roverLauncher ?? throw new ArgumentNullException(nameof(roverLauncher));
            RoverRepository = roverRepository ?? throw new ArgumentNullException(nameof(roverRepository));
        }

        public IRover CommandRover(Guid roverId, string commands)
        {
            var rover = RoverRepository.GetRover(roverId);

            if (rover == null)
            {
                return null;
            }

            return RoverCommandModule.CommandRover(rover, commands);
        }

        public IRover GetRover(Guid roverId)
        {
            return RoverRepository.GetRover(roverId);
        }

        public IEnumerable<IRover> GetRovers()
        {
            return RoverRepository.GetRovers();
        }

        public IRover LaunchRover(LaunchRoverRequest request)
        {
            var rover = RoverLauncher.LaunchRover(request);

            RoverRepository.SaveRover(rover);

            return rover;
        }
    }
}
