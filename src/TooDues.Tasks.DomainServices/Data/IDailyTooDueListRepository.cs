using TooDues.Tasks.Models;

namespace TooDues.Tasks.DomainServices.Data
{
    public interface IDailyTooDueListRepository
    {
        bool TryGetCurrentTooDueList(out DailyTooDueList dailyTooDueList);
        void SaveCurrentTooDueList(DailyTooDueList dailyTooDueList);
        /// <summary>
        /// Make sure <paramref name="dailyTooDueList"/> is no longer
        /// returned by <see cref="TryGetCurrentTooDueList"/>.
        /// <param />
        /// No current requirements on archiving lists, so this could also
        /// delete.
        /// </summary>
        void ArchiveTooDueList(DailyTooDueList dailyTooDueList);
    }
}