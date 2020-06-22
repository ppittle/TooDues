using System;
using TooDues.Tasks.Models;

namespace TooDues.Tasks
{
    /// <summary>
    /// Thrown if you try to call <see cref="IDailyTooDueListService.CreateDailyTooDueList"/>
    /// while there is still an Active <see cref="DailyTooDueList"/>.
    /// <para />
    /// Call <see cref="IDailyTooDueListService.CompleteDailyTooDueList"/> to end the currently active list.
    /// </summary>
    public class DailyTooDueListAlreadyActiveException : Exception
    {

    }
}