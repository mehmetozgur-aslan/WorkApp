using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YSKProje.ToDo.DataAccess.Concrete.EntityFrameworkCore.Contexts;
using YSKProje.ToDo.DataAccess.Interfaces;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.ToDo.DataAccess.Concrete.EntityFrameworkCore.Repositories
{
    public class EfReportRepository : EfGenericRepository<Report>, IReportDal
    {
        public int GetReportCount()
        {
            using var context = new TodoContext();

            return context.Reports.Count();
        }

        public int GetReportCountByUserId(int userId)
        {
            using var context = new TodoContext();

            var result = context.Tasks.Include(i => i.Reports).Where(i => i.AppUserId == userId);

            return result.SelectMany(i => i.Reports).Count();
        }

        public Report GetReportWithTaskById(int id)
        {
            using var context = new TodoContext();

            return context.Reports.Include(x => x.Task).ThenInclude(x => x.Urgent).Where(x => x.Id == id).FirstOrDefault();

        }
    }
}
