using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using YSKProje.ToDo.DTO.DTOs.AppUserDtos;
using YSKProje.ToDo.Entities.Concrete;
using YSKProje.ToDo.Web.BaseControllers;

namespace YSKProje.ToDo.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProfileController : BaseIdentityController
    {
        //private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public ProfileController(UserManager<AppUser> userManager, IMapper mapper) : base(userManager)
        {
            //_userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            TempData["menu"] = "profile";

            var user = await GetSingInUser();

            var model = _mapper.Map<ListAppUserDto>(user);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(ListAppUserDto model, IFormFile picture)
        {
            if (ModelState.IsValid)
            {
                var user = _userManager.Users.FirstOrDefault(i => i.Id == model.Id);

                if (picture != null)
                {
                    var extension = Path.GetExtension(picture.FileName);
                    var pictureName = Guid.NewGuid().ToString() + extension;
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/" + pictureName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await picture.CopyToAsync(stream);
                    }

                    user.Picture = pictureName;
                }

                user.Name = model.Name;
                user.Surname = model.Surname;
                user.Email = model.Email;

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    TempData["message"] = "Güncelleme işlemi başarılı.";
                    return RedirectToAction("Index");
                }
                else
                {
                    AddError(result.Errors);
                }
            }

            return View(model);
        }
    }
}
