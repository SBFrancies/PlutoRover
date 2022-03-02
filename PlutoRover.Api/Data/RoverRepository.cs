using PlutoRover.Api.Interface;
using PlutoRover.Api.Models.Rover;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace PlutoRover.Api.Data
{
    public class RoverRepository : IRoverRepository
    {
        private ConcurrentDictionary<Guid, IRover> Rovers { get; } = new ConcurrentDictionary<Guid, IRover>();

        public bool SaveRover(IRover rover)
        {
           return Rovers.TryAdd(rover.Id, rover);
        }

        public IRover GetRover(Guid roverId)
        {
            Rovers.TryGetValue(roverId, out var rover);

            return rover;
        }

        public IEnumerable<IRover> GetRovers()
        {
            return Rovers.Values.AsEnumerable();
        }
    }
}
