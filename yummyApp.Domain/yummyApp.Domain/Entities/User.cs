using System.ComponentModel.DataAnnotations.Schema;
using yummyApp.Domain.Common;
using yummyApp.Domain.Enums;
namespace yummyApp.Domain.Entities
{
    public class User : BaseAuditableEntity<Guid>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Phone { get; set; }
        public string ConfirmPhone { get; set; }
        public DateTime Birthday { get; set; }
        public bool IsActive { get; set; }
        public Gender Gender { get; set; }

        public ICollection<UserRating> Ratings { get; set; }
        public ICollection<UserLocation> Locations { get; set; }
        public ICollection<UserReview> Reviews { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Friendship> Friendships { get; set; }
        public ICollection<Post> Posts { get; set; }
        public ICollection<Like> Likes { get; set; }
        public ICollection<Message> SentMessages { get; set; }
        public ICollection<Message> ReceivedMessages { get; set; }
        public ICollection<BusinessReview> BusinessReviews { get; set; }
    }
}
