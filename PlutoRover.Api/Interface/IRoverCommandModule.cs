using PlutoRover.Api.Models.Rover;
using System;

namespace PlutoRover.Api.Interface
{
    public interface IRoverCommandModule
    {
        IRover CommandRover(IRover rover, string commands);
    }
}
