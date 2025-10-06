using Microsoft.AspNetCore.Identity;

namespace TaskMgmtSys.Web.Entities
{
    public class AppRoleClaim : IdentityRoleClaim<long>
    {
        public virtual AppRole Role { get; set; } = default!;
    }
}
