using Microsoft.AspNetCore.Identity;

namespace TaskMgmtSys.Web.Entities
{
    public class AppUserLogin : IdentityUserLogin<long>
    {
        public virtual AppUser User { get; set; } = default!;
    }
}
