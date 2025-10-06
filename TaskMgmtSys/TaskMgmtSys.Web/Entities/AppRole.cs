using Microsoft.AspNetCore.Identity;

namespace TaskMgmtSys.Web.Entities
{
    public class AppRole : IdentityRole<long>, IAuditableEntity
    {
        public string? Description { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public virtual ICollection<AppUserRole>? UserRoles { get; set; }
        public virtual ICollection<AppRoleClaim> RoleClaims { get; set; } = new List<AppRoleClaim>();
    }
}
