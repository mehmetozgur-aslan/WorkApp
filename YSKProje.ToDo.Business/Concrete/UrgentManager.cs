using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using YSKProje.ToDo.Business.Interfaces;
using YSKProje.ToDo.DataAccess.Concrete.EntityFrameworkCore.Repositories;
using YSKProje.ToDo.DataAccess.Interfaces;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.ToDo.Business.Concrete
{
    public class UrgentManager : IUrgentService
    {
        private readonly IUrgentDal _urgentDal;

        public UrgentManager(IUrgentDal urgentDal)
        {
            _urgentDal = urgentDal;
        }

        public void Delete(Urgent entity)
        {
            _urgentDal.Delete(entity);
        }

        public List<Urgent> GetAll()
        {
            return _urgentDal.GetAll();
        }

        public Urgent GetById(int id)
        {
            return _urgentDal.GetById(id);
        }

        public void Save(Urgent entity)
        {
            _urgentDal.Save(entity);
        }

        public void Update(Urgent entity)
        {
            _urgentDal.Update(entity);
        }
    }
}
