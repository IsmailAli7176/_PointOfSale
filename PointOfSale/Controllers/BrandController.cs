using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PointOfSale.Models;

namespace PointOfSale.Controllers
{

    public class BrandController : Controller
    {
        private readonly CategoriesDbContext _Db;
        public BrandController(CategoriesDbContext db)
        {
            _Db = db;
        }
        [Authorize]

        public async Task<IActionResult> Index()
        {
            var brands = await _Db.Brands.ToListAsync();
            return View(brands);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Brand brand)
        {
            if (!ModelState.IsValid)
                return View(brand);
            

            bool brandExists = await _Db.Brands.AnyAsync(b => b.Brand_Name == brand.Brand_Name);
            if (brandExists)
            {
                ModelState.AddModelError("Brand_Name", "Brand name must be unique.");
                return View(brand);
            }

            _Db.Add(brand);
            await _Db.SaveChangesAsync();
            TempData["SuccessMessage"] = "Brand added successfully!";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var brand = await _Db.Brands.FindAsync(id);
            if (brand == null) return NotFound();

            return View(brand);
        }
        // EDIT: POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Brand brand)
        {
            if (id != brand.Brand_Id)
                return NotFound();


            try
            {
                _Db.Update(brand);
                await _Db.SaveChangesAsync();
                TempData["SuccessMessage"] = "Category updated successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_Db.Brands.Any(e => e.Brand_Id == id))
                    return NotFound();
                else
                    throw;
            }
            return RedirectToAction(nameof(Index));

            return View(brand);
        }
        public async Task<IActionResult> Delete (int? id)
        {
            if (id == null)
                return NotFound();
              var brand = await _Db.Brands.FirstOrDefaultAsync(m => m.Brand_Id == id);
            if (brand == null)
                return NotFound();
            return View(brand);

        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var brand = await _Db.Brands.FindAsync(id);
            if (brand != null)
            {
                _Db.Brands.Remove(brand);
                await _Db.SaveChangesAsync();
                TempData["SuccessMessage"] = "Category deleted successfully!";
            }

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Details (int? id)
        {
            if (id == null) return NotFound();
            var Brand = await _Db.Brands.FirstOrDefaultAsync(b => b.Brand_Id == id);
            if (Brand == null) return NotFound();
            return View(Brand);

        }



    }
}
