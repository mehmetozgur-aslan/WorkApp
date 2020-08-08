using System.Collections.Generic;
using YSKProje.ToDo.Business.Interfaces;
using YSKProje.ToDo.DataAccess.Concrete.EntityFrameworkCore.Repositories;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.ToDo.Business.Concrete
{
    class TaskManager : ITaskService
    {
        private readonly EfTaskRepository efCalismaRepository;

        public TaskManager()
        {
            efCalismaRepository = new EfTaskRepository();
        }

        public List<Task> GetAll()
        {
            return efCalismaRepository.GetAll();
        }

        public Task GetById(int id)
        {
            return efCalismaRepository.GetById(id);
        }

        public void Update(Task entity)
        {
            efCalismaRepository.Update(entity);
        }

        public void Save(Task entity)
        {
            efCalismaRepository.Save(entity);
        }

        public void Delete(Task entity)
        {
            efCalismaRepository.Delete(entity);
        }

    }
}
