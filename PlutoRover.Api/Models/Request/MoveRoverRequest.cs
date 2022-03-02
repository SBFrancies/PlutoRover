
using PlutoRover.Api.Enums;
using System;
using System.Linq;

namespace PlutoRover.Api.Models.Request
{
    public class MoveRoverRequest
    {
        public string Commands { get; set; }

        public (bool isValid, string errorMessage) Validate()
        {
            if(Commands !=null && !Commands.All(a => Enum.IsDefined(typeof(RoverCommand),a.ToString().ToUpperInvariant())))
            {
                return (false, "Command string is invalid, only F, B, L and R are valid characters");
            }

            return (true, null);
        }
    }
}
