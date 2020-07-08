using System;
using System.Management.Automation;
using TooDues.Client.PowerShell.Client;
using TooDues.Tasks.Models;

namespace TooDues.Client.PowerShell
{
    [Cmdlet(VerbsCommon.Set, "TaskItem")]
    [OutputType(typeof(TooDueTaskItem))]
    public class SetTooDuesTaskItem : PSCmdlet
    {
        [Parameter(Mandatory = true, Position = 0)]
        public Guid Id { get; set; }

        [Parameter(Mandatory = false)]
        public string Description { get; set; }

        [Parameter(Mandatory = false)]
        public int Priority { get; set; }
        
        [Parameter(Mandatory = false)]
        public DateTimeOffset TargetDueDate { get; set; }

        [Parameter(Mandatory = false)]
        public string Title { get; set; }

        protected override void ProcessRecord()
        {
            if (Id == Guid.Empty)
                throw new Exception($"{nameof(Id)} can not be the Empty Guid");

            var taskItem = TooDuesClient.TaskService.GetTask(Id);

            if (base.MyInvocation.BoundParameters.ContainsKey(nameof(Description)))
                taskItem.Description = Description;

            if (base.MyInvocation.BoundParameters.ContainsKey(nameof(Priority)))
                taskItem.Priority = Priority;

            if (base.MyInvocation.BoundParameters.ContainsKey(nameof(TargetDueDate)))
                taskItem.TargetDueDate = TargetDueDate;

            if (base.MyInvocation.BoundParameters.ContainsKey(nameof(Title)))
                taskItem.Title = Title;

            var fullTaskItem = TooDuesClient.TaskService.UpsertTask(taskItem);

            WriteObject(fullTaskItem);
        }
    }
}