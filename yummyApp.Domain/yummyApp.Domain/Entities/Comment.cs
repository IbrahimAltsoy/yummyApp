﻿using yummyApp.Domain.Common;
using yummyApp.Domain.Identity;

namespace yummyApp.Domain.Entities
{
    public class Comment: BaseAuditableEntity<Guid>
    {
        public string? Title { get; set; }
        public string? Content { get; set; }
        public int? LikeCount { get; set; }
        public DateTime? Timestamp { get; set; }

        public Guid? UserID { get; set; }
        public Guid? PostID { get; set; }

        public AppUser? User { get; set; }
        public Post? Post { get; set; }
    }
}
