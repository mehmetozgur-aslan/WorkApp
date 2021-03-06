﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using YSKProje.ToDo.Business.Interfaces;
using YSKProje.ToDo.DTO.DTOs.TaskDtos;
using YSKProje.ToDo.Entities.Concrete;
using YSKProje.ToDo.Web.BaseControllers;
using YSKProje.ToDo.Web.StringInfo;

namespace YSKProje.ToDo.Web.Areas.Member.Controllers
{
    [Authorize(Roles = RoleInfo.Member)]
    [Area(AreaInfo.Member)]
    public class TaskController : BaseIdentityController
    {
        private readonly ITaskService _taskService;
        //private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public TaskController(ITaskService taskService, UserManager<AppUser> userManager, IMapper mapper):base(userManager)
        {
            _taskService = taskService;
            //_userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(int activePage = 1)
        {
            var user = await GetSingInUser();
            int totalPage;
            var tasks = _taskService.GetAllTaskDatasNotCompleted(out totalPage, user.Id, activePage);

            ViewBag.TotalPage = totalPage;
            ViewBag.ActivePage = activePage;

            var ListAllTaskDto = _mapper.Map<List<TaskListAllDto>>(tasks);

            return View(ListAllTaskDto);
        }
    }
}
