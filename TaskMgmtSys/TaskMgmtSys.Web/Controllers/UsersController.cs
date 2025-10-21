using Microsoft.AspNetCore.Mvc;

namespace TaskMgmtSys.Web.Controllers
{
    public class UsersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
