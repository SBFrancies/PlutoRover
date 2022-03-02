using PlutoRover.Api.Models.Request;
using PlutoRover.Api.Models.Rover;
using System;
using System.Collections.Generic;

namespace PlutoRover.Api.Interface
{
    public interface IRoverService
    {
        IRover LaunchRover(LaunchRoverRequest request);

        IRover CommandRover(Guid roverId, string commands);

        IEnumerable<IRover> GetRovers();

        IRover GetRover(Guid roverId);
    }
}
