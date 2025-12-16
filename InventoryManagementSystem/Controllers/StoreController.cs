using InventoryManagementSystem.Models;
using InventoryManagementSystem.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Controllers
{
    public class StoreController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StoreController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _context.Products.ToListAsync();
            return View(products);
        }

        public async Task<IActionResult> Buy(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();

            var saleModel = new Sale
            {
                ProductId = product.Id,
                TotalAmount = product.UnitPrice
            };

            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Name");
            ViewBag.ProductName = product.Name;
            ViewBag.StockAvailable = product.Quantity;
            ViewBag.UnitPrice = product.UnitPrice;

            return View(saleModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Buy(Sale sale)
        {
            sale.Id = 0;

            var product = await _context.Products.FindAsync(sale.ProductId);

            if (product == null) return NotFound();

            if (sale.Quantity > product.Quantity)
            {
                ModelState.AddModelError("Quantity", $"Error: Only {product.Quantity} piece(s) left in stock!");
            }

            if (ModelState.IsValid)
            {
                // === NEW CODE HERE ===
                // 1. Save the current Unit Price into the Sale record
                sale.SoldPrice = product.UnitPrice;

                // 2. Calculate Total based on that price
                sale.TotalAmount = sale.Quantity * sale.SoldPrice;
                // =====================

                sale.SaleDate = DateTime.Now;

                product.Quantity = product.Quantity - sale.Quantity;

                _context.Sales.Add(sale);
                _context.Products.Update(product);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Name", sale.CustomerId);
            ViewBag.ProductName = product.Name;
            ViewBag.StockAvailable = product.Quantity;
            ViewBag.UnitPrice = product.UnitPrice;

            return View(sale);
        }
    }
}
