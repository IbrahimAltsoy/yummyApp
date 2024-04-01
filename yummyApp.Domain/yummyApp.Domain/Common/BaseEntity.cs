using System.ComponentModel.DataAnnotations.Schema;

namespace yummyApp.Domain.Common
{
    public abstract class BaseEntity<TKey> : IEntity<TKey>
    {
        public TKey Id { get; set; }
     
        readonly List<BaseEvent> _domainEvents = new();

        [NotMapped]
        public IReadOnlyCollection<BaseEvent> DomainEvents => _domainEvents.AsReadOnly();

        public void AddDomainEvent(BaseEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        public void RemoveDomainEvent(BaseEvent domainEvent)
        {
            _domainEvents.Remove(domainEvent);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }


    }
    public abstract class BaseEntity : BaseEntity<int>
    {
    }

    public abstract class BaseListEntity : BaseEntity<int>
    {
        public string Name { get; set; }
    }
}
