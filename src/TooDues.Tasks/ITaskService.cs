using System;
using System.Collections.Generic;
using TooDues.Tasks.Models;

namespace TooDues.Tasks
{
    public interface ITaskService
    {
        TooDueTaskItem GetTask(Guid id);
        List<TooDueTaskItem> GetAll();
        TooDueTaskItem UpsertTask(TooDueTaskItem taskItem);
        void DeleteTask(TooDueTaskItem taskItem);

        /// <summary>
        /// Explore this, ideally tasks are completed as part of a
        /// <see cref="DailyTooDueList"/>
        /// </summary>
        void CompleteTask(TooDueTaskItem taskItem);
    }
}