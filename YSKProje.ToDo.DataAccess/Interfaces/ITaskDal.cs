using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.ToDo.DataAccess.Interfaces
{
    public interface ITaskDal : IGenericDal<Task>
    {
        List<Task> GetNotCompletedTaskListWithUrgent();
        List<Task> GetAllTaskDatas();
        List<Task> GetAllTaskDatas(Expression<Func<Task,bool>> filter);
        Task GetTaskWithUrgent(int id);
        List<Task> GetTasksByAppUserId(int id);
        Task GetTaskWithReport(int id);
    }
}
