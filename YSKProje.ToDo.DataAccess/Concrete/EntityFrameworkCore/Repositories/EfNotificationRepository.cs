using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YSKProje.ToDo.DataAccess.Concrete.EntityFrameworkCore.Contexts;
using YSKProje.ToDo.DataAccess.Interfaces;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.ToDo.DataAccess.Concrete.EntityFrameworkCore.Repositories
{
    public class EfNotificationRepository : EfGenericRepository<Notification>, INotificationDal
    {
        public List<Notification> GetNotReadByUserId(int appUserId)
        {
            using TodoContext todoContext = new TodoContext();

            return todoContext.Notifications.Where(x => x.AppUserId == appUserId && !x.State).ToList();
        }
    }
}
