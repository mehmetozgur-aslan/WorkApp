using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YSKProje.ToDo.Business.Interfaces;
using YSKProje.ToDo.DTO.DTOs.UrgentDtos;
using YSKProje.ToDo.Entities.Concrete;
using YSKProje.ToDo.Web.StringInfo;

namespace YSKProje.ToDo.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = RoleInfo.Admin)]
    [Area(AreaInfo.Admin)]
    public class UrgentController : Controller
    {
        private readonly IUrgentService _urgentService;
        private readonly IMapper _mapper;

        public UrgentController(IUrgentService urgentService, IMapper mapper)
        {
            _urgentService = urgentService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            TempData["menu"] = "urgent";

            var urgentListViewModels = _mapper.Map<List<ListUrgentDto>>(_urgentService.GetAll());

            return View(urgentListViewModels);
        }

        public IActionResult Add()
        {
            TempData["menu"] = "urgent";
            return View(new AddUrgentDto());
        }

        [HttpPost]
        public IActionResult Add(AddUrgentDto model)
        {
            if (ModelState.IsValid)
            {
                _urgentService.Save(new Urgent
                {
                    Definition = model.Definition
                });

                return RedirectToAction("Index");

            }

            return View(model);
        }

        public IActionResult Update(int id)
        {
            TempData["menu"] = "urgent";

            var updateUrgentDto = _mapper.Map<UpdateUrgentDto>(_urgentService.GetById(id));

            return View(updateUrgentDto);
        }

        [HttpPost]
        public IActionResult Update(UpdateUrgentDto model)
        {
            if (ModelState.IsValid)
            {
                Urgent urgent = _urgentService.GetById(model.Id);

               
                urgent.Definition = model.Definition;

                _urgentService.Update(urgent);

                return RedirectToAction("Index");
            }

            return View(model);
        }
    }
}
