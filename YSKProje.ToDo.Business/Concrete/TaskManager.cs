﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
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

        public List<Task> GetAllTaskDatas(Expression<Func<Task, bool>> filter)
        {
            return _taskDal.GetAllTaskDatas(filter);
        }

        public List<Task> GetAllTaskDatasNotCompleted(out int totalPage, int userId, int activePage)
        {
            return _taskDal.GetCompletedAllTaskDatas(out totalPage, userId, activePage);
        }

        public int GetCompletedTaskCountByUserId(int userId)
        {
            return _taskDal.GetCompletedTaskCountByUserId(userId);
        }

        public int GetNotCompletedTaskCountByUserId(int userId)
        {
            return _taskDal.GetNotCompletedTaskCountByUserId(userId);
        }

        public int GetNotAssignTaskCount()
        {
            return _taskDal.GetNotAssignTaskCount();
        }

        public int GetCompletedTaskCount()
        {
            return _taskDal.GetCompletedTaskCount();
        }
    }
}
