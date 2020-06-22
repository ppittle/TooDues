using System;
using System.Management.Automation;
using TooDues.Client.PowerShell.Client;
using TooDues.Tasks.Models;

namespace TooDues.Client.PowerShell
{
    [Cmdlet(VerbsCommon.New, "TaskItem")]
    public class NewTooDuesTaskItem : Cmdlet
    {
        [Parameter(Mandatory = true, Position = 0)]
        public string Title { get; set; }

        [Parameter(Mandatory = false)]
        public string Description { get; set; }

        [Parameter(Mandatory = false)]
        public int Priority { get; set; } = 0;

        [Parameter(Mandatory = false)]
        public DateTimeOffset? TargetDueDate { get; set; }

        protected override void ProcessRecord()
        {
            var taskItem = new TooDueTaskItem
            {
                Title = Title,
                Description = Description,
                Priority = Priority,
                TargetDueDate = TargetDueDate
            };

            var fullTaskItem = TooDuesClient.TaskService.UpsertTask(taskItem);

            WriteObject(fullTaskItem);
        }
    }
}