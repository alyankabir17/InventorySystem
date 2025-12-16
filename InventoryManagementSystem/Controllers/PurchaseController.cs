using InventoryManagementSystem.Models;
using InventoryManagementSystem.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Controllers
{
    public class PurchaseController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PurchaseController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Purchase
        public async Task<IActionResult> Index()
        {
            var purchases = await _context.Purchases.ToListAsync();
            return View(purchases);
        }

        // GET: Purchase/Create
        public IActionResult Create()
        {
            // This loads the lists for the Dropdowns
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name");
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "Name");
            return View();
        }

        // POST: Purchase/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Purchase purchase)
        {
            if (ModelState.IsValid)
            {
                // 1. Save the Purchase Record
                _context.Add(purchase);

                // 2. OPTIONAL: Update Product Stock (Increase Quantity)
                var product = await _context.Products.FindAsync(purchase.ProductId);
                if (product != null)
                {
                    product.Quantity += purchase.Quantity;
                    _context.Update(product);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Reload the lists if something fails
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name", purchase.ProductId);
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "Name", purchase.SupplierId);
            return View(purchase);
        }
    }
}