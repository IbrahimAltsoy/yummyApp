using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using yummyApp.Domain.Common;
using yummyApp.Domain.Entities;
using yummyApp.Domain.Enums;
namespace yummyApp.Domain.Identity
{
    public class AppUser :IdentityUser<Guid>,IAuditableEntity<Guid>
    {
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; }=string.Empty;
        public string? Roles { get; set; }
        public string? ActivationKey { get; set; }
        public string? RefreshToken { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime? RefreshTokenEndDate { get; set; }
        public DateTime? Birthday { get; set; }
        public bool? IsActive { get; set; }
        public Gender? Gender { get; set; }
        public string? ActivationCode { get; set; }
        

        public ICollection<UserRating>? Ratings { get; set; }
        public ICollection<UserLocation>? Locations { get; set; }
        public ICollection<UserReview>? Reviews { get; set; }
        public ICollection<Comment>? Comments { get; set; }
        public ICollection<Friendship>? Friendships { get; set; }
        public ICollection<Post>? Posts { get; set; }
        public ICollection<Like>? Likes { get; set; }
        public ICollection<Message>? SentMessages { get; set; }
        public ICollection<Message>? ReceivedMessages { get; set; }
        public ICollection<BusinessReview>? BusinessReviews { get; set; }
        public ICollection<UserFeedback>? UserFeedbacks { get; set; }

        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? LastModifiedAt { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string? DeletedBy { get; set; }
    }
}
