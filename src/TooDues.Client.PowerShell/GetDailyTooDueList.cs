using System.Management.Automation;
using TooDues.Client.PowerShell.Client;
using TooDues.Tasks.Models;

namespace TooDues.Client.PowerShell
{
    [Cmdlet(VerbsCommon.Get, "DailyTooDueList")]
    [OutputType(typeof(DailyTooDueList))]
    public class GetDailyTooDueList : Cmdlet
    {
        [Parameter(Position = 0, Mandatory = false)]
        public SwitchParameter Planning { get; set; }

        protected override void ProcessRecord()
        {
            if (Planning.IsPresent)
            {
                WriteObject(TooDuesClient.DailyTooDueListState.PlanningList);
            }
            else
            {
                WriteObject(TooDuesClient.DailyTooDueListState.CurrentList);
            }
        }
    }
}