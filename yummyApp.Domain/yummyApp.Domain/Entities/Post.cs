﻿using yummyApp.Domain.Common;
using yummyApp.Domain.Enums;
using yummyApp.Domain.Identity;

namespace yummyApp.Domain.Entities
{

    public class Post: BaseAuditableEntity<Guid>
    {
        public string? Title { get; set; }
        public string? Content { get; set; }
        public DateTime? Timestamp { get; set; }
        public PostQuality? Quality { get; set; }
        public int? Rating { get; set; }

        public Guid? UserID { get; set; }
        public Guid? BusinessId { get; set; }

        public List<Media>? Media { get; set; }
        public List<Tag>? Tags { get; set; }
        public List<Like>? Likes { get; set; }
        public AppUser? User { get; set; }
        public Business? Business { get; set; }
    }
    
}



