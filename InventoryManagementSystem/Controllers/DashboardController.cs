using InventoryManagementSystem.Models;
using InventoryManagementSystem.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace InventoryManagementSystem.Controllers
{
    public class DashboardController : Controller
    {
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

        private readonly ApplicationDbContext db;

        public DashboardController(ApplicationDbContext _db)
        {
            db = _db;
        }

        // GET: DashboardController
        public ActionResult Index()
        {
            if (IsUser())
            {
                int Role = RoleCheck();

                if (Role == 1 || Role == 2)
                {
                    ViewBag.UsersCount = db.Users.Count();
                    ViewBag.ProductsCount = db.Products.Count();

                    return View();
                } 
                else if (Role == 3 )
                {
                    return RedirectToAction("Index", "Store");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
