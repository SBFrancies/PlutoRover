using PlutoRover.Api.Models.Request;
using PlutoRover.Api.Models.Rover;

namespace PlutoRover.Api.Interface
{
    public interface IRoverLauncher
    {
        IRover LaunchRover(LaunchRoverRequest request);
    }
}
