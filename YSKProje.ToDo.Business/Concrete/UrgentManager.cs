using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using YSKProje.ToDo.Business.Interfaces;
using YSKProje.ToDo.DataAccess.Concrete.EntityFrameworkCore.Repositories;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.ToDo.Business.Concrete
{
    public class UrgentManager : IUrgentService
    {
        EfUrgentRepository urgentRepository;

        public UrgentManager()
        {
            urgentRepository = new EfUrgentRepository();
        }

        public void Delete(Urgent entity)
        {
            urgentRepository.Delete(entity);
        }

        public List<Urgent> GetAll()
        {
            return urgentRepository.GetAll();
        }

        public Urgent GetById(int id)
        {
            return urgentRepository.GetById(id);
        }

        public void Save(Urgent entity)
        {
            urgentRepository.Save(entity);
        }

        public void Update(Urgent entity)
        {
            urgentRepository.Update(entity);
        }
    }
}
