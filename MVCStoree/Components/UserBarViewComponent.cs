using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MVCStoreeWeb.Components
{
   
    public class UserBarViewComponent:ViewComponent
    {
        [Authorize]
        public IViewComponentResult Invoke()
        {
              return View();
        }
    }
}
    