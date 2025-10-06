using Microsoft.AspNetCore.Identity;

namespace TaskMgmtSys.Web.Entities
{
    public class AppUserToken : IdentityUserToken<long>
    {
        public virtual AppUser User { get; set; } = default!;
    }
}
