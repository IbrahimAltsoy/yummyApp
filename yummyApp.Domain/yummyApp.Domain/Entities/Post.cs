using yummyApp.Domain.Common;
using yummyApp.Domain.Enums;

namespace yummyApp.Domain.Entities
{
    
    public class Post: BaseAuditableEntity<Guid>
    {
        public Guid? UserID { get; set; }
        public string Content { get; set; }
        public string[] MediaURLs { get; set; }
        public DateTime Timestamp { get; set; }
        public PostQuality Quality { get; set; }
        public int Rating { get; set; }

        public List<Tag>? Tags { get; set; }
        public List<Like>? Likes { get; set; }
        public User? User { get; set; }

    }
    
}



