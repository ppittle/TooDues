using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;
using TooDues.Tasks.DomainServices.Data;
using TooDues.Tasks.DomainServices.FileSystem.Data.Infrastructure;
using TooDues.Tasks.Models;

namespace TooDues.Tasks.DomainServices.FileSystem.Data
{
    internal class TaskItemRepository : ITaskItemRepository
    {
        private readonly FileRepository<ConcurrentDictionary<Guid, TooDueTaskItem>> _fileRepo;

        public TaskItemRepository(IOptions<FileSystemSettings> options)
        {
            _fileRepo = new FileRepository<ConcurrentDictionary<Guid, TooDueTaskItem>>(options, nameof(TaskItemRepository));
        }

        public bool TryGetTask(Guid id, out TooDueTaskItem item)
        {
            return _fileRepo.Data.TryGetValue(id, out item);
        }

        public List<TooDueTaskItem> GetAll()
        {
            return 
                _fileRepo
                    .Data
                    .Values
                    .Where(x => x.Status != TooDueTaskItemLifecycleStatus.Finished)
                    .ToList();
        }

        public void UpsertTask(TooDueTaskItem taskItem)
        {
            if (taskItem.Id == Guid.Empty)
                taskItem.Id = Guid.NewGuid();

            _fileRepo.Data.AddOrUpdate(taskItem.Id, taskItem, (id, _) => taskItem);

            _fileRepo.FlushToDisk();
        }

        public void DeleteTask(Guid id)
        {
            _fileRepo.Data.TryRemove(id, out _);

            _fileRepo.FlushToDisk();
        }
    }
}