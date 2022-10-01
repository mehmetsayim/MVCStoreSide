using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCStoreData;
using System.Data;

namespace MVCStoreeWeb.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class RayonsController : Controller
    {
        private readonly AppDbContext context;

        public RayonsController(
           AppDbContext context
            )
        {
            this.context = context;
        }
        public async Task<IActionResult> Index()
        {
            var model = await context.Rayons.ToListAsync();
            return View(model);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View(new Rayon {Enabled=true });
        }
        [HttpPost]
        public async Task<IActionResult> Create(Rayon model)
        {
            model.DateCreated= DateTime.UtcNow;
            model.Enabled = true;
            context.Rayons.Add(model);
            try
            {
                await context.SaveChangesAsync();
                TempData["success"] = "Reyon ekleme işlemi başarıyla tamamlanmıştır";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["error"] = "Aynı isimde başka bir reyon olduğundan ekleme işlemi yapılamaz ";
                return View(model);
            }
                 
        }
        [HttpGet]
        public  async Task<IActionResult> Edit(Guid id)
        {
            var model =  await context.Rayons.FindAsync(id);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Rayon model)
        {
          
            context.Rayons.Update(model);
            try
            {
                await context.SaveChangesAsync();
                TempData["success"] = "Reyon güncelleme işlemi başarıyla tamamlanmıştır";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex) 
            {
                TempData["error"] = "Aynı isimde başka bir reyon olduğundan güncelleme işlemi yapılamaz ";
                return View(model);
            }

        }
        [HttpGet]
        public async Task<IActionResult> Remove(Guid id)
        {
            var model = await context.Rayons.FindAsync(id);
            context.Rayons.Remove(model);
            try
            {
                await context.SaveChangesAsync();
                TempData["success"] = "Reyon silme işlemi başarıyla tamamlanmıştır";

                return RedirectToAction(nameof(Index));

            }
            catch (Exception)
            {

                TempData["error"] = $"{model.Name} isimli reyon bir ya da daha fazla  kayıt ile ilişkili olduğu için  silme işlemi gerçekleştirelimiyor.";

            }          
                return RedirectToAction(nameof(Index));          
        }
    }

}
