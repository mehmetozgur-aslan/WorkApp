using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YSKProje.ToDo.DTO.DTOs.AppUserDtos;
using YSKProje.ToDo.DTO.DTOs.NotificationDtos;
using YSKProje.ToDo.DTO.DTOs.ReportDtos;
using YSKProje.ToDo.DTO.DTOs.TaskDtos;
using YSKProje.ToDo.DTO.DTOs.UrgentDtos;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.ToDo.Web.Mapping.AutoMapperProfile
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            #region Aciliyet-AciliyetDto

            CreateMap<AddUrgentDto, Urgent>();
            CreateMap<Urgent, AddUrgentDto>();

            CreateMap<ListUrgentDto, Urgent>();
            CreateMap<Urgent, ListUrgentDto>();

            CreateMap<UpdateUrgentDto, Urgent>();
            CreateMap<Urgent, UpdateUrgentDto>();

            #endregion

            #region User-UserDto

            CreateMap<AddAppUserDto, AppUser>();
            CreateMap<AppUser, AddAppUserDto>();

            CreateMap<ListAppUserDto, AppUser>();
            CreateMap<AppUser, ListAppUserDto>();

            CreateMap<SignInAppUserDto, AppUser>();
            CreateMap<AppUser, SignInAppUserDto>();

            #endregion

            #region Bildirim-BildirimDto

            CreateMap<ListNotificationDto, Notification>();
            CreateMap<Notification, ListNotificationDto>();

            #endregion

            #region Görev-GörevDto

            CreateMap<AddTaskDto, Entities.Concrete.Task>();
            CreateMap<Entities.Concrete.Task, AddTaskDto>();

            CreateMap<ListTaskDto, Entities.Concrete.Task>();
            CreateMap<Entities.Concrete.Task, ListTaskDto>();

            CreateMap<UpdateTaskDto, Entities.Concrete.Task>();
            CreateMap<Entities.Concrete.Task, UpdateTaskDto>();

            CreateMap<Entities.Concrete.Task, TaskListAllDto>();
            CreateMap<TaskListAllDto, Entities.Concrete.Task>();

            #endregion

            #region Rapor-RaporDto

            CreateMap<AddReportDto, Report>();
            CreateMap<Report, AddReportDto>();

            CreateMap<UpdateReportDto, Report>();
            CreateMap<Report, UpdateReportDto>();

            CreateMap<Entities.Concrete.Task, AddReportDto>();
            CreateMap<Report, UpdateReportDto>();

            #endregion

          

        }
    }
}
