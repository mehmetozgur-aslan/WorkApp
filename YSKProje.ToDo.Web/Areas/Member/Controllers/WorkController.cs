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

namespace YSKProje.ToDo.Web.Areas.Member.Controllers
{
    [Area("Member")]
    [Authorize(Roles = "Member")]
    public class WorkController : Controller
    {
        private readonly IAppUserService _appUserService;
        private readonly ITaskService _taskService;
        private readonly UserManager<AppUser> _userManager;

        public WorkController(IAppUserService appUserService, ITaskService taskService, UserManager<AppUser> userManager)
        {
            _appUserService = appUserService;
            _taskService = taskService;
            _userManager = userManager;
        }

        public IActionResult Index(int id)
        {
            TempData["menu"] = "work";

            var tasks = _taskService.GetAllTaskDatas(x => x.AppUserId == id && !x.State);

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
    }
}
