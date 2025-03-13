using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using yummyApp.Application.BackGroundJobs;
using yummyApp.Application.Features.Users.Commands.Create;
using yummyApp.Application.Features.Users.Commands.Delete;
using yummyApp.Application.Features.Users.Commands.Update;
using yummyApp.Application.Features.Users.Commands.UpdateUserProfileImage;
using yummyApp.Application.Features.Users.Queries.GetAll;
using yummyApp.Application.Features.Users.Queries.GetUserById;

namespace yummyApp.Api.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }
        //[Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllUserQueryRequest request)
        {
            GetAllUserQueryResponse response = await _mediator.Send(request);
            return Ok(response);

        }
        [Authorize]
        [HttpGet("id")]
        public async Task<IActionResult> GetById([FromQuery] GetUserByIdQueryRequest request)
        {
            GetUserByIdQueryResponse response = await _mediator.Send(request);
            return Ok(response);
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CreateUserCommandRequest request)
        {
            CreateUserCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateUserCommandRequest request)
        {
            UpdateUserCommandResponse response = await _mediator.Send(request);

            return Ok("Güncelleme başarılı bir şekilde yapılmıştır.");

        }
        [Authorize]
        [HttpPost("updateImage")]
        public async Task<IActionResult> UpdateImage(IFormFile image )
        {
            UpdateUserProfileImageCommandRequest request = new UpdateUserProfileImageCommandRequest
            {
                Image = image
            };
            UpdateUserProfileImageCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }
        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody]DeleteUserCommandRequest request)
        {
            DeleteUserCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
