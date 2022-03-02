using System;

namespace PlutoRover.Api.Interface
{
    public interface IIdentifierGenerator
    {
        Guid GenerateId();
    }
}
