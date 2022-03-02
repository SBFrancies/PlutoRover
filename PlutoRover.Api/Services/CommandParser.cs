using PlutoRover.Api.Interface;
using System;
using System.Collections.Generic;

namespace PlutoRover.Api.Services
{
    public class CommandParser : ICommandParser
    {
        public IList<Enums.RoverCommand> ParseCommands(string commands)
        {
            if(string.IsNullOrEmpty(commands))
            {
                return new List<Enums.RoverCommand>();
            }

            var list = new List<Enums.RoverCommand>();

            foreach(var c in commands)
            {
                if(Enum.IsDefined(typeof(Enums.RoverCommand), c.ToString().ToUpperInvariant()))
                {
                    list.Add(Enum.Parse<Enums.RoverCommand>(c.ToString().ToUpperInvariant()));
                }
                else
                {
                    throw new ArgumentException("Command string could not be parsed");
                }
            }

            return list;
        }
    }
}
