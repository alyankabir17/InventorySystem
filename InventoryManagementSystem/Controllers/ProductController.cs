using InventoryManagementSystem.Models;
using InventoryManagementSystem.Models.AddModels;
using InventoryManagementSystem.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext db;

        public ProductController(ApplicationDbContext _db)
        {
            db = _db;
        }

        private bool IsUser()
        {
            int? UserId = HttpContext.Session.GetInt32("UserId");
            if (UserId == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private int RoleCheck()
        {
            int? Role = HttpContext.Session.GetInt32("UserRole");
            if (Role == null)
            {
                return 0;
            }
            else
            {
                return (int) Role;
            }
        }

        // GET: ProductController
        public ActionResult Index()
        {
            if (IsUser())
            {
                var products = db.Products.ToList();
                return View(products);
            } 
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AddProduct addProduct)
        {
            try
            {
                if (addProduct.Name == "" || addProduct.Description == "" || addProduct.Quantity == null || addProduct.UnitPrice == null)
                {
                    return View(addProduct);
                }
                else
                {
                    Product product = new Product()
                    {
                        Name = addProduct.Name,
                        Description = addProduct.Description,
                        Quantity = addProduct.Quantity,
                        UnitPrice = addProduct.UnitPrice
                    };

                    db.Products.Add(product);
                    db.SaveChanges();
                    
                    return RedirectToAction(nameof(Index));                    
                }
            }
            catch
            {
                return View(addProduct);
            }
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                var productFromDb = db.Products.SingleOrDefault(x => x.Id == id);
                if (productFromDb == null)
                {
                    return NotFound();
                }
                else
                {
                    return View(productFromDb);
                }
            } 
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Product product)
        {
            try
            {
                var productFromDb = db.Products.SingleOrDefault(x => x.Id == id);
                if (productFromDb == null)
                {
                    return NotFound();
                }
                else
                {
                    productFromDb.Name = product.Name;
                    productFromDb.Description = product.Description;
                    productFromDb.Quantity = product.Quantity;
                    productFromDb.UnitPrice = product.UnitPrice;

                    db.SaveChanges();

                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                return View(product);
            }
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Product product)
        {
            try
            {
                var productFromDb = db.Products.SingleOrDefault(x => x.Id == product.Id);
                if (productFromDb == null)
                {
                    return NotFound();
                }
                else
                {
                    db.Remove(productFromDb);
                    db.SaveChanges();

                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
