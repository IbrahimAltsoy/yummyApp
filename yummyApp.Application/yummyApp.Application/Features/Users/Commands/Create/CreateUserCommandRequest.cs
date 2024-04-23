using MediatR;
using yummyApp.Domain.Entities;
using yummyApp.Domain.Enums;

namespace yummyApp.Application.Features.Users.Commands.Create
{
    public class CreateUserCommandRequest:IRequest<CreateUserCommandResponse>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public Gender Gender { get; set; }
        public bool IsActive { get; set; }
        public string ProfilePicture { get; set; }
        public bool NotificationPreferences { get; set; }
        public bool PrivacySettings { get; set; }

        //public List<Post>? Posts { get; set; }
        //public List<Like>? Likes { get; set; }
        //public List<Comment>? Comments { get; set; }
        //public int? TotalRating
        //{
        //    get
        //    {
        //        int totalRating = 0;
        //        foreach (var post in Posts)
        //        {
        //            totalRating += post.Rating;
        //        }
        //        return totalRating;
        //    }
        //}
        //public List<Friendship>? Followings { get; set; }
        //public List<Friendship>? Followers { get; set; }
    }
}
