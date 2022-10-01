﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCStoreData;
using System.Data;

namespace MVCStoreeWeb.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class CategoriesController : Controller
    {
        private readonly AppDbContext context;

        public CategoriesController(
           AppDbContext context
            )
        {
            this.context = context;
        }
        public async Task<IActionResult> Index()
        {
            var model = await context.Categories.ToListAsync();
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
             await Dropdowns();
            return View(new Category {Enabled=true });
        }
        [HttpPost]  
        public async Task<IActionResult> Create(Category model)
        {
            model.DateCreated= DateTime.UtcNow;
            model.Enabled = true;
            context.Categories.Add(model);
            try
            {
                await context.SaveChangesAsync();
                TempData["success"] = "kategori ekleme işlemi başarıyla tamamlanmıştır";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                await Dropdowns();
                TempData["error"] = "Aynı isimde başka bir kategori olduğundan ekleme işlemi yapılamaz ";
                return View(model);
            }
                 
        }
        [HttpGet]
        public  async Task<IActionResult> Edit(Guid id)
        {
            await Dropdowns();
            var model =  await context.Categories.FindAsync(id);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Category model)
        {
          
            context.Categories.Update(model);
            try
            {
                await context.SaveChangesAsync();
                TempData["success"] = "Kategori güncelleme işlemi başarıyla tamamlanmıştır";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex) 
            {
                await Dropdowns();
                TempData["error"] = "Aynı isimde başka bir kategori olduğundan güncelleme işlemi yapılamaz ";
                return View(model);
            }

        }
        [HttpGet]
        public async Task<IActionResult> Remove(Guid id)
        {
            var model = await context.Categories.FindAsync(id);
            context.Categories.Remove(model);
            try
            {
                await context.SaveChangesAsync();
                TempData["success"] = "Kategori silme işlemi başarıyla tamamlanmıştır";

                return RedirectToAction(nameof(Index));

            }
            catch (Exception)
            {

                TempData["error"] = $"{model.Name} isimli kategori bir ya da daha fazla  kayıt ile ilişkili olduğu için  silme işlemi gerçekleştirelimiyor.";

            }          
                return RedirectToAction(nameof(Index));          
        }

        private async Task Dropdowns()
        { 
            ViewBag.Rayons = new SelectList(await context.Rayons.OrderBy(p=>p.Name).ToListAsync(), "Id", "Name");
    
        }

    }

}
