using System.Collections.Generic;
using YSKProje.ToDo.Business.Interfaces;
using YSKProje.ToDo.DataAccess.Concrete.EntityFrameworkCore.Repositories;
using YSKProje.ToDo.DataAccess.Interfaces;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.ToDo.Business.Concrete
{
    public class TaskManager : ITaskService
    {
        private readonly ITaskDal _taskDal;

        public TaskManager(ITaskDal taskDal)
        {
            _taskDal = taskDal;
        }

        public List<Task> GetAll()
        {
            return _taskDal.GetAll();
        }

        public Task GetById(int id)
        {
            return _taskDal.GetById(id);
        }

        public void Update(Task entity)
        {
            _taskDal.Update(entity);
        }

        public void Save(Task entity)
        {
            _taskDal.Save(entity);
        }

        public void Delete(Task entity)
        {
            _taskDal.Delete(entity);
        }

        public List<Task> GetNotCompletedTaskListWithUrgent()
        {
            return _taskDal.GetNotCompletedTaskListWithUrgent();
        }

        public List<Task> GetAllTaskDatas()
        {
            return _taskDal.GetAllTaskDatas();
        }

        public Task GetTaskWithUrgent(int id)
        {
            return _taskDal.GetTaskWithUrgent(id);
        }

        public List<Task> GetTasksByAppUserId(int id)
        {
            return _taskDal.GetTasksByAppUserId(id);
        }

        public Task GetTaskWithReport(int id)
        {
            return _taskDal.GetTaskWithReport(id);
        }
    }
}
