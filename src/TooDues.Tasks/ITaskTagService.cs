using System.Collections.Generic;
using TooDues.Tasks.Models;

namespace TooDues.Tasks
{
    public interface ITaskTagService
    {
        void AddTags(IEnumerable<string> tags, IEnumerable<TooDueTaskItem> taskItems);
        void RemoveTags(IEnumerable<string> tags, IEnumerable<TooDueTaskItem> taskItems);
    }

    public static class TaskTagServiceExtensions
    {
        public static void AddTags(this ITaskTagService taskTagService, IEnumerable<string> tags, TooDueTaskItem taskItem)
        {
            taskTagService.AddTags(tags, new [] { taskItem });
        }

        public static void AddTag(this ITaskTagService taskTagService, string tag, IEnumerable<TooDueTaskItem> taskItems)
        {
            taskTagService.AddTags(new []{ tag }, taskItems);
        }

        public static void AddTag(this ITaskTagService taskTagService, string tag, TooDueTaskItem taskItem)
        {
            taskTagService.AddTags(new[] { tag }, new [] { taskItem });
        }

        public static void RemoveTags(this ITaskTagService taskTagService, IEnumerable<string> tags, TooDueTaskItem taskItem)
        {
            taskTagService.RemoveTags(tags, new[] { taskItem });
        }

        public static void RemoveTag(this ITaskTagService taskTagService, string tag, IEnumerable<TooDueTaskItem> taskItems)
        {
            taskTagService.RemoveTags(new[] { tag }, taskItems);
        }

        public static void RemoveTag(this ITaskTagService taskTagService, string tag, TooDueTaskItem taskItem)
        {
            taskTagService.RemoveTags(new[] { tag }, new[] { taskItem });
        }
    }
}