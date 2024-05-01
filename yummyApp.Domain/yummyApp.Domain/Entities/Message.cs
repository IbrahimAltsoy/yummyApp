using yummyApp.Domain.Common;

namespace yummyApp.Domain.Entities
{
    public class Message:BaseAuditableEntity<Guid>
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }

        public Guid SenderId { get; set; }
        public Guid ReceiverId { get; set; }

        public User Sender { get; set; }
        public User Receiver { get; set; }
    }
}
