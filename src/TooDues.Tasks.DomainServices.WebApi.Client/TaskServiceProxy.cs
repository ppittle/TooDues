using System;
using System.Collections.Generic;
using TooDues.Tasks.Models;

namespace TooDues.Tasks.DomainServices.WebApi.Client
{
    public class TaskServiceProxy : ITaskService
    {
        //private readonly IHttpClientFactory _httpClientFactory;
        
        public TooDueTaskItem GetTask(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<TooDueTaskItem> GetAll()
        {
            throw new NotImplementedException();
        }

        public TooDueTaskItem UpsertTask(TooDueTaskItem taskItem)
        {
            throw new NotImplementedException();
        }

        public void DeleteTask(TooDueTaskItem taskItem)
        {
            throw new NotImplementedException();
        }

        public void CompleteTask(TooDueTaskItem taskItem)
        {
            throw new NotImplementedException();
        }
    }
}