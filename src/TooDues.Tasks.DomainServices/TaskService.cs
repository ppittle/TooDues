using System;
using System.Collections.Generic;
using TooDues.Tasks.DomainServices.Data;
using TooDues.Tasks.Models;

namespace TooDues.Tasks.DomainServices
{
    public class TaskService : ITaskService
    {
        private readonly ITaskItemRepository _taskItemRepository;

        public TaskService(ITaskItemRepository taskItemRepository)
        {
            _taskItemRepository = taskItemRepository;
        }

        public TooDueTaskItem GetTask(Guid id)
        {
            if (_taskItemRepository.TryGetTask(id, out var task))
                return task;

            throw new Exception($"Item not found: [{id}]");
        }

        public List<TooDueTaskItem> GetAll()
        {
            return _taskItemRepository.GetAll();
        }

        public TooDueTaskItem UpsertTask(TooDueTaskItem taskItem)
        {
            _taskItemRepository.UpsertTask(taskItem);

            return taskItem;
        }

        public void DeleteTask(TooDueTaskItem taskItem)
        {
            _taskItemRepository.DeleteTask(taskItem.Id);
        }

        public void CompleteTask(TooDueTaskItem taskItem)
        {
            throw new NotImplementedException();
        }
    }
}