using MediatR;
using Microsoft.EntityFrameworkCore;
using yummyApp.Application.Abstract.DbContext;

namespace yummyApp.Application.Features.Users.Commands.Update
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommandRequest, UpdateUserCommandResponse>
    {
        readonly IYummyAppDbContext _context;

        public UpdateUserCommandHandler(IYummyAppDbContext context)
        {
            _context = context;
        }

        public async Task<UpdateUserCommandResponse> Handle(UpdateUserCommandRequest request, CancellationToken cancellationToken)
        {
            var user = await _context.AppUsers.FirstOrDefaultAsync(x=> x.Id == request.Id);
            if (user != null)
            {
                user.Name = request.Name;
                user.Surname = request.Surname;
                user.UserName = request.UserName;
                user.PhoneNumber = request.PhoneNumber;
                user.Birthday = request.Birthday;
                user.IsActive = request.IsActive;
                user.Email = request.Email;
                await _context.SaveChangesAsync();
            }
            return new();
        }
    }
}
