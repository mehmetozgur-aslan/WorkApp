using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using YSKProje.ToDo.Business.Interfaces;
using YSKProje.ToDo.Entities.Concrete;
using YSKProje.ToDo.Web.BaseControllers;

namespace YSKProje.ToDo.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class HomeController : BaseIdentityController
    {
        private readonly IReportService _reportService;
        private readonly ITaskService _taskService;
        //private readonly UserManager<AppUser> _userManager;
        private readonly INotificationService _notificationService;

        public HomeController(IReportService reportService, UserManager<AppUser> userManager, ITaskService taskService, INotificationService notificationService) : base(userManager)
        {
            _reportService = reportService;
            //_userManager = userManager;
            _taskService = taskService;
            _notificationService = notificationService;
        }

        public async Task<IActionResult> Index()
        {
            var user = await GetSingInUser();

            ViewBag.NotAssignTaskCount = _taskService.GetNotAssignTaskCount();
            ViewBag.CompletedTaskCount = _taskService.GetCompletedTaskCount();
            ViewBag.NotReadNotificationCount = _notificationService.GetNotReadNotificationCountByUserId(user.Id);
            ViewBag.ReportCount = _reportService.GetReportCount();

            return View();
        }
    }
}
