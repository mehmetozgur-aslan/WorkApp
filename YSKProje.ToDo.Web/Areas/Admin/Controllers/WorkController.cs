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


namespace YSKProje.ToDo.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class WorkController : Controller
    {
        private readonly IAppUserService _appUserService;
        private readonly ITaskService _taskService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IFileService _fileService;
        private readonly INotificationService _notificationService;

        public WorkController(IAppUserService appUserService, ITaskService taskService, UserManager<AppUser> userManager, IFileService fileService, INotificationService notificationService)
        {
            _appUserService = appUserService;
            _taskService = taskService;
            _userManager = userManager;
            _fileService = fileService;
            _notificationService = notificationService;
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
            Entities.Concrete.Task task = _taskService.GetTaskWithUrgent(id);
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

        [HttpPost]
        public IActionResult AssignToPerson(AssignPersonelToTaskViewModel model)
        {
            var task = _taskService.GetById(model.TaskId);
            task.AppUserId = model.AppUserId;

            _taskService.Update(task);

            _notificationService.Save(new Notification
            {
                AppUserId = model.AppUserId,
                Description = $"{task.Name} isimli iş için görevlendirildiniz."
            });

            return RedirectToAction("Index");
        }

        public IActionResult AssignPersonelToTask(AssignPersonelToTaskViewModel model)
        {
            TempData["menu"] = "work";
            AppUser user = _userManager.Users.FirstOrDefault(x => x.Id == model.AppUserId);
            YSKProje.ToDo.Entities.Concrete.Task task = _taskService.GetTaskWithUrgent(model.TaskId);

            if (user == null || task == null)
            {
                return NotFound();
            }

            AppUserListViewModel userModel = new AppUserListViewModel();
            userModel.Id = user.Id;
            userModel.Name = user.Name;
            userModel.Picture = user.Picture;
            userModel.Surname = user.Surname;
            userModel.Email = user.Email;

            TaskListViewModel taskModel = new TaskListViewModel();
            taskModel.Id = task.Id;
            taskModel.Description = task.Description;
            taskModel.Urgent = task.Urgent;
            taskModel.Name = task.Name;

            AssignPersonelToTaskListViewModel assignPersonelToTaskListModel = new AssignPersonelToTaskListViewModel();

            assignPersonelToTaskListModel.AppUser = userModel;
            assignPersonelToTaskListModel.Task = taskModel;

            return View(assignPersonelToTaskListModel);
        }

        public IActionResult GetDetails(int id)
        {
            var task = _taskService.GetTaskWithReport(id);

            TaskListAllViewModel model = new TaskListAllViewModel()
            {
                Id = task.Id,
                Reports = task.Reports,
                Name = task.Name,
                Description = task.Description,
                AppUser = task.AppUser

            };

            return View(model);
        }

        public IActionResult GetExcel(int id)
        {
            return File(_fileService.ExportExcel(_taskService.GetTaskWithReport(id).Reports), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", Guid.NewGuid() + ".xlsx");
        }

        public IActionResult GetPDF(int id)
        {
            var path = _fileService.ExportPdf(_taskService.GetTaskWithReport(id).Reports);
            return File(path, "application/pdf", Guid.NewGuid() + ".pdf");
        }
    }
}
