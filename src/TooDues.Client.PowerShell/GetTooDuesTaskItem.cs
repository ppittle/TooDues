using System;
using System.Management.Automation;
using TooDues.Client.PowerShell.Client;
using TooDues.Tasks.Models;

namespace TooDues.Client.PowerShell
{
    [Cmdlet(VerbsCommon.Get, "TaskItem")]
    [OutputType(typeof(TooDueTaskItem))]
    public class GetTooDuesTaskItem : PSCmdlet
    {
        [Parameter(Mandatory = false, Position = 0)]
        public Guid Id { get; set; }

        protected override void ProcessRecord()
        {
            if (base.MyInvocation.BoundParameters.ContainsKey(nameof(Id)))  
            {
                WriteObject(TooDuesClient.TaskService.GetTask(Id));
            }
            else
            {
                WriteObject(TooDuesClient.TaskService.GetAll());
            }
        }
    }

    [Cmdlet(VerbsLifecycle.Complete, "DailyTooDueListTaskItem")]
    public class CompleteTooDueListTaskItem : Cmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public TooDueTaskItem TaskItem { get; set;  }

        protected override void ProcessRecord()
        {
            if (null == TooDuesClient.DailyTooDueListState.CurrentList)
                throw new Exception("No Daily Too Due List.  First call New-DailyTooDueList then Approve-DailyTooDueList");

            TooDuesClient.DailyTooDueListService.CompleteTask(
                TooDuesClient.DailyTooDueListState.CurrentList,
                TaskItem);
        }
    }
}