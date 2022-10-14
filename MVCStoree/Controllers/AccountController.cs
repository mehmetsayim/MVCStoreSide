using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVCStoreData;
using MVCStoreeWeb.Models;
using NETCore.MailKit.Core;

namespace MVCStoreeWeb.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IEmailService emailService;
        private readonly IWebHostEnvironment env;

        public AccountController(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            IEmailService emailService,
            IWebHostEnvironment env
            )
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.emailService = emailService;
            this.env = env;
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
            return View( new RegisterViewModel { });
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {

            

            var user = new ApplicationUser
            {
                UserName = model.UserName,
                Name = model.Name,
                Gender = model.Gender,
                BirthDate = model.BirthDate,
                EmailConfirmed = false

            };
             var result= await  userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {

                // TODO :MAİL GÖNDER
                var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                var body = string.Format(
                    System.IO.File.ReadAllText(Path.Combine(env.WebRootPath, "templates", "EmailConfirmation.html")),
                    model.Name,
                    token);
                await emailService.SendAsync(
                    mailTo: model.UserName,
                    subject: "MVCStore e-posta doğrulama mesajı",
                    message:body,
                    isHtml:true


                    );
                return RedirectToAction("Index", "Home");
            }
            else
            {
                result.Errors
                    .ToList()
                    .ForEach(p=> ModelState.AddModelError("", p.Description));
                     return View(model);
            }

         
        }


        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(Guid id, string token)
        { 
          var user = await userManager.FindByIdAsync(id.ToString());
            if (user == null)
                return View("UserNotFound");
            var result = await userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
                return View("EmailConfirmed");
            else
                return View("InvalidToken");
            // TODO 
        }

    }

}
    