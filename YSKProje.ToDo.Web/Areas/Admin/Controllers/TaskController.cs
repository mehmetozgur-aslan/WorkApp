using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using YSKProje.ToDo.Business.Interfaces;
using YSKProje.ToDo.Entities.Concrete;
using YSKProje.ToDo.Web.Areas.Admin.Models;
using Task = YSKProje.ToDo.Entities.Concrete.Task;

namespace YSKProje.ToDo.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TaskController : Controller
    {
        private readonly ITaskService _taskService;
        private readonly IUrgentService _urgentService;
        public TaskController(ITaskService taskService, IUrgentService urgentService)
        {
            _urgentService = urgentService;
            _taskService = taskService;
        }

        public IActionResult Index()
        {
            TempData["menu"] = "task";
            List<Task> tasks = _taskService.GetNotCompletedTaskListWithUrgent();

            List<TaskListViewModel> taskListViewModels = new List<TaskListViewModel>();

            foreach (var task in tasks)
            {
                TaskListViewModel taskListViewModel = new TaskListViewModel();

                taskListViewModel.Id = task.Id;
                taskListViewModel.Name = task.Name;
                taskListViewModel.State = task.State;
                taskListViewModel.CreatedDate = task.CreatedDate;
                taskListViewModel.Description = task.Description;
                taskListViewModel.Urgent = task.Urgent;
                taskListViewModel.UrgentId = task.UrgentId;
                taskListViewModels.Add(taskListViewModel);
            }

            return View(taskListViewModels);
        }

        public IActionResult Add()
        {
            TempData["menu"] = "task";

            ViewBag.UrgentList = new SelectList(_urgentService.GetAll(), "Id", "Definition");

            return View(new TaskAddViewModel());
        }

        [HttpPost]
        public IActionResult Add(TaskAddViewModel model)
        {
            if (ModelState.IsValid)
            {
                _taskService.Save(new Entities.Concrete.Task
                {
                    Name = model.Name,
                    Description = model.Description,
                    State = false,
                    UrgentId = model.UrgentId
                });

                return RedirectToAction("Index");
            }

            return View(model);
        }

        public IActionResult Update(int id)
        {
            TempData["menu"] = "task";
            Task task = _taskService.GetById(id);

            if (task == null)
            {
                return NotFound();
            }

            TaskUpdateViewModel taskUpdateViewModel = new TaskUpdateViewModel
            {
                Id = task.Id,
                Name = task.Name,
                Description = task.Description,
                UrgentId = task.UrgentId
            };

            ViewBag.UrgentList = new SelectList(_urgentService.GetAll(), "Id", "Definition", task.UrgentId);

            return View(taskUpdateViewModel);
        }

        [HttpPost]
        public IActionResult Update(TaskUpdateViewModel model)
        {
            if (ModelState.IsValid)
            {
                _taskService.Update(new Entities.Concrete.Task
                {
                    Id = model.Id,
                    Name = model.Name,
                    Description = model.Description,
                    State = false,
                    UrgentId = model.UrgentId
                });

                return RedirectToAction("Index");
            }

            ViewBag.UrgentList = new SelectList(_urgentService.GetAll(), "Id", "Definition");
            return View(model);
        }

        public IActionResult Delete(int id)
        {
            Task task = _taskService.GetById(id);

            if (task == null)
            {
                return NotFound();
            }

            _taskService.Delete(task);

            return Json(null);
        }
    }
}
