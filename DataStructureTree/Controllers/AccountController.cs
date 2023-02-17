using DataStructureTree.Models;
using EntityLayer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace DataStructureTree.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login(string ReturnUrl = null)
        {
            return View(new LoginModel()
            {
                ReturnUrl = ReturnUrl
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                // generate token
                // email
                ModelState.AddModelError("", "No account has been created with this username before.");
                return View(model);
            }
            else
            {
                var result = await _signInManager.PasswordSignInAsync(user, model.Password, true, false);
                if (result.Succeeded)
                {
                    Console.WriteLine("esra  : ");
                    return Redirect("/home/index");
                }
            }
            ModelState.AddModelError("", "The entered username or password is incorrect.");
            return View(model);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }


            var user = new User()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.FirstName + "-" + Guid.NewGuid().ToString().Substring(0, 4),
                Email = model.Email,
                EmailConfirmed = true,
                PhoneNumber = model.PhoneNumber
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                // generate token
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var url = Url.Action("Login", "Account", new
                {
                    userId = user.Id,
                    token = code
                });

                await _userManager.AddToRoleAsync(user, "User");

                return RedirectToAction("login");
            }
            else
            {
                if (_userManager.Users.Select(x => x.UserName).Contains(user.UserName))
                {
                    ModelState.AddModelError("", "This username has been used before.");
                }

                if (_userManager.Users.Select(x => x.Email).Contains(model.Email))
                {
                    ModelState.AddModelError("", "An account has already been created with this e-mail.");
                }

                ModelState.AddModelError("", "Your password must contain uppercase letters, lowercase letters and numbers.");
                ModelState.AddModelError("", "An unknown error occurred. Please try again.");
            }
            return View(model);
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

    }
}
