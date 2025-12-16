using InventoryManagementSystem.Models;
using InventoryManagementSystem.Models.AddModels;
using InventoryManagementSystem.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext db;

        public UserController(ApplicationDbContext _db)
        {
            db = _db;
        }

        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User user)
        {
            // 1. Check if fields are empty
            if (string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Password))
            {
                ModelState.AddModelError("", "Please enter both Username and Password.");
                return View(user);
            }

            // 2. Check Database
            var userFromDb = db.Users.SingleOrDefault(x => x.Username == user.Username && x.Password == user.Password);

            if (userFromDb == null)
            {
                // ADD ERROR MESSAGE HERE so the user knows why it failed
                ModelState.AddModelError("", "Invalid Username or Password.");
                return View(user);
            }
            else
            {
                // 3. Set Session
                HttpContext.Session.SetString("Username", userFromDb.Username);
                HttpContext.Session.SetInt32("UserId", userFromDb.Id);
                HttpContext.Session.SetInt32("UserRole", userFromDb.Role);

                // 4. Redirect based on Role
                // MAKE SURE 'Dashboard' and 'Store' controllers actually exist!
                if (userFromDb.Role == 1 || userFromDb.Role == 2)
                {
                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    return RedirectToAction("Index", "Store");
                }
            }
        }

        // GET: Signup
        public ActionResult Signup()
        {
            return View();
        }

        // POST: Signup
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Signup(AddUser addUser)
        {
            try
            {
                // 1. Check for empty fields
                if (string.IsNullOrEmpty(addUser.FirstName) || string.IsNullOrEmpty(addUser.Username) || string.IsNullOrEmpty(addUser.Password))
                {
                    ModelState.AddModelError("", "Please fill in all required fields.");
                    return View(addUser);
                }

                // 2. Check if Username already exists (Prevent Duplicates)
                bool usernameExists = db.Users.Any(u => u.Username == addUser.Username);
                if (usernameExists)
                {
                    ModelState.AddModelError("", "This username is already taken.");
                    return View(addUser);
                }

                // 3. Create User
                User user = new User()
                {
                    FirstName = addUser.FirstName,
                    LastName = addUser.LastName,
                    Email = addUser.Email,
                    Username = addUser.Username,
                    Password = addUser.Password,
                    Role = 3 // Default User Role
                };

                db.Users.Add(user);
                db.SaveChanges();

                // 4. Redirect to Login page after signup
                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred: " + ex.Message);
                return View(addUser);
            }
        }

        // ... Keep your other methods (Edit, Delete, etc.) ...

        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "User");
        }
    }
}