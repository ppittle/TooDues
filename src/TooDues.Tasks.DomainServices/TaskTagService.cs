using System;
using System.Collections.Generic;
using TooDues.Tasks.Models;

namespace TooDues.Tasks.DomainServices
{
    public class TaskTagService : ITaskTagService
    {
        public void AddTags(IEnumerable<string> tags, IEnumerable<TooDueTaskItem> taskItems)
        {
            throw new NotImplementedException();
        }

        public void RemoveTags(IEnumerable<string> tags, IEnumerable<TooDueTaskItem> taskItems)
        {
            throw new NotImplementedException();
        }
    }
}