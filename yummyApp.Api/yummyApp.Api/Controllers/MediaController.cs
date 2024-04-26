using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using yummyApp.Application.Features.Medias.Commands.Create;
using yummyApp.Application.Features.Medias.Commands.Delete;
using yummyApp.Application.Features.Medias.Commands.Update;
using yummyApp.Application.Features.Medias.Queries.GetAll;
using yummyApp.Persistance.Migrations;

namespace yummyApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MediaController : ControllerBase
    {
        readonly IMediator _mediator;

        public MediaController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]GetAllMediaQueryRequest request)
        {
            GetAllMediaQueryResponse response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> Post(CreateMediaCommandRequest request)
        {
            CreateMediaCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(DeleteMediaCommandRequest request)
        {
            DeleteMediaCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpPut]
        public async Task<IActionResult> Update(UpdateMediaCommandRequest request)
        {
            UpdateMediaCommandResponse response = await _mediator.Send(request);
            return Ok(response);

        }
    }
    
}
