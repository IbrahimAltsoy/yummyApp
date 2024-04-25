using MediatR;
using Microsoft.AspNetCore.Mvc;
using yummyApp.Application.Features.FriendShips.Commands.Create;
using yummyApp.Application.Features.FriendShips.Queries.GetAll;

namespace yummyApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FriendShipController : ControllerBase
    {
        readonly IMediator _mediator;

        public FriendShipController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]GetAllFrindShipQueryRequst request)
        {
            GetAllFrindShipQueryResponse response = await _mediator.Send(request);
            return Ok(response);

        }
        [HttpPost]
        public async Task<IActionResult> Post(CreateFrindShipCommandRequest request)
        {
            CreateFrindShipCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
