using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVCStoreData;
using MVCStoreeWeb.Models;

namespace MVCStoreeWeb.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(SignInManager<ApplicationUser> signInManager)
        {
            this.signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginViewModel { RememberMe=true});
        }

        [HttpPost]

        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var result = await signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, true);

            if (result.Succeeded)
            {
                return Redirect(model.ReturnUrl ?? "/admin");
            }
            else
            {
                ModelState.AddModelError("", "Geçersiz Kullanıcı Girişi");
                return View(model);
            }
        }
        [HttpGet, Authorize]
        public async Task<IActionResult> Logout()
        { 
         await signInManager.SignOutAsync();
         return RedirectToAction("Index","Home");
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
    }

}
    