using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yummyApp.Domain.Common
{
    public abstract class BaseAuditableEntity<TKey>:BaseEntity<TKey>,IAuditableEntity
    {
        public DateTime CreatedAt { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? LastModifiedAt { get; set; }

        public string? LastModifiedBy { get; set; }

        public DateTime? DeletedAt { get; set; }

        public string? DeletedBy { get; set; }
    }
    public abstract class BaseAuditableEntity : BaseAuditableEntity<int>
    {
    }
}
