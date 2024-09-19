using Task_manager.Domain;
using Task_manager.Entity;
using Task_manager.Models;
using Task_manager.Domain.Respone;

namespace Task_manager.Service.Interfaces
{
    public interface ITaskService
    {
        Task<IBaseRespone<TaskEntity>> Create(CreateTaskVievModel model);
        Task<DataTableResult> GetTasks(TaskFilter filter);

        Task<IBaseRespone<IEnumerable<TaskVievModel>>> GetCompletedTasks();

        Task<IBaseRespone<TaskVievModel>> GetTask(long id);
        Task<IBaseRespone<bool>> EndTask(long id);

        Task<IBaseRespone<TaskEntity>> Update(TaskVievModel model);

        Task<IBaseRespone<TaskEntity>> Delete(TaskVievModel model);
    }
}
