using System.Collections.Generic;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.ToDo.DataAccess.Interfaces
{
    public interface ITaskDal : IGenericDal<Task>
    {
        List<Task> GetNotCompletedTaskListWithUrgent();
        List<Task> GetAllTaskDatas();
        Task GetTaskWithUrgent(int id);
        List<Task> GetTasksByAppUserId(int id);
        Task GetTaskWithReport(int id);
    }
}
