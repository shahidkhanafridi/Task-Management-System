using Microsoft.AspNetCore.Identity;

namespace TaskMgmtSys.Web.Entities
{
    public class AppRole : IdentityRole<long>
    {
        public string? Description { get; set; }
        public virtual ICollection<AppUserRole>? UserRoles { get; set; }
        public virtual ICollection<AppRoleClaim> RoleClaims { get; set; } = new List<AppRoleClaim>();
    }
}
