using MediatR;
using Microsoft.EntityFrameworkCore;
using yummyApp.Application.Abstract.DbContext;
using yummyApp.Application.Services.Users;

namespace yummyApp.Application.Features.Users.Queries.GetAll
{
    public class GetAllUserQueryHandler : IRequestHandler<GetAllUserQueryRequest, GetAllUserQueryResponse>
    {
        readonly IUserService _userService;
        readonly IYummyAppDbContext _context;

        public GetAllUserQueryHandler(IUserService userService, IYummyAppDbContext context)
        {
            _userService = userService;
            _context = context;
        }

        public async Task<GetAllUserQueryResponse> Handle(GetAllUserQueryRequest request, CancellationToken cancellationToken)
        {
            var totalCount = _context.AppUsers.Count();
            var users = _context.AppUsers.Skip(request.Page * request.Size).Take(request.Size).Select(u => new
            {
                u.Name,
                u.Surname, 
                u.UserName,
                u.Birthday,
                u.Gender,
                u.IsActive,
                u.PhoneNumber,
                u.Email,
                
            }).ToList();
            return new()
            {
                Users = users,
                TotalUserCount = totalCount,
            };
        }
    }
}
