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
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class NotificationController : Controller
    {
        private readonly INotificationService _notificationService;
        private readonly UserManager<AppUser> _userManager;

        public NotificationController(INotificationService notificationService, UserManager<AppUser> userManager)
        {
            _notificationService = notificationService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var notificationList = _notificationService.GetNotReadByUserId(user.Id);

            List<NotificationListViewModel> notificationListViewModels = new List<NotificationListViewModel>();

            foreach (var notification in notificationList)
            {
                NotificationListViewModel notificationListViewModel = new NotificationListViewModel();

                notificationListViewModel.Id = notification.Id;
                notificationListViewModel.Description = notification.Description;


                notificationListViewModels.Add(notificationListViewModel);
            }

            return View(notificationListViewModels);
        }

        [HttpPost]
        public IActionResult Index(int id)
        {
            var notification = _notificationService.GetById(id);
            notification.State = true;
            _notificationService.Update(notification);


            return RedirectToAction("Index");
        }
    }
}
