using Microsoft.Extensions.Options;
using TooDues.Tasks.DomainServices.Data;
using TooDues.Tasks.DomainServices.FileSystem.Data.Infrastructure;
using TooDues.Tasks.Models;

namespace TooDues.Tasks.DomainServices.FileSystem.Data
{
    internal class DailyTooDueListRepository : IDailyTooDueListRepository
    {
        private readonly FileRepository<DailyTooDueListDataModel> _fileRepo;

        public DailyTooDueListRepository(IOptions<FileSystemSettings> options)
        {
            _fileRepo = new FileRepository<DailyTooDueListDataModel>(options, nameof(DailyTooDueListRepository));
        }

        public bool TryGetCurrentTooDueList(out DailyTooDueList dailyTooDueList)
        {
            dailyTooDueList = new DailyTooDueList();

            if (null == _fileRepo.Data.Current)
                return false;

            
            dailyTooDueList = _fileRepo.Data.Current;
            return true;
        }

        public void SaveCurrentTooDueList(DailyTooDueList dailyTooDueList)
        {
            _fileRepo.Data.Current = dailyTooDueList;

            _fileRepo.FlushToDisk();
        }

        public void ArchiveTooDueList(DailyTooDueList dailyTooDueList)
        {
            if (_fileRepo.Data.Current?.Date != dailyTooDueList.Date)
                return;

            _fileRepo.Data.Current = null;

            _fileRepo.FlushToDisk();
        }
    }

    internal class DailyTooDueListDataModel
    {
        public DailyTooDueList? Current { get; set; }
    }
}