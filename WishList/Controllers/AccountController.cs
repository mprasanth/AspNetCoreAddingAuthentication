using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WishList.Models;
using WishList.Models.AccountViewModels;

namespace WishList.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View("Register");
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Register(RegisterViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var applicationUser = new ApplicationUser { UserName = vm.Email, Email = vm.Email};
                var result =   _userManager.CreateAsync(applicationUser, vm.Password).Result;
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("Password", error.Description);
                    }

                    return View("Register", vm);
                }
                return RedirectToAction("Index", "Home");
            }
            else
            {
               return View("Register", vm);
            }
        }
    }
}
