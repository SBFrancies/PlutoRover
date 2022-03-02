using PlutoRover.Api.Enums;
using PlutoRover.Api.Models.Rover;
using System;

namespace PlutoRover.Api.Models.Request
{
    public class LaunchRoverRequest
    {
        public int MaxX { get; set; } = 100;

        public int MaxY { get; set; } = 100;

        public int StartX { get; set; } = 0;
        public int StartY { get; set; } = 0;

        public Direction StartDirection { get; set; } = Direction.North;

        public string RoverName { get; set; } = "Rover";

        public string MapName { get; set; } = "Pluto";

        public (bool isValid, string errorMessage) Validate()
        {
            if(MaxX <= 0)
            {
                return (false, "MaxX must be greater than 0");
            }

            if (MaxY <= 0)
            {
                return (false, "MaxY must be greater than 0");
            }

            if(StartX > MaxX)
            {
                return (false, "StartX cannot be greater than MaxX");
            }

            if (StartY > MaxY)
            {
                return (false, "StartY cannot be greater than MaxY");
            }

            if(!Enum.IsDefined(typeof(Direction), StartDirection))
            {
                return (false, "StartDirection must be a defined member of the Direction enum");
            }

            return (true, null);
        }
    }
}
