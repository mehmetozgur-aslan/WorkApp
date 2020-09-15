using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using YSKProje.ToDo.Business.Interfaces;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.ToDo.Web.Areas.Member.Controllers
{
    [Authorize(Roles = "Member")]
    [Area("Member")]
    public class HomeController : Controller
    {
        private readonly IReportService _reportService;
        private readonly ITaskService _taskService;
        private readonly UserManager<AppUser> _userManager;
        private readonly INotificationService _notificationService;

        public HomeController(IReportService reportService, UserManager<AppUser> userManager, ITaskService taskService, INotificationService notificationService)
        {
            _reportService = reportService;
            _userManager = userManager;
            _taskService = taskService;
            _notificationService = notificationService;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            ViewBag.ReportCount = _reportService.GetReportCountByUserId(user.Id);
            ViewBag.TaskCount = _taskService.GetCompletedTaskCountByUserId(user.Id);
            ViewBag.NotCompletedTaskCount = _taskService.GetNotCompletedTaskCountByUserId(user.Id);
            ViewBag.NotReadNotification = _notificationService.GetNotReadNotificationCountByUserId(user.Id);

            return View();
        }
    }
}
