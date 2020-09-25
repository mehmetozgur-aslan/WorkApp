﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using YSKProje.ToDo.Business.Interfaces;
using YSKProje.ToDo.DTO.DTOs.AppUserDtos;
using YSKProje.ToDo.Entities.Concrete;
using YSKProje.ToDo.Web.BaseControllers;

namespace YSKProje.ToDo.Web.Controllers
{
    public class HomeController : BaseIdentityController
    {
        //private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public HomeController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager):base(userManager)
        {
            //_userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(SignInAppUserDto model)
        {
            if (ModelState.IsValid)
            {
                AppUser appUser = await _userManager.FindByNameAsync(model.UserName);

                if (appUser != null)
                {
                    var signInResult = await _signInManager.PasswordSignInAsync(appUser, model.Password, model.RememberMe, false);

                    if (signInResult.Succeeded)
                    {
                        var roles = await _userManager.GetRolesAsync(appUser);

                        if (roles.Contains("Admin"))
                        {
                            return RedirectToAction("Index", "Home", new { area = "Admin" });
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home", new { area = "Member" });
                        }

                    }
                }

                ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalı");
            }

            return View("Index", model);
        }

        public ActionResult Register()
        {
            return View(new AddAppUserDto());
        }

        [HttpPost]
        public async Task<ActionResult> Register(AddAppUserDto model)
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
                        return RedirectToAction("Index");
                    }
                    foreach (var err in addUserResult.Errors)
                    {
                        ModelState.AddModelError("", err.Description);
                    }
                }

                AddError(addUserResult.Errors);
            }

            return View(model);
        }

        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }
    }
}
