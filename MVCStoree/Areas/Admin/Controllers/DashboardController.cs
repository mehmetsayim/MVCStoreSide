using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MVCStoreeWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Administrators")]

    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
