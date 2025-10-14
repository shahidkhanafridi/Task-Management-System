namespace TaskMgmtSys.Web.Entities
{
    public interface IAuditableEntity
    {
        long? CreatedBy { get; set; }
        DateTime? CreatedAt { get; set; }
        long? UpdatedBy { get; set; }
        DateTime? UpdatedAt { get; set; }
    }
    public abstract class AuditableEntity : IAuditableEntity
    {
        public long? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
    public abstract class BaseEntityIsActive : AuditableEntity
    {
        public bool IsActive { get; set; }
    }
    public abstract class BaseEntityIsDeleted : AuditableEntity
    {
        public bool IsDeleted { get; set; }
    }
    public abstract class BaseEntity : AuditableEntity
    {
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
