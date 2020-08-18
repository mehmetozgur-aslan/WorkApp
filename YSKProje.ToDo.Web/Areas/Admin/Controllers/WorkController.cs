using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YSKProje.ToDo.Business.Interfaces;
using YSKProje.ToDo.Entities.Concrete;
using YSKProje.ToDo.Web.Areas.Admin.Models;

namespace YSKProje.ToDo.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class WorkController : Controller
    {
        private readonly IAppUserService _appUserService;
        private readonly ITaskService _taskService;

        public WorkController(IAppUserService appUserService, ITaskService taskService)
        {
            _appUserService = appUserService;
            _taskService = taskService;
        }

        public IActionResult Index()
        {
            TempData["menu"] = "work";

            List<Entities.Concrete.Task> tasks = _taskService.GetAllTaskDatas();

            List<TaskListAllViewModel> taskListAllViewModels = new List<TaskListAllViewModel>();

            foreach (var task in tasks)
            {
                TaskListAllViewModel taskListAllViewModel = new TaskListAllViewModel()
                {
                    Id = task.Id,
                    AppUser = task.AppUser,
                    CreatedDate = task.CreatedDate,
                    Description = task.Description,
                    Name = task.Name,
                    Reports = task.Reports,
                    Urgent = task.Urgent
                };

                taskListAllViewModels.Add(taskListAllViewModel);
            }


            return View(taskListAllViewModels);
        }

        public IActionResult AssignToPerson(int id, string s, int page = 1)
        {
            TempData["menu"] = "work";
            ViewBag.ActivePage = page;
            int totalPage;
            Entities.Concrete.Task task = _taskService.GetById(id);
            var userList = _appUserService.GetUserWithoutAdmin(out totalPage, s, page);

            ViewBag.TotalPage = totalPage;
            ViewBag.Searched = s;

            List<AppUserListViewModel> appUserListViewList = new List<AppUserListViewModel>();

            foreach (var user in userList)
            {
                AppUserListViewModel userListViewModel = new AppUserListViewModel();

                userListViewModel.Id = user.Id;
                userListViewModel.Name = user.Name;
                userListViewModel.Surname = user.Surname;
                userListViewModel.Email = user.Email;
                userListViewModel.Picture = user.Picture;

                appUserListViewList.Add(userListViewModel);
            }

            ViewBag.Personels = appUserListViewList;

            TaskListViewModel taskModel = new TaskListViewModel
            {
                Id = task.Id,
                CreatedDate = task.CreatedDate,
                Description = task.Description,
                Name = task.Name,
                State = task.State,
                UrgentId = task.UrgentId,
                Urgent = task.Urgent
            };

            return View(taskModel);
        }
    }
}
