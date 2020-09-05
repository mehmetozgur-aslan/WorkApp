﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.ToDo.Business.Interfaces
{
    public interface ITaskService : IGenericService<Task>
    {
        List<Task> GetNotCompletedTaskListWithUrgent();
        List<Task> GetAllTaskDatas();
        Task GetTaskWithUrgent(int id);
        List<Task> GetTasksByAppUserId(int id);
        Task GetTaskWithReport(int id);
        List<Task> GetAllTaskDatas(Expression<Func<Task, bool>> filter);

    }
}
