using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using YSKProje.ToDo.Business.Interfaces;

namespace YSKProje.ToDo.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class ChartController : Controller
    {
        private readonly IAppUserService _appUserService;

        public ChartController(IAppUserService appUserService)
        {
            _appUserService = appUserService;
        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult GetUsersWithMostCompletedTask()
        {
            string result = JsonConvert.SerializeObject(_appUserService.GetUsersWithMostCompletedTask());

            return Json(result);
        }

        public IActionResult GetUsersWithMostWorkingTask()
        {
            string result = JsonConvert.SerializeObject(_appUserService.GetUsersWithMostWorkingTask());

            return Json(result);
        }

    }
}
