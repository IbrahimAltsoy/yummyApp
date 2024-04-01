namespace yummyApp.Domain.Common
{
    public interface IAuditableEntity
    {
        DateTime CreatedAt { get; set; }

        string? CreatedBy { get; set; }

        DateTime? LastModifiedAt { get; set; }

        string? LastModifiedBy { get; set; }

        DateTime? DeletedAt { get; set; }

        string? DeletedBy { get; set; }
    }
}
