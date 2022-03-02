using PlutoRover.Api.Interface;
using System;

namespace PlutoRover.Api.Models.Rover
{
    public class PlutoMap : IMap
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int MaxX { get; set; }

        public int MaxY { get; set; }
    }
}
