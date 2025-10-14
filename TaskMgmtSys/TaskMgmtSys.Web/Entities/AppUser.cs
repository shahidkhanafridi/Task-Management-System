using Microsoft.AspNetCore.Identity;

namespace TaskMgmtSys.Web.Entities
{
    public class AppUser : IdentityUser<long>, IAuditableEntity
    {
        public required string FirstName { get; set; }
        public string? LastName { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual AppUser? CreatedByUser { get; set; }
        public virtual AppUser? UpdatedByUser { get; set; }

        public virtual ICollection<AppUserRole>? UserRoles { get; set; }
        public virtual ICollection<AppUserClaim> Claims { get; set; } = new List<AppUserClaim>();
        public virtual ICollection<AppUserLogin> Logins { get; set; } = new List<AppUserLogin>();
        public virtual ICollection<AppUserToken> Tokens { get; set; } = new List<AppUserToken>();

        public ICollection<UserProject>? UserProjects { get; set; }
        public ICollection<TaskItem>? TaskItems { get; set; }
        public ICollection<TaskAssignment>? TaskAssignments { get; set; }
        public ICollection<TaskAttachment>? TaskAttachments { get; set; }
        public ICollection<Comment>? Comments { get; set; }
        public ICollection<CommentAttachment>? CommentAttachments { get; set; }
    }
}
