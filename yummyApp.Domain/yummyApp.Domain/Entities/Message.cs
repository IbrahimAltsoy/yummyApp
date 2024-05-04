using yummyApp.Domain.Common;
using yummyApp.Domain.Identity;

namespace yummyApp.Domain.Entities
{
    public class Message:BaseAuditableEntity<Guid>
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }

        public Guid SenderId { get; set; }
        public Guid ReceiverId { get; set; }

        public AppUser Sender { get; set; }
        public AppUser Receiver { get; set; }
    }
}
