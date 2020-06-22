using System;
using System.Collections.Generic;

namespace TooDues.Tasks.Models
{
    // TODO: Tasks can have pre-requirements.  Tasks can have a parent.
    // TODO: Move scheduling properties to a different model/service?
    // TODO: Move Status properties to a different model/service?
    public class TooDueTaskItem
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTimeOffset? TargetDueDate { get; set; }
        public List<TooDueTag> Tags { get; set; } = new List<TooDueTag>();
        public TooDueEstimate Estimate { get; set; }

        /// <summary>
        /// Don't auto schedule until hitting a certain date.
        /// </summary>
        public DateTimeOffset? SnoozeAutoScheduleUntil { get; set; }
        /// <summary>
        /// Higher is more important
        /// </summary>
        public int Priority { get; set; } = 0;

        /// <summary>
        /// TODO - move to its own service?
        /// Is the item complete or in progress?
        /// </summary>
        public TooDueTaskItemLifecycleStatus Status { get; set; }
    }

    public static class TooDueTaskItemExtensions
    {
        public static bool IsSnoozed(this TooDueTaskItem taskItem)
        {
            return
                null != taskItem.SnoozeAutoScheduleUntil &&
                taskItem.SnoozeAutoScheduleUntil.Value > DateTimeOffset.Now.Date;
        }
    }
}
