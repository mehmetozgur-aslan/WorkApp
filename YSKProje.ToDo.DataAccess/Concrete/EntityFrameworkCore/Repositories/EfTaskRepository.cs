using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using YSKProje.ToDo.DataAccess.Concrete.EntityFrameworkCore.Contexts;
using YSKProje.ToDo.DataAccess.Interfaces;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.ToDo.DataAccess.Concrete.EntityFrameworkCore.Repositories
{
    public class EfTaskRepository : EfGenericRepository<Task>, ITaskDal
    {
        public List<Task> GetAllTaskDatas()
        {
            using var context = new TodoContext();
            return context.Tasks.Include(i => i.Urgent).Include(i => i.Reports).Include(i => i.AppUser).Where(i => !i.State).OrderByDescending(i => i.CreatedDate).ToList();
        }

        public List<Task> GetNotCompletedTaskListWithUrgent()
        {
            using var context = new TodoContext();
            return context.Tasks.Include(i => i.Urgent).Where(i => !i.State).OrderByDescending(i => i.CreatedDate).ToList();
        }

        public List<Task> GetTasksByAppUserId(int id)
        {
            using var context = new TodoContext();
            return context.Tasks.Where(x => x.AppUserId == id).ToList();
        }

        public Task GetTaskWithUrgent(int id)
        {
            using var context = new TodoContext();
            return context.Tasks.Include(i => i.Urgent).Where(i => i.Id == id && !i.State).FirstOrDefault();
        }

        public Task GetTaskWithReport(int id)
        {
            using var context = new TodoContext();

            return context.Tasks.Include(i => i.Reports).Include(i => i.AppUser).Where(i => i.Id == id).FirstOrDefault();
        }

        public List<Task> GetAllTaskDatas(Expression<Func<Task, bool>> filter)
        {
            using var context = new TodoContext();
            return context.Tasks.Include(i => i.Urgent).Include(i => i.Reports).Include(i => i.AppUser).Where(filter).OrderByDescending(i => i.CreatedDate).ToList();
        }

        public List<Task> GetCompletedAllTaskDatas(out int totalPage, int userId, int activePage = 1)
        {
            using var context = new TodoContext();
            var returnValue = context.Tasks.Include(i => i.Urgent).Include(i => i.Reports).Include(i => i.AppUser).Where(i => i.AppUserId == userId && i.State).OrderByDescending(i => i.CreatedDate);

            totalPage = (int)(Math.Ceiling(((decimal)(returnValue.Count() / 3))));

            return returnValue.Skip((activePage - 1) * 3).Take(3).ToList();
        }
    }
}
