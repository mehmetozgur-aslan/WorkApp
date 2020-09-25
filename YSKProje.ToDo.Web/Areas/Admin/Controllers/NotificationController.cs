using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using YSKProje.ToDo.Business.Interfaces;
using YSKProje.ToDo.DTO.DTOs.NotificationDtos;
using YSKProje.ToDo.Entities.Concrete;


namespace YSKProje.ToDo.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class NotificationController : Controller
    {
        private readonly INotificationService _notificationService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public NotificationController(INotificationService notificationService, UserManager<AppUser> userManager, IMapper mapper)
        {
            _notificationService = notificationService;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var notificationList = _notificationService.GetNotReadByUserId(user.Id);

            List<ListNotificationDto> listNotificationDto = _mapper.Map<List<ListNotificationDto>>(_notificationService.GetNotReadByUserId(user.Id));           

            return View(listNotificationDto);
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
