using MediatR;
using Microsoft.AspNetCore.Identity;
using yummyApp.Application.Abstract.DbContext;
using yummyApp.Application.Repositories.Repository;
using yummyApp.Application.Services.Users;
using yummyApp.Domain.Identity;

namespace yummyApp.Application.Features.Users.Commands.VerifyEmail
{
    public class VerifyEmailCommandHandler : IRequestHandler<VerifyEmailCommandRequest, VerifyEmailCommandResponse>
    {
        readonly UserManager<AppUser> _userManager;
        readonly IUserService _userService;
       
        public VerifyEmailCommandHandler(UserManager<AppUser> userManager, IUserService userService)
        {
            _userManager = userManager;
            _userService = userService;            
           
        }

        public async Task<VerifyEmailCommandResponse> Handle(VerifyEmailCommandRequest request, CancellationToken cancellationToken)
        {
            AppUser? user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null || user.ActivationCode != request.ActivationCode)
                return new VerifyEmailCommandResponse()
                {
                    Succeeded = false,
                    Message = "Doğrulama başarısız! Lütfen kontrol ediniz!"
                };

            user.IsActive = true; 
            user.ActivationCode = "";
            user.EmailConfirmed = true;
            await _userManager.UpdateAsync(user);
            await _userManager.AddToRoleAsync(user, "User");
            return new VerifyEmailCommandResponse()
            {
                Succeeded = true,
                Message = "Doğrulama başarıyla gerçekleşti."
            };
        }
    }
}
