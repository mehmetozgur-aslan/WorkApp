using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using YSKProje.ToDo.Business.Interfaces;
using YSKProje.ToDo.DTO.DTOs.ReportDtos;
using YSKProje.ToDo.DTO.DTOs.TaskDtos;
using YSKProje.ToDo.Entities.Concrete;
using YSKProje.ToDo.Web.BaseControllers;

namespace YSKProje.ToDo.Web.Areas.Member.Controllers
{
    [Area("Member")]
    [Authorize(Roles = "Member")]
    public class WorkController : BaseIdentityController
    {
        private readonly IAppUserService _appUserService;
        private readonly ITaskService _taskService;
        //private readonly UserManager<AppUser> _userManager;
        private readonly IReportService _reportService;
        private readonly INotificationService _notificationService;
        private readonly IMapper _mapper;

        public WorkController(IAppUserService appUserService, ITaskService taskService, UserManager<AppUser> userManager, IReportService reportService, INotificationService notificationService, IMapper mapper):base(userManager)
        {
            _appUserService = appUserService;
            _taskService = taskService;
            //_userManager = userManager;
            _reportService = reportService;
            _notificationService = notificationService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            TempData["menu"] = "work";

            var user = await GetSingInUser();

            var tasks = _taskService.GetAllTaskDatas(x => x.AppUserId == user.Id && !x.State);

            var taskListDto = _mapper.Map<List<TaskListAllDto>>(tasks);

            //List<TaskListAllViewModel> taskListModel = new List<TaskListAllViewModel>();

            //foreach (var task in tasks)
            //{
            //    TaskListAllViewModel model = new TaskListAllViewModel();
            //    model.Id = task.Id;
            //    model.Name = task.Name;
            //    model.CreatedDate = task.CreatedDate;
            //    model.Description = task.Description;
            //    model.AppUser = task.AppUser;
            //    model.Reports = task.Reports;
            //    model.Urgent = task.Urgent;

            //    taskListModel.Add(model);
            //}

            return View(taskListDto);
        }

        public IActionResult Add(int taskId)
        {
            var task = _taskService.GetTaskWithUrgent(taskId);

            AddReportDto addReportDto = new AddReportDto();
            addReportDto.TaskId = taskId;
            addReportDto.Task = task;

            return View(addReportDto);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddReportDto model)
        {
            if (ModelState.IsValid)
            {
                _reportService.Save(new Report()
                {
                    TaskId = model.TaskId,
                    Description = model.Definition,
                    Detail = model.Detail

                });

                var adminUserList = await _userManager.GetUsersInRoleAsync("Admin");
                var activeUser = await GetSingInUser();

                foreach (var adminUser in adminUserList)
                {
                    _notificationService.Save(new Notification()
                    {
                        Description = $"{activeUser.Name} {activeUser.Surname} yeni bir rapor yazdı.",
                        AppUserId = adminUser.Id,

                    });
                }

                return RedirectToAction("Index");
            }

            return View();
        }

        public IActionResult Update(int id)
        {
            var report = _reportService.GetReportWithTaskById(id);
            UpdateReportDto updateReportDto = new UpdateReportDto();
            updateReportDto.ReportId = report.Id;
            updateReportDto.Definition = report.Description;
            updateReportDto.Detail = report.Detail;
            updateReportDto.Task = report.Task;
            updateReportDto.TaskId = report.TaskId;            

            return View(updateReportDto);
        }

        [HttpPost]
        public IActionResult Update(UpdateReportDto model)
        {
            if (ModelState.IsValid)
            {
                Report report = _reportService.GetById(model.ReportId);

                report.Description = model.Definition;
                report.Detail = model.Detail;

                _reportService.Update(report);

                return RedirectToAction("Index");
            }

            return View(model);
        }

        public async Task<IActionResult> Done(int taskId)
        {
            var task = _taskService.GetById(taskId);
            task.State = true;

            _taskService.Update(task);

            var adminUserList = await _userManager.GetUsersInRoleAsync("Admin");
            var activeUser = await GetSingInUser();

            foreach (var adminUser in adminUserList)
            {
                _notificationService.Save(new Notification()
                {
                    Description = $"{activeUser.Name} {activeUser.Surname} görevi tamamladı.",
                    AppUserId = adminUser.Id,
                });
            }

            return Json(null);
        }
    }
}
