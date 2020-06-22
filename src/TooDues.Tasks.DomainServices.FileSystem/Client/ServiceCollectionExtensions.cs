using Microsoft.Extensions.DependencyInjection;
using TooDues.Tasks.DomainServices.Data;
using TooDues.Tasks.DomainServices.FileSystem.Data;

namespace TooDues.Tasks.DomainServices.FileSystem.Client
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTooDuesTasksViaLocalFiles(this IServiceCollection services)
        {
            return
                services
                    .AddScoped<IDailyTooDueListService, DailyTooDueListService>()
                    .AddScoped<ITaskTagService, TaskTagService>()
                    .AddScoped<ITaskService, TaskService>()
                    .AddScoped<IDailyTooDueListRepository, DailyTooDueListRepository>()
                    .AddScoped<ITaskItemRepository, TaskItemRepository>();
        }
    }
}