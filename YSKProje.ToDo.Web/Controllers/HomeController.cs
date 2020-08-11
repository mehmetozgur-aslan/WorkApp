using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using YSKProje.ToDo.Business.Interfaces;
using YSKProje.ToDo.Entities.Concrete;
using YSKProje.ToDo.Web.Models;

namespace YSKProje.ToDo.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public HomeController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View(new AppUserAddViewModel());
        }

        [HttpPost]
        public async Task<ActionResult> Register(AppUserAddViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser appUser = new AppUser()
                {
                    Name = model.Name,
                    Surname = model.Surname,
                    Email = model.Email,
                    UserName = model.UserName
                };

                var addUserResult = await _userManager.CreateAsync(appUser, model.Password);

                if (addUserResult.Succeeded)
                {
                    var addRoleResult = await _userManager.AddToRoleAsync(appUser, "Member");
                    if (addUserResult.Succeeded)
                    {
                        return RedirectToAction("Login");
                    }
                    foreach (var err in addUserResult.Errors)
                    {
                        ModelState.AddModelError("", err.Description);
                    }
                }

                foreach (var err in addUserResult.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                }
            }

            return View(model);
        }
    }
}
