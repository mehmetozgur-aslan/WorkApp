using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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
    }
}
