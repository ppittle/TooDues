using System;
using System.Management.Automation;
using TooDues.Client.PowerShell.Client;
using TooDues.Tasks.Models;

namespace TooDues.Client.PowerShell
{
    [Cmdlet(VerbsCommon.Get, "TaskItem")]
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

    /// <summary>
    /// 0a) Finalize-DailyTooDueList (script that asks to close each open list item)
    /// 0b) Complete-DailyTooDueList
    ///    (current list most be stopped)
    ///    (warn when daily list expires?  its only valid for 1 day?)
    ///    (doesn't do anything with tasks)
    ///    (prompts to continue if Finalize hasn't been run)
    /// 
    /// 1) New-DailyTooDueList (bootstraps a new list, loads into session)
    /// 2) Add-TaskToDailyTooDueList or Remove-TaskFromDailyTooDueList
    /// 3) Approve-DailyTooDueList or Deny-DailyTooDueList
    /// 4) Complete-DailyTooDueListTaskItem
    ///
    /// 
    /// 5) Complete-DailyTooDueList
    /// 
    /// </summary>
    [Cmdlet(VerbsCommon.New, "DailyTooDueList")]
    public class CreateDailyTooDueList : Cmdlet
    {
        protected override void ProcessRecord()
        {
            var dailyList = TooDuesClient.DailyTooDueListService.CreateDailyTooDueList();

            TooDuesClient.DailyTooDueListState.PlanningList = dailyList;

            WriteObject(dailyList);
        }
    }

    [Cmdlet(VerbsLifecycle.Deny, "DailyTooDueList")]
    public class DenyDailyTooDueList : Cmdlet
    {
        protected override void ProcessRecord()
        {
            TooDuesClient.DailyTooDueListState.PlanningList = null;
        }
    }

    [Cmdlet(VerbsLifecycle.Approve, "DailyTooDueList")]
    public class ApproveDailyTooDueList : Cmdlet
    {
        protected override void ProcessRecord()
        {
            if (null == TooDuesClient.DailyTooDueListState.PlanningList)
                throw new Exception("No Daily Too Due List to Approve.  First call New-DailyTooDueList");

            var list = TooDuesClient.DailyTooDueListState.PlanningList;

            TooDuesClient.DailyTooDueListService.AcceptDailyTooDueList(list);

            TooDuesClient.DailyTooDueListState.CurrentList = list;
            TooDuesClient.DailyTooDueListState.PlanningList = null;
            TooDuesClient.DailyTooDueListState.HasUserFinalizedList = false;


            WriteObject(list);
        }
    }

    [Cmdlet(VerbsCommon.Get, "DailyTooDueList")]
    public class GetDailyTooDueList : Cmdlet
    {
        protected override void ProcessRecord()
        {
            WriteObject(TooDuesClient.DailyTooDueListState.CurrentList);
        }
    }


    [Cmdlet(VerbsLifecycle.Complete, "DailyTooDueList")]
    public class CompleteDailyTooDueList : Cmdlet
    {
        [Parameter(Mandatory = false)]
        public SwitchParameter Force { get; set; }

        protected override void ProcessRecord()
        {
            if (null == TooDuesClient.DailyTooDueListState.CurrentList)
                throw new Exception("No Daily Too Due List.  First call New-DailyTooDueList then Approve-DailyTooDueList");

            if (!Force.IsPresent && !TooDuesClient.DailyTooDueListState.HasUserFinalizedList)
                // TODO read tutorial on how to prompt user to continue
                throw new Exception("Build this: https://docs.microsoft.com/en-us/powershell/scripting/developer/cmdlet/requesting-confirmation-from-cmdlets?view=powershell-7");

            TooDuesClient.DailyTooDueListService.CompleteCurrentDailyTooDueList();

            TooDuesClient.DailyTooDueListState.CurrentList = null;
        }
    }
}