using System;
using System.Collections.Generic;

namespace TooDues.Tasks.Models
{
    public class DailyTooDueList
    {
        public DateTimeOffset Date { get; set; }
        /// <summary>
        /// Ordered list
        /// </summary>
        public List<TooDueTaskItem> Tasks { get; set; } = new List<TooDueTaskItem>();
    }
}