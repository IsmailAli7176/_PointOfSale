using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PointOfSale.Models;

namespace PointOfSale.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly CategoriesDbContext _Db;

        public CategoriesController(CategoriesDbContext db)
        {
            _Db = db;
        }

        // READ: List all categories
        [Authorize]

        public async Task<IActionResult> Index()
        {
            var categories = await _Db.Categories.ToListAsync();
            return View(categories);
        }

        // READ: Details of one category
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var category = await _Db.Categories.FirstOrDefaultAsync(c => c.Cat_Id == id);
            if (category == null) return NotFound();

            return View(category);
        }

        // CREATE: GET
        public IActionResult Create()
        {
            return View();
        }

        // CREATE: POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Categories category)
        {
            if (!ModelState.IsValid)
                return View(category);

            bool exists = await _Db.Categories.AnyAsync(c => c.Cat_Name == category.Cat_Name);
            if (exists)
            {
                ModelState.AddModelError("Cat_Name", "Category name must be unique.");
                return View(category);
            }

            _Db.Add(category);
            await _Db.SaveChangesAsync();
            TempData["SuccessMessage"] = "Category added successfully!";
            return RedirectToAction(nameof(Index));
        }


        // EDIT: GET
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var category = await _Db.Categories.FindAsync(id);
            if (category == null) return NotFound();

            return View(category);
        }

        // EDIT: POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Categories category)
        {
            if (id != category.Cat_Id) 
                return NotFound();

            
                try
                {
                    _Db.Update(category);
                    await _Db.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Category updated successfully!";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_Db.Categories.Any(e => e.Cat_Id == id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            
            return View(category);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var category = await _Db.Categories.FirstOrDefaultAsync(m => m.Cat_Id == id);

            if (category == null)
                return NotFound();

            return View(category); // Show confirmation page
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _Db.Categories.FindAsync(id);
            if (category != null)
            {
                _Db.Categories.Remove(category);
                await _Db.SaveChangesAsync();
                TempData["SuccessMessage"] = "Category deleted successfully!";
            }

            return RedirectToAction(nameof(Index));
        }

    }
}
