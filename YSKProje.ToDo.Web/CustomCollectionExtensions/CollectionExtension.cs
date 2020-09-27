using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YSKProje.ToDo.Business.Concrete;
using YSKProje.ToDo.Business.Interfaces;
using YSKProje.ToDo.Business.ValidationRules.FluentValidation;
using YSKProje.ToDo.DataAccess.Concrete.EntityFrameworkCore.Contexts;
using YSKProje.ToDo.DataAccess.Concrete.EntityFrameworkCore.Repositories;
using YSKProje.ToDo.DataAccess.Interfaces;
using YSKProje.ToDo.DTO.DTOs.AppUserDtos;
using YSKProje.ToDo.DTO.DTOs.ReportDtos;
using YSKProje.ToDo.DTO.DTOs.TaskDtos;
using YSKProje.ToDo.DTO.DTOs.UrgentDtos;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.ToDo.Web.CustomCollectionExtensions
{
    public static class CollectionExtension
    {
        public static void AddCustomIdentity(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, AppRole>(opt =>
            {
                opt.Password.RequireDigit = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequiredLength = 1;
            }).AddEntityFrameworkStores<TodoContext>();

            services.ConfigureApplicationCookie(opt =>
            {
                opt.Cookie.Name = "IsTakipCookie";
                opt.Cookie.SameSite = SameSiteMode.Strict;
                opt.Cookie.HttpOnly = true;
                opt.ExpireTimeSpan = TimeSpan.FromDays(20);
                opt.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
                opt.LoginPath = "/Home/Index";
            });
        }

        public static void AddCustomValidator(this IServiceCollection services)
        {
            //Bir DTO için validasyon işlemi varsa bu validasyonu belirtilen validator classıyla yap
            services.AddTransient<IValidator<AddUrgentDto>, UrgentAddValidator>();
            services.AddTransient<IValidator<UpdateUrgentDto>, UrgentUpdateValidator>();
            services.AddTransient<IValidator<AddAppUserDto>, AppUserAddValidator>();
            services.AddTransient<IValidator<SignInAppUserDto>, AppUserSignInValidator>();
            services.AddTransient<IValidator<AddTaskDto>, TaskAddValidator>();
            services.AddTransient<IValidator<UpdateTaskDto>, TaskUpdateValidator>();
            services.AddTransient<IValidator<AddReportDto>, ReportAddValidator>();
            services.AddTransient<IValidator<UpdateReportDto>, ReportUpdateValidator>();
        }
    }
}
