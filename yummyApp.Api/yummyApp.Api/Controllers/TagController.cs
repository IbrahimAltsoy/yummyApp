using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using yummyApp.Application.Features.Tags.Commands.Create;
using yummyApp.Application.Features.Tags.Commands.Update;

namespace yummyApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        readonly IMediator _mediator;

        public TagController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateTagCommandRequest request)
        {
            CreateTagCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpPut]
        public async Task<IActionResult> Update(UpdateTagCommandRequest request)
        {
            UpdateTagCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
