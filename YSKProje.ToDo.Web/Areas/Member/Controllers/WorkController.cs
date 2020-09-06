using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using YSKProje.ToDo.Business.Interfaces;
using YSKProje.ToDo.Entities.Concrete;
using YSKProje.ToDo.Web.Areas.Admin.Models;
using YSKProje.ToDo.Web.Areas.Member.Models;

namespace YSKProje.ToDo.Web.Areas.Member.Controllers
{
    [Area("Member")]
    [Authorize(Roles = "Member")]
    public class WorkController : Controller
    {
        private readonly IAppUserService _appUserService;
        private readonly ITaskService _taskService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IReportService _reportService;

        public WorkController(IAppUserService appUserService, ITaskService taskService, UserManager<AppUser> userManager, IReportService reportService)
        {
            _appUserService = appUserService;
            _taskService = taskService;
            _userManager = userManager;
            _reportService = reportService;
        }

        public async Task<IActionResult> Index()
        {
            TempData["menu"] = "work";

            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var tasks = _taskService.GetAllTaskDatas(x => x.AppUserId == user.Id && !x.State);

            List<TaskListAllViewModel> taskListModel = new List<TaskListAllViewModel>();

            foreach (var task in tasks)
            {
                TaskListAllViewModel model = new TaskListAllViewModel();
                model.Id = task.Id;
                model.Name = task.Name;
                model.CreatedDate = task.CreatedDate;
                model.Description = task.Description;
                model.AppUser = task.AppUser;
                model.Reports = task.Reports;
                model.Urgent = task.Urgent;

                taskListModel.Add(model);
            }

            return View(taskListModel);
        }

        public IActionResult Add(int taskId)
        {
            var task = _taskService.GetTaskWithUrgent(taskId);

            ReportAddViewModel reportAddViewModel = new ReportAddViewModel();
            reportAddViewModel.TaskId = taskId;
            reportAddViewModel.Task = task;

            return View(reportAddViewModel);
        }

        [HttpPost]
        public IActionResult Add(ReportAddViewModel model)
        {
            if (ModelState.IsValid)
            {
                _reportService.Save(new Report()
                {
                    TaskId = model.TaskId,
                    Description = model.Definition,
                    Detail = model.Detail

                });

                return RedirectToAction("Index");
            }


            return View();
        }
    }
}
