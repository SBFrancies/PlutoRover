using PlutoRover.Api.Enums;
using System.Collections.Generic;

namespace PlutoRover.Api.Interface
{
    public interface ICommandParser
    {
        IList<RoverCommand> ParseCommands(string commands);
    }
}
