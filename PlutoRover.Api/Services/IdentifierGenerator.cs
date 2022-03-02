using PlutoRover.Api.Interface;
using System;

namespace PlutoRover.Api.Services
{
    public class IdentifierGenerator : IIdentifierGenerator
    {
        public Guid GenerateId()
        {
            return Guid.NewGuid();
        }
    }
}
