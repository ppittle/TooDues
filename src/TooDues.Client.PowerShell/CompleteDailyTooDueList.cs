using System;
using System.Linq;
using System.Management.Automation;
using TooDues.Client.PowerShell.Client;
using TooDues.Tasks.Models;

namespace TooDues.Client.PowerShell
{
    [Cmdlet(VerbsLifecycle.Complete, "DailyTooDueList")]
    public class CompleteDailyTooDueList : Cmdlet
    {
        [Parameter(Mandatory = false)]
        public SwitchParameter Force { get; set; }

        protected override void ProcessRecord()
        {
            if (null == TooDuesClient.DailyTooDueListState.CurrentList)
                throw new Exception("No Daily Too Due List.  First call New-DailyTooDueList then Approve-DailyTooDueList");

            if (!Force.IsPresent)
                PromptToCloseAnyOpenTasks();
            
            TooDuesClient.DailyTooDueListService.CompleteCurrentDailyTooDueList();

            TooDuesClient.DailyTooDueListState.CurrentList = null;
        }

        private void PromptToCloseAnyOpenTasks()
        {
            var currentList = TooDuesClient.DailyTooDueListState.CurrentList;

            var openTasks =
                currentList
                    .Tasks
                    .Where(x => x.Status != TooDueTaskItemLifecycleStatus.Finished)
                    .ToList();

            var yesToAll = false;
            var noToAll = false;

            foreach (var task in openTasks)
            {
                var keepOpen =
                    ShouldContinue(
                        task.Title,
                        "The following Task has not been marked Complete.  Would you like it to remain Active?",
                        ref yesToAll,
                        ref noToAll);

                if (!keepOpen)
                    TooDuesClient.DailyTooDueListService.CompleteTask(currentList, task);
            }

        }
    }
}