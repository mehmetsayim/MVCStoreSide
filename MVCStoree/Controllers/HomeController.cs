using Microsoft.AspNetCore.Mvc;
using MVCStoree.Models;
using MVCStoreeWeb.Models;
using NETCore.MailKit.Core;
using System.Diagnostics;

namespace MVCStoree.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEmailService emailService;
        private readonly IConfiguration configuration;

        public HomeController(
            ILogger<HomeController> logger,
            IEmailService emailService,
            IConfiguration configuration
            )
        {
            _logger = logger;
            this.emailService = emailService;
            this.configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AboutUs()
        {
            return View();
        }
        public IActionResult ContactUs()
        {
            return View();
        }
        [HttpPost] 
        public async Task<IActionResult> ContactUs(ContactUsViewModel model)
        {
            await emailService.SendAsync(
                configuration.GetValue<string>("EMailSettings:SenderEmail"),
                $"Ziyaret Mesajı ({model.Name })",
                $"Gönderen:\t{model.Name}\nTel: \t\t{model.PhonoNumber ?? "Tel. Belirtilmemiş"}\nE-Posta \t\t{ model.Email}\nMesaj:\n----\n{model.Message }"                
                );
            TempData["messageSent"] = true;
            return View(new ContactUsViewModel());
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}