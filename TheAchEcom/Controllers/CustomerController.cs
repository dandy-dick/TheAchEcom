using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TheAchEcom.Models;
using Repository;
using Repository.DomainModels;
using Repository.BusinessModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Security.Claims;

namespace TheAchEcom.Controllers
{
    public class CustomerController : ApplicationController
    {
        private EcomRepository Repository = new EcomRepository();
        private UserManager<Customer> UserManager { get; set; }
        private SignInManager<Customer> SignInManager { get; set; }

        public CustomerController(
            UserManager<Customer> userManager, SignInManager<Customer> signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(AccountModel model)
        {
            if (ModelState.IsValid == false)
                return View();

            var user = new Customer() { UserName = model.UserName, Email = model.Email };
            var result = await UserManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await SignInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);
                return RedirectToAction("CloneCartFromCookie", "ShoppingCart");
            }

            // error trying to create new user
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View();
        }

        public async Task<IActionResult> LoginDefault()
        {
            var model = new LoginModel { UserName = "Huu", Password = "Huu@23419" };
            var result = await SignInManager
                .PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);
            if (result.Succeeded)
            {
                HttpContext.Response.Cookies.Delete(_cartCookieName);
                return RedirectToAction("ShopList", "Shop");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid == false)
                return View();

            var result = await SignInManager
                .PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);
            if (result.Succeeded)
            {
                HttpContext.Response.Cookies.Delete(_cartCookieName);
                return RedirectToAction("ShopList", "Shop");
            }

            return View();
        }

        public async Task<IActionResult> LogOut()
        {
            await SignInManager.SignOutAsync();
            HttpContext.Response.Cookies.Delete(_cartCookieName);
            return RedirectToAction("ShopList", "Shop");
        }
    }
}
