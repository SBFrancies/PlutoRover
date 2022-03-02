using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlutoRover.Api.Interface;
using PlutoRover.Api.Models.Request;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PlutoRover.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RoverController : ControllerBase
    {
        private IRoverService RoverService { get; }
        
        public RoverController(IRoverService roverService)
        {
            RoverService = roverService ?? throw new ArgumentNullException(nameof(roverService));
        }


        [HttpPost]
        [Route("{id:guid}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.UnprocessableEntity)]
        public IActionResult Post([FromRoute]Guid id, [FromBody] MoveRoverRequest request)
        {
            var valid = request.Validate();

            if (!valid.isValid)
            {
                return StatusCode((int)HttpStatusCode.UnprocessableEntity, valid.errorMessage);
            }

            var rover = RoverService.CommandRover(id, request.Commands);

            if (rover == null)
            {
                return NotFound();
            }

            return Ok(rover);
        }


        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.UnprocessableEntity)]
        public IActionResult Post([FromBody]LaunchRoverRequest request)
        {
            var valid = request.Validate();

            if(!valid.isValid)
            {
                return StatusCode((int)HttpStatusCode.UnprocessableEntity, valid.errorMessage);
            }

            var rover = RoverService.LaunchRover(request);

            return Ok(rover);
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult Get()
        {
            var rovers = RoverService.GetRovers();

            if(!rovers.Any())
            {
                return NotFound();
            }

            return Ok(rovers);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult Get([FromRoute] Guid id)
        {
            var rover = RoverService.GetRover(id);

            if(rover == null)
            {
                return NotFound();
            }    

            return Ok(rover);
        }
    }
}
