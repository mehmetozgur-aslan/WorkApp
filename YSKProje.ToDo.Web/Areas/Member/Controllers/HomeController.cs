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
using YSKProje.ToDo.Web.StringInfo;

namespace YSKProje.ToDo.Web.Areas.Member.Controllers
{
    [Authorize(Roles = RoleInfo.Member)]
    [Area(AreaInfo.Member)]
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

            ViewBag.ReportCount = _reportService.GetReportCountByUserId(user.Id);
            ViewBag.TaskCount = _taskService.GetCompletedTaskCountByUserId(user.Id);
            ViewBag.NotCompletedTaskCount = _taskService.GetNotCompletedTaskCountByUserId(user.Id);
            ViewBag.NotReadNotification = _notificationService.GetNotReadNotificationCountByUserId(user.Id);

            return View();
        }
    }
}
