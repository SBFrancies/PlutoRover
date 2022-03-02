using PlutoRover.Api.Enums;
using System;

namespace PlutoRover.Api.Interface
{
    public interface IRover
    {
        Guid Id { get; }

        void Move(RoverCommand command);
    }
}
