using PlutoRover.Api.Models.Rover;
using System;
using System.Collections.Generic;

namespace PlutoRover.Api.Interface
{
    public interface IRoverRepository
    {
        bool SaveRover(IRover rover);

        IRover GetRover(Guid roverId);

        IEnumerable<IRover> GetRovers();
    }
}
