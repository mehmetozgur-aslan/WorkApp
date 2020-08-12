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
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class UrgentController : Controller
    {
        private readonly IUrgentService _urgentService;

        public UrgentController(IUrgentService urgentService)
        {
            _urgentService = urgentService;
        }

        public IActionResult Index()
        {
            TempData["menu"] = "urgent";

            List<Urgent> urgents = _urgentService.GetAll();

            List<UrgentListViewModel> urgentListViewModels = new List<UrgentListViewModel>();

            foreach (var urgent in urgents)
            {
                UrgentListViewModel urgentListViewModel = new UrgentListViewModel();

                urgentListViewModel.Id = urgent.Id;
                urgentListViewModel.Definition = urgent.Definition;
                urgentListViewModels.Add(urgentListViewModel);
            }

            return View(urgentListViewModels);
        }

        public IActionResult Add()
        {
            TempData["menu"] = "urgent";
            return View(new UrgentAddViewModel());
        }

        [HttpPost]
        public IActionResult Add(UrgentAddViewModel model)
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
            Urgent urgent = _urgentService.GetById(id);

            if (urgent == null)
            {
                return NotFound();
            }

            UrgentUpdateViewModel urgentUpdateViewModel = new UrgentUpdateViewModel()
            {
                Definition = urgent.Definition,
                Id = urgent.Id
            };

            return View(urgentUpdateViewModel);
        }

        [HttpPost]
        public IActionResult Update(UrgentUpdateViewModel model)
        {
            if (ModelState.IsValid)
            {
                Urgent urgent = _urgentService.GetById(model.Id);

                if (urgent == null)
                {
                    return NotFound();
                }

                urgent.Definition = model.Definition;

                _urgentService.Update(urgent);

                return RedirectToAction("Index");
            }

            return View(model);
        }
    }
}
