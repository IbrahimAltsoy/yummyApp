using AutoMapper;
using MediatR;
using yummyApp.Application.Abstract.DbContext;
using yummyApp.Application.Repositories;
using yummyApp.Application.Repositories.Repository;
using yummyApp.Application.Services.Users;
using yummyApp.Domain.Events;
using yummyApp.Domain.Identity;

namespace yummyApp.Application.Features.Users.Commands.Create
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
    {
        readonly IUserService _userService;

        public CreateUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
            CreateUserCommandResponse response = await _userService.CreateUserAsync(new()
            {
                Name = request.Name,
                Surname = request.Surname,
                UserName = request.UserName,
                PhoneNumber = request.PhoneNumber,
                Gender = request.Gender,
                Birthday = request.Birthday,
                IsActive = request.IsActive,
                Email = request.Email,
                Password = request.Password,
                PasswordConfirm = request.PasswordConfirm
            });
            return new()
            {
                Message = response.Message,
                Succeeded = response.Succeeded
            };
        }
    }
}
