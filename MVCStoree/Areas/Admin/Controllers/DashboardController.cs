using Microsoft.AspNetCore.Mvc;

namespace MVCStoreeWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
