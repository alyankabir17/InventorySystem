using InventoryManagementSystem.Models;
using InventoryManagementSystem.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Controllers
{
    public class SaleController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SaleController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Sale
        public async Task<IActionResult> Index()
        {
            return View(await _context.Sales.ToListAsync());
        }

        // GET: Sale/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name");
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Name");
            return View();
        }

        // POST: Sale/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Sale sale)
        {
            // Check Stock Level First
            var product = await _context.Products.FindAsync(sale.ProductId);

            if (product != null)
            {
                if (product.Quantity < sale.Quantity)
                {
                    ModelState.AddModelError("Quantity", "Not enough stock available!");
                }
                else if (ModelState.IsValid)
                {
                    // 1. Create Sale Record
                    _context.Add(sale);

                    // 2. Decrease Stock
                    product.Quantity -= sale.Quantity;
                    _context.Update(product);

                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }

            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name", sale.ProductId);
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Name", sale.CustomerId);
            return View(sale);
        }
    }
}