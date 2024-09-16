using Task_manager.DAL.Interfaces;
using Task_manager.DAL.Repositories;
using Task_manager.Entity;
using Task_manager.Enum;
using Task_manager.Service.Implementations;
using Task_manager.Service.Interfaces;

namespace Task_manager
{
    public static class Initialazer
    {
        public static void RegisterRepositories(this IServiceCollection services)
        {
            
            services.AddScoped<ITaskService, TaskService>();
        }



    }
}
