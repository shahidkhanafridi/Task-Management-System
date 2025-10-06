using Microsoft.AspNetCore.Identity;

namespace TaskMgmtSys.Web.Entities
{
    public class AppUserClaim : IdentityUserClaim<long>
    {
        public virtual AppUser User { get; set; } = default!;
    }
}
