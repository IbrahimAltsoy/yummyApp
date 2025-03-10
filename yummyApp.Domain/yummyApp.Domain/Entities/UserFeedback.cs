using yummyApp.Domain.Common;
using yummyApp.Domain.Enums;
using yummyApp.Domain.Identity;

namespace yummyApp.Domain.Entities
{
    public class UserFeedback: BaseAuditableEntity<Guid>
    {
        public Guid? UserID { get; set; }
        public string? Title { get; set; }
        public string? Message { get; set; }
        public UserFeedbackEnum? UserFeedbackEnum { get; set; }
        public string? Email { get; set; }
        public bool? IsAddressed { get; set; }

        public AppUser? User { get; set; }
    }
}
