using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using PointOfSale.Migrations;
using PointOfSale.Models;
using Register_Login.Entity;
using Register_Login.Models;
using System.Security.Claims;

namespace Register_Login.Controllers
{
    public class AccountController : Controller
    {
        private readonly CategoriesDbContext _context;
        public AccountController(CategoriesDbContext appDbContext)
        {
            _context = appDbContext;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View(_context.UserAccounts.ToList());
        }

        public IActionResult Registration()
        {
            return View(new UserAccount()); // Return UserAccount model to match view's expectation
        }

        [HttpPost]
        public IActionResult Registration(UserAccount model)
        {
            if (ModelState.IsValid)
            {
                UserAccount account = new UserAccount
                {
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Password = model.Password,
                    UserName = model.UserName
                };
                try
                {
                    _context.UserAccounts.Add(account);
                    _context.SaveChanges();
                    ModelState.Clear();
                    ViewBag.Message = $"{account.FirstName} {account.LastName} Registered Successfully. Please login.";

                    return View(new UserAccount()); // Return UserAccount to match view's expectation
                }
                catch (DbUpdateException ex)
                {
                    // Log the exception details
                    ModelState.AddModelError("", "Unable to save changes. Please try again or contact administrator.");
                    TempData["SuccessMessage"] = "Account Register successfully!";
                    return View(model);
                }
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _context.UserAccounts
               .FirstOrDefault(x => (x.UserName == model.UserNameOrEmail ||
                x.Email == model.UserNameOrEmail) &&
                x.Password == model.Password);

                  if (user != null)
                {
                    // Success - create authentication cookie
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Email),
                        new Claim("Name", user.FirstName),
                        new Claim(ClaimTypes.Role, "User"),
                    };
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Username/Email or Password is not correct");
                }
            }
            return View(model);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }


        [Authorize]
        public IActionResult SecurePage()
        {
            ViewBag.Name = HttpContext.User.Identity.Name;
            return View();
        }
    }
}