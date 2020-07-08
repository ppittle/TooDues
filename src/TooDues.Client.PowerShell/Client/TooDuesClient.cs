using System;
using Microsoft.Extensions.DependencyInjection;
using TooDues.Tasks;
using TooDues.Tasks.DomainServices.FileSystem.Client;
using TooDues.Tasks.Models;

namespace TooDues.Client.PowerShell.Client
{
    public static class TooDuesClient
    {
        public static void Connect()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection
                .AddOptions()
                .AddTooDuesTasksViaLocalFiles();

            var provider = serviceCollection.BuildServiceProvider();

            _dailyTooDueListService = provider.GetService<IDailyTooDueListService>();
            _taskService = provider.GetService<ITaskService>();
            _taskTagService = provider.GetService<ITaskTagService>();

            _dailyTooDueListState = new DailyTooDueListState
            {
                CurrentList = _dailyTooDueListService.GetCurrentTooDueList()
            };
        }

        private static IDailyTooDueListService _dailyTooDueListService;
        public static IDailyTooDueListService DailyTooDueListService
        {
            get
            {
                EnsureConnected();

                return _dailyTooDueListService;
            }
        }

        private static ITaskService _taskService;
        public static ITaskService TaskService
        {
            get
            {
                EnsureConnected();

                return _taskService;
            }
        }

        private static ITaskTagService _taskTagService;
        public static ITaskTagService TaskTagService
        {
            get
            {
                EnsureConnected();

                return _taskTagService;
            }
        }

        private static DailyTooDueListState _dailyTooDueListState;
        public static DailyTooDueListState DailyTooDueListState
        {
            get
            {
                EnsureConnected();

                return _dailyTooDueListState;
            }
        }

        private static void EnsureConnected()
        {
            if (null == _taskService)
                throw new Exception("Client is not connected yet.  Must first call Connect-TooDues");
        }
    }

    /// <summary>
    /// "Client Application" state for managing planning workflow in this
    /// client implementation.
    /// </summary>
    public class DailyTooDueListState
    {
        public DailyTooDueList CurrentList { get; set; }
        public DailyTooDueList PlanningList { get; set; }
    }
}