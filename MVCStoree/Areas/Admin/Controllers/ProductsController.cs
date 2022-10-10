using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCStoreData;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System.Data;

namespace MVCStoreeWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrators")]


    public class ProductsController : Controller
    {
        private readonly AppDbContext context;
        private readonly IConfiguration configuration;

        public ProductsController(
           AppDbContext context,
           IConfiguration configuration
            )
        {
            this.context = context;
            this.configuration = configuration;
        }
        public async Task<IActionResult> Index()
        {
            var model = await context.Products.ToListAsync();
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await Dropdowns();
            return View(new Product { Enabled = true });
        }
        [HttpPost]
        public async Task<IActionResult> Create(Product model)
        {
            model.DateCreated= DateTime.UtcNow;
            model.Enabled = true;
            model.Price = decimal.Parse(model.PriceText);
            model.DiscountedPrice = model.DiscountedPriceText != null ? decimal.Parse(model.DiscountedPriceText) : null;
            var categories = await context.Categories.Where(p => model.CategoryIds.Any(q => q == p.Id)).ToListAsync();

            if (model.Images is not null)
            {
                foreach (var file in model.Images)
                {
                    var image = await Image.LoadAsync(file.OpenReadStream());
                    image.Mutate(p => {
                        p.Resize(
                            configuration.GetValue<int>("DefaultImageSize:Width"),
                            configuration.GetValue<int>("DefaultImageSize:Height"));
                    
                    });

                    using var ms = new MemoryStream();
                    await image.SaveAsJpegAsync(ms);
                     
                    model.ProductImages.Add(new ProductImage
                    {
                        DateCreated = DateTime.UtcNow,
                        Enabled = true,
                        Image = ms.ToArray()
                }); 
                }
            } 
            context.Products.Add(model);
            try
            {
                await context.SaveChangesAsync();
                TempData["success"] = "Ürün ekleme işlemi başarıyla tamamlanmıştır";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                await Dropdowns();

                TempData["error"] = "Aynı isimde başka bir ürün olduğundan ekleme işlemi yapılamaz ";
                return View(model);
            }
                 
        }
        [HttpGet]
        public  async Task<IActionResult> Edit(Guid id)
        {
            await Dropdowns();

            var model =  await context.Products.FindAsync(id);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Product model)
        {
          
            context.Products.Update(model);
            try
            {
                await context.SaveChangesAsync();
                TempData["success"] = "Ürün güncelleme işlemi başarıyla tamamlanmıştır";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex) 
            {
                await Dropdowns();

                TempData["error"] = "Aynı isimde başka bir ürün olduğundan güncelleme işlemi yapılamaz ";
                return View(model);
            }

        }
        [HttpGet]
        public async Task<IActionResult> Remove(Guid id)
        {
            var model = await context.Products.FindAsync(id);
            context.Products.Remove(model);
            try
            {
                await context.SaveChangesAsync();
                TempData["success"] = "Ürün silme işlemi başarıyla tamamlanmıştır";

                return RedirectToAction(nameof(Index));

            }
            catch (Exception)
            {

                TempData["error"] = $"{model.Name} isimli ürün bir ya da daha fazla  kayıt ile ilişkili olduğu için  silme işlemi gerçekleştirilemiyor.";

            }          
                return RedirectToAction(nameof(Index));          
        }
        private async Task Dropdowns()
        {
            ViewBag.Categories = new SelectList(await context.Categories.Select(p=> new {p.Id, p.Name, RayonName = p.Rayon!.Name }).OrderBy(p=>p.Name).ToListAsync(), "Id", "Name", null,"RayonName");

        }
    }

}
