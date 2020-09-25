using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using YSKProje.ToDo.Business.Interfaces;
using YSKProje.ToDo.DTO.DTOs.AppUserDtos;
using YSKProje.ToDo.DTO.DTOs.TaskDtos;
using YSKProje.ToDo.Entities.Concrete;



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
        private readonly IMapper _mapper;

        public WorkController(IAppUserService appUserService, ITaskService taskService, UserManager<AppUser> userManager, IFileService fileService, INotificationService notificationService, IMapper mapper)
        {
            _appUserService = appUserService;
            _taskService = taskService;
            _userManager = userManager;
            _fileService = fileService;
            _notificationService = notificationService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            TempData["menu"] = "work";

            List<TaskListAllDto> listTaskDto = _mapper.Map<List<TaskListAllDto>>(_taskService.GetAllTaskDatas());

            return View(listTaskDto);
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

            var userListDto = _mapper.Map<List<ListAppUserDto>>(userList);

            ViewBag.Personels = userListDto;

            var taskModel = _mapper.Map<ListTaskDto>(task);

            return View(taskModel);
        }

        [HttpPost]
        public IActionResult AssignToPerson(AssignPersonelToTaskListDto model)
        {
            var task = _taskService.GetById(model.Task.Id);
            task.AppUserId = model.AppUser.Id;

            _taskService.Update(task);

            _notificationService.Save(new Notification
            {
                AppUserId = model.AppUser.Id,
                Description = $"{task.Name} isimli iş için görevlendirildiniz."
            });

            return RedirectToAction("Index");
        }

        public IActionResult AssignPersonelToTask(AssignPersonelToTaskDto model)
        {
            TempData["menu"] = "work";
            AppUser user = _userManager.Users.FirstOrDefault(x => x.Id == model.AppUserId);
            YSKProje.ToDo.Entities.Concrete.Task task = _taskService.GetTaskWithUrgent(model.TaskId);

            var userModel = _mapper.Map<ListAppUserDto>(user);

            var taskModel = _mapper.Map<ListTaskDto>(task);

            AssignPersonelToTaskListDto assignPersonelToTaskListModel = new AssignPersonelToTaskListDto();

            assignPersonelToTaskListModel.AppUser = userModel;
            assignPersonelToTaskListModel.Task = taskModel;

            return View(assignPersonelToTaskListModel);
        }

        public IActionResult GetDetails(int id)
        {
            TaskListAllDto taskList = _mapper.Map<TaskListAllDto>(_taskService.GetTaskWithReport(id));

            return View(taskList);
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
