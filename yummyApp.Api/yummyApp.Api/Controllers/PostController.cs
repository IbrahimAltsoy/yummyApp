using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using yummyApp.Application.Features.Posts.Commands.Create;
using yummyApp.Application.Features.Posts.Commands.Delete;
using yummyApp.Application.Features.Posts.Commands.Update;
using yummyApp.Application.Features.Posts.Queries.GetAll;
using yummyApp.Application.Features.Posts.Queries.GetById;

namespace yummyApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        readonly IMediator _mediator;

        public PostController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]GetAllPostQueryRequest request)
        {
            IList<GetAllPostQueryResponse> response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromQuery]GetByIdPostQueryRequest request)
        {
            GetByIdPostQueryResponse response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> Post(CreatePostCommandRequest request)
        {
            CreatePostCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpPut]
        public async Task<IActionResult> Update(UpdatePostCommandRequest request)
        {
            UpdatePostCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(DeletePostCommandRequest request)
        {
            DeletePostCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
