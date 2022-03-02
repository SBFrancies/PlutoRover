using PlutoRover.Api.Interface;
using PlutoRover.Api.Models.Rover;
using System;

namespace PlutoRover.Api.Services
{
    public class RoverCommandModule : IRoverCommandModule
    {
        private ICommandParser CommandParser { get; }

        public RoverCommandModule(ICommandParser commandParser)
        {
            CommandParser = commandParser ?? throw new ArgumentNullException(nameof(commandParser));
        }

        public IRover CommandRover(IRover rover, string commands)
        {
            if (rover == null)
            {
                throw new ArgumentNullException(nameof(rover));
            }

            var commandList = CommandParser.ParseCommands(commands);

            foreach (var command in commandList)
            {
                rover.Move(command);
            }

            return rover;
        }
    }
}
