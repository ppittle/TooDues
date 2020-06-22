using System;
using System.Collections.Generic;
using TooDues.Tasks.Models;

namespace TooDues.Tasks.DomainServices.Data
{
    public interface ITaskItemRepository
    {
        bool TryGetTask(Guid id, out TooDueTaskItem item);
        /// <summary>
        /// NOTE: Automatically filters out <see cref="TooDueTaskItemLifecycleStatus.Finished"/>
        /// items.
        /// </summary>
        List<TooDueTaskItem> GetAll();
        void UpsertTask(TooDueTaskItem taskItem);
        void DeleteTask(Guid id);
    }
}