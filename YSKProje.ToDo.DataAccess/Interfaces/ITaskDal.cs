﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.ToDo.DataAccess.Interfaces
{
    public interface ITaskDal : IGenericDal<Task>
    {
        List<Task> GetNotCompletedTaskListWithUrgent();
        List<Task> GetAllTaskDatas();
        List<Task> GetAllTaskDatas(Expression<Func<Task, bool>> filter);
        List<Task> GetCompletedAllTaskDatas(out int totalPage, int userId, int activePage);
        Task GetTaskWithUrgent(int id);
        List<Task> GetTasksByAppUserId(int id);
        Task GetTaskWithReport(int id);
        int GetCompletedTaskCountByUserId(int userId);
        int GetNotCompletedTaskCountByUserId(int userId);
        int GetNotAssignTaskCount();
        int GetCompletedTaskCount();
    }
}
