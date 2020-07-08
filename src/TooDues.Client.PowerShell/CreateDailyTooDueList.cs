using System;
using System.Management.Automation;
using TooDues.Client.PowerShell.Client;
using TooDues.Tasks.Models;

namespace TooDues.Client.PowerShell
{
    /// <summary>
    /// 0b) Complete-DailyTooDueList
    ///    (current list most be stopped)
    ///    (warn when daily list expires?  its only valid for 1 day?)
    ///    (prompts if Tasks aren't completed)
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
    [OutputType(typeof(DailyTooDueList))]
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
    [OutputType(typeof(DailyTooDueList))]
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

            WriteObject(list);
        }
    }
}