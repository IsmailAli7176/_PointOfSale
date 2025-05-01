using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PointOfSale.Models;

namespace PointOfSale.Controllers
{
    public class ProductsController : Controller
    {
        private readonly CategoriesDbContext _context;

        public ProductsController(CategoriesDbContext context)
        {
            _context = context;
        }

        // GET: Products
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var products = await _context.Products
                .Include(p => p.Categories)
                .Include(b => b.Brands) // ✅ Include brand details
                .ToListAsync();

            return View(products);
        }


        // GET: Products/Create
        public IActionResult Create()
        {
            ViewBag.Cat_Id = new SelectList(_context.Categories, "Cat_Id", "Cat_Name");
            ViewBag.Brand_Id = new SelectList(_context.Brands, "Brand_Id", "Brand_Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Products product)
        {
            if (product.Product_Seling_Price < product.Product_Price)
            {
                ModelState.AddModelError(nameof(product.Product_Seling_Price), "Selling price cannot be lower than product price.");
            }

            if (!ModelState.IsValid)
            {
                // Rebuild dropdowns for redisplay
                ViewBag.Cat_Id = new SelectList(_context.Categories, "Cat_Id", "Cat_Name", product.Cat_Id);
                ViewBag.Brand_Id = new SelectList(_context.Brands, "Brand_Id", "Brand_Name", product.Brand_Id);
                return View(product);
            }

            _context.Add(product);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Product created successfully!";
            return RedirectToAction(nameof(Index));
        }
        // GET: Products/Edit/
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var product = await _context.Products
                .Include(p => p.Categories)
                .Include(p => p.Brands)
                .FirstOrDefaultAsync(p => p.Product_Id == id);

            if (product == null) return NotFound();

            ViewBag.Cat_Id = new SelectList(_context.Categories, "Cat_Id", "Cat_Name", product.Cat_Id);
            ViewBag.Brand_Id = new SelectList(_context.Brands, "Brand_Id", "Brand_Name", product.Brand_Id);

            return View(product);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Product_Id,Cat_Id,Brand_Id,Product_Name,Product_Price")] Products product)
        {
            if (id != product.Product_Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Product updated successfully!";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Products.Any(e => e.Product_Id == product.Product_Id))
                        return NotFound();
                    else
                        throw;
                }
            }

            ViewBag.Cat_Id = new SelectList(_context.Categories, "Cat_Id", "Cat_Name", product.Cat_Id);
            ViewBag.Brand_Id = new SelectList(_context.Brands, "Brand_Id", "Brand_Name", product.Brand_Id);
            return View(product);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var product = await _context.Products
                .Include(p => p.Categories)
                .Include(p => p.Brands) // ✅ Added
                .FirstOrDefaultAsync(m => m.Product_Id == id);

            if (product == null) return NotFound();

            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var product = await _context.Products
                .Include(p => p.Categories)
                .Include(p => p.Brands) // ✅ Added
                .FirstOrDefaultAsync(m => m.Product_Id == id);

            if (product == null) return NotFound();

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Product deleted successfully!";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
