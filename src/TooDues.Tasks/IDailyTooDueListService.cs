using System;
using TooDues.Tasks.Models;

namespace TooDues.Tasks
{
    public interface IDailyTooDueListService
    {
        /// <summary>
        /// Creates a new Agenda pre-populated with recommended <see cref="TooDueTaskItem"/>.
        /// </summary>
        /// <exception cref="DailyTooDueListAlreadyActiveException">
        /// Thrown if there is an Active Daily List.  Call <see cref="CompleteDailyTooDueList"/> to end
        /// the Active <see cref="DailyTooDueList"/> then you can create a new one.
        /// </exception>
        DailyTooDueList CreateDailyTooDueList();

        void AcceptDailyTooDueList(DailyTooDueList tooDueList);

        DailyTooDueList GetCurrentTooDueList();

        DailyTooDueList GetDailyTooDueList(DateTimeOffset day);

        void CompleteTask(DailyTooDueList tooDueList, TooDueTaskItem taskItem);

        /// <summary>
        /// Marks the list as complete.
        /// Note: It's up to the UI to run the daily review and have user review 
        /// <see cref="DailyTooDueList.Tasks"/> that are not Completed.
        /// </summary>
        void CompleteCurrentDailyTooDueList();
    }
}