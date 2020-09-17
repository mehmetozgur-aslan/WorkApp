using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YSKProje.ToDo.DataAccess.Concrete.EntityFrameworkCore.Contexts;
using YSKProje.ToDo.DataAccess.Interfaces;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.ToDo.DataAccess.Concrete.EntityFrameworkCore.Repositories
{
    public class EfUserRepository : IUserDal
    {
        public List<AppUser> GetUserWithoutAdmin()
        {
            using var context = new TodoContext();

            return context.Users.Join(context.UserRoles, user => user.Id, userRole => userRole.UserId, (resultUser, resultUserRole) => new
            {
                user = resultUser,
                userRole = resultUserRole
            }).Join(context.Roles, twoTableResult => twoTableResult.userRole.RoleId, role => role.Id,
               (resultTable, resultRole) => new
               {
                   user = resultTable.user,
                   userRoles = resultTable.userRole,
                   roles = resultRole
               }).Where(I => I.roles.Name != "Member").Select(I => new AppUser()
               {
                   Id = I.user.Id,
                   Name = I.user.Name,
                   Surname = I.user.Surname,
                   Picture = I.user.Picture,
                   Email = I.user.Email,
                   UserName = I.user.UserName
               }).ToList();


        }

        public List<AppUser> GetUserWithoutAdmin(out int totalPage, string name, int activePage = 1)
        {
            using var context = new TodoContext();

            var result = context.Users.Join(context.UserRoles, user => user.Id, userRole => userRole.UserId, (resultUser, resultUserRole) => new
            {
                user = resultUser,
                userRole = resultUserRole
            }).Join(context.Roles, twoTableResult => twoTableResult.userRole.RoleId, role => role.Id,
               (resultTable, resultRole) => new
               {
                   user = resultTable.user,
                   userRoles = resultTable.userRole,
                   roles = resultRole
               }).Where(I => I.roles.Name == "Member").Select(I => new AppUser()
               {
                   Id = I.user.Id,
                   Name = I.user.Name,
                   Surname = I.user.Surname,
                   Picture = I.user.Picture,
                   Email = I.user.Email,
                   UserName = I.user.UserName
               });

            totalPage = (int)Math.Ceiling((double)result.Count() / 3);

            if (!string.IsNullOrWhiteSpace(name))
            {
                result = result.Where(x => x.Name.ToLower().Contains(name.ToLower()) || x.Surname.ToLower().Contains(name.ToLower()));

                totalPage = (int)Math.Ceiling((double)result.Count() / 3);
            }

            result = result.Skip((activePage - 1) * 3).Take(3);

            return result.ToList();
        }

        public List<DualHelper> GetUsersWithMostCompletedTask()
        {
            using var context = new TodoContext();

            var result = context.Tasks.Include(I => I.AppUser).Where(I => I.State).GroupBy(I => I.AppUser.UserName).OrderByDescending(I => I.Count()).Take(5).Select(I => new DualHelper
            {
                Username = I.Key,
                TaskCount = I.Count()
            }).ToList();

            return result;
        }

        public List<DualHelper> GetUsersWithMostWorkingTask()
        {
            using var context = new TodoContext();

            var result = context.Tasks.Include(I => I.AppUser).Where(I => !I.State && I.AppUser != null).GroupBy(I => I.AppUser.UserName).OrderByDescending(I => I.Count()).Take(5).Select(I => new DualHelper
            {
                Username = I.Key,
                TaskCount = I.Count()
            }).ToList();

            return result;
        }
    }
}
