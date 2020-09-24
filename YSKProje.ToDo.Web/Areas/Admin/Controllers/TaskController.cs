using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using YSKProje.ToDo.Business.Interfaces;
using YSKProje.ToDo.DTO.DTOs.TaskDtos;
using YSKProje.ToDo.Entities.Concrete;
using YSKProje.ToDo.Web.Areas.Admin.Models;
using Task = YSKProje.ToDo.Entities.Concrete.Task;

namespace YSKProje.ToDo.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class TaskController : Controller
    {
        private readonly ITaskService _taskService;
        private readonly IUrgentService _urgentService;
        private readonly IMapper _mapper;
        public TaskController(ITaskService taskService, IUrgentService urgentService, IMapper mapper)
        {
            _urgentService = urgentService;
            _taskService = taskService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            TempData["menu"] = "task";

            List<ListTaskDto> listTaskDto = _mapper.Map<List<ListTaskDto>>(_taskService.GetNotCompletedTaskListWithUrgent());

            return View(listTaskDto);
        }

        public IActionResult Add()
        {
            TempData["menu"] = "task";

            ViewBag.UrgentList = new SelectList(_urgentService.GetAll(), "Id", "Definition");

            return View(new AddTaskDto());
        }

        [HttpPost]
        public IActionResult Add(AddTaskDto model)
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

            UpdateTaskDto updateTaskDto = _mapper.Map<UpdateTaskDto>(_taskService.GetById(id));

            ViewBag.UrgentList = new SelectList(_urgentService.GetAll(), "Id", "Definition", updateTaskDto.UrgentId);

            return View(updateTaskDto);
        }

        [HttpPost]
        public IActionResult Update(UpdateTaskDto model)
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

            ViewBag.UrgentList = new SelectList(_urgentService.GetAll(), "Id", "Definition", model.UrgentId);
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
