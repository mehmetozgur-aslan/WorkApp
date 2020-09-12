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
    [Authorize(Roles = "Member")]
    [Area("Member")]
    public class TaskController : Controller
    {
        private readonly ITaskService _taskService;
        private readonly UserManager<AppUser> _userManager;

        public TaskController(ITaskService taskService, UserManager<AppUser> userManager)
        {
            _taskService = taskService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(int activePage = 1)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            int totalPage;
            var tasks = _taskService.GetAllTaskDatasNotCompleted(out totalPage, user.Id, activePage);

            List<TaskListAllViewModel> taskListAllViewModels = new List<TaskListAllViewModel>();

            ViewBag.TotalPage = totalPage;
            ViewBag.ActivePage = activePage;

            foreach (var task in tasks)
            {
                TaskListAllViewModel taskListAllViewModel = new TaskListAllViewModel();
                taskListAllViewModel.Id = task.Id;
                taskListAllViewModel.Name = task.Name;
                taskListAllViewModel.Description = task.Description;
                taskListAllViewModel.CreatedDate = task.CreatedDate;
                taskListAllViewModel.AppUser = task.AppUser;
                taskListAllViewModel.Reports = task.Reports;
                taskListAllViewModel.Urgent = task.Urgent;

                taskListAllViewModels.Add(taskListAllViewModel);
            }

            return View(taskListAllViewModels);
        }
    }
}
