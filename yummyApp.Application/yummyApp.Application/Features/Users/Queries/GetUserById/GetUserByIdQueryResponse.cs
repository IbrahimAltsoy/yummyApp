using yummyApp.Domain.Enums;

namespace yummyApp.Application.Features.Users.Queries.GetUserById
{
    public class GetUserByIdQueryResponse
    {
        
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }       
        public string PhoneNumber { get; set; }
        public DateTime? Birthday { get; set; }
        public Gender Gender { get; set; }
        public bool IsActive { get; set; }
        
    }
}