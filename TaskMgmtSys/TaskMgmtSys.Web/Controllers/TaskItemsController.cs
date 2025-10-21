using Microsoft.AspNetCore.Mvc;

namespace TaskMgmtSys.Web.Controllers
{
    public class TaskItemsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
