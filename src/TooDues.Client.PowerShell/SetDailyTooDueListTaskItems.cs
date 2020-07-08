using System;
using System.Linq;
using System.Management.Automation;
using TooDues.Client.PowerShell.Client;
using TooDues.Tasks.Models;

namespace TooDues.Client.PowerShell
{
    [Cmdlet(VerbsCommon.Set, "DailyTooDueListTaskItems")]
    [OutputType(typeof(DailyTooDueList))]
    public class SetDailyTooDueListTaskItems : Cmdlet
    {
        [Parameter(Mandatory = true)]
        public TooDueTaskItem[] TaskItems { get; set; }

        protected override void ProcessRecord()
        {
            if (null == TooDuesClient.DailyTooDueListState.PlanningList)
                throw new Exception("No Daily Too Due List to Modify.  First call New-DailyTooDueList");

            TooDuesClient.DailyTooDueListState.PlanningList.Tasks = TaskItems.ToList();

            WriteObject(TooDuesClient.DailyTooDueListState.PlanningList);
        }
    }
}