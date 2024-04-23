using MediatR;
using Microsoft.AspNetCore.Mvc;
using yummyApp.Application.Features.Comments.Commands.Create;
using yummyApp.Application.Features.Comments.Commands.Delete;
using yummyApp.Application.Features.Comments.Commands.Update;

namespace yummyApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        readonly IMediator _mediator;

        public CommentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateCommentCommandRequest request)
        {
            CreateCommentCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpPut]
        public async Task<IActionResult> Update(UpdateCommentCommandRequest request)
        {
            UpdateCommentCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromBody] DeleteCommentCommandRequest request)
        {
            DeleteCommentCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
