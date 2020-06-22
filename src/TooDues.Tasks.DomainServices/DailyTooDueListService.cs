using System;
using System.Linq;
using TooDues.Tasks.DomainServices.Data;
using TooDues.Tasks.Models;

namespace TooDues.Tasks.DomainServices
{
    public class DailyTooDueListService : IDailyTooDueListService
    {
        private readonly ITaskItemRepository _taskItemRepository;
        private readonly IDailyTooDueListRepository _dailyTooDueListRepository;

        public DailyTooDueListService(
            ITaskItemRepository taskItemRepository, 
            IDailyTooDueListRepository dailyTooDueListRepository)
        {
            _taskItemRepository = taskItemRepository;
            _dailyTooDueListRepository = dailyTooDueListRepository;
        }

        public DailyTooDueList CreateDailyTooDueList()
        {
            if (_dailyTooDueListRepository.TryGetCurrentTooDueList(out _))
                throw new DailyTooDueListAlreadyActiveException();

            return new DailyTooDueList
            {
                Date = DateTimeOffset.Now.Date,

                Tasks = 

                    // very naive task selection algorithm
                    _taskItemRepository
                        .GetAll()
                        // ignore any 'snoozed' tasks
                        .Where(x => !x.IsSnoozed())
                        .OrderBy(x => x.Priority)
                        .Take(3)
                        .ToList()
            };
        }

        public void AcceptDailyTooDueList(DailyTooDueList tooDueList)
        {
            _dailyTooDueListRepository.SaveCurrentTooDueList(tooDueList);
        }

        public DailyTooDueList GetCurrentTooDueList()
        {
            if (!_dailyTooDueListRepository.TryGetCurrentTooDueList(out var currentList))
                currentList = new DailyTooDueList();

            return currentList;
        }

        public DailyTooDueList GetDailyTooDueList(DateTimeOffset day)
        {
            throw new NotImplementedException();
        }

        public void CompleteTask(DailyTooDueList tooDueList, TooDueTaskItem taskItem)
        {
            if (taskItem.Status != TooDueTaskItemLifecycleStatus.Finished)
                taskItem.Status = TooDueTaskItemLifecycleStatus.Finished;

            _taskItemRepository.UpsertTask(taskItem);
        }

        public void CompleteCurrentDailyTooDueList()
        {
            if (_dailyTooDueListRepository.TryGetCurrentTooDueList(out var tooDueList))
                _dailyTooDueListRepository.ArchiveTooDueList(tooDueList);
        }
    }
}