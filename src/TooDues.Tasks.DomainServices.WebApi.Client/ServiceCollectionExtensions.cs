using Microsoft.Extensions.DependencyInjection;

namespace TooDues.Tasks.DomainServices.WebApi.Client
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTooDuesTasksViaWebApi(this IServiceCollection services)
        {
            return
                services
                    .AddScoped<ITaskService, TaskServiceProxy>();
        }
    }
}