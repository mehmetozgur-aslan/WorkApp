using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YSKProje.ToDo.DataAccess.Concrete.EntityFrameworkCore.Contexts;
using YSKProje.ToDo.DataAccess.Interfaces;
using YSKProje.ToDo.Entities.Interfaces;

namespace YSKProje.ToDo.DataAccess.Concrete.EntityFrameworkCore.Repositories
{
    public class EfGenericRepository<T> : IGenericDal<T>
        where T : class, ITable, new()
    {
        public List<T> GetAll()
        {

            using var context = new TodoContext();
            return context.Set<T>().ToList();
        }

        public T GetById(int id)
        {
            using var context = new TodoContext();
            return context.Set<T>().Find(id);
        }

        public void Update(T tablo)
        {
            using var context = new TodoContext();
            context.Set<T>().Update(tablo);
            context.SaveChanges();
        }

        public void Save(T tablo)
        {
            using var context = new TodoContext();
            context.Set<T>().Add(tablo);
            context.SaveChanges();
        }

        public void Delete(T tablo)
        {
            using var context = new TodoContext();
            context.Set<T>().Remove(tablo);
            context.SaveChanges();
        }
    }
}
