using Microsoft.EntityFrameworkCore;
using Task_manager.DAL.Interfaces;
using Task_manager.Domain.Respone;
using Task_manager.Entity;
using Task_manager.Models;
using Task_manager.Service.Interfaces;
using Task_manager.Enum;
using Task_manager.Domain;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Task_manager.Service.Implementations
{
    public class TaskService : ITaskService
    {
        private readonly ILogger<TaskService> _logger;
        private readonly IBaseRepository<TaskEntity> _taskRepository;
        
        public TaskService(ILogger<TaskService> logger, IBaseRepository<TaskEntity> taskRepository)
            { 
            _taskRepository = taskRepository;
            _logger = logger;
            }

        public async Task<IBaseRespone<TaskVievModel>> GetTask(long id) 
        {
            try
            {
                var task = await _taskRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (task == null) 
                {
                    return new BaseRespone<TaskVievModel>()
                    {
                        Description = "Задача не найдена",
                        StatusCode = StatusCode.TaskNotFound
                    };
                }
                var data = new TaskVievModel()
                {
                    Id = task.Id,
                    Name = task.Name,
                    IsDone = task.IsDone == true ? "Готово" : "Не готово",
                    IsDoneBool = task.IsDone,
                    Description = task.Description,
                    Priority = task.Priority.GetDisplayName(),
                    Created = task.Created.ToLongDateString(),

                };
                _logger.LogInformation($"[TaskService.GetTask] получен элемент TASK ID: {task.Id}");
                return new BaseRespone<TaskVievModel>()
                {
                    StatusCode = StatusCode.OK,
                    Data = data
                };                    
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[TaskService.GetTasks]: {ex.Message}");
                return new BaseRespone<TaskVievModel>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
        public async Task<IBaseRespone<bool>> EndTask(long id)
        {
            try
            {
                var task = await _taskRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (task == null)
                {
                    return new BaseRespone<bool>()
                    {
                        Description = "Задача не найдена",
                        StatusCode = StatusCode.TaskNotFound
                    };
                }
                task.IsDone = true;
                await _taskRepository.Update(task);
                _logger.LogInformation($"[TaskService.GetTask] получен элемент TASK ID: {task.Id}");
                return new BaseRespone<bool>()
                {
                    StatusCode = StatusCode.OK,
                    Description = "Задача завершена"
                };
                
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, $"[TaskService.GetTasks]: {ex.Message}");
                return new BaseRespone<bool>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<DataTableResult> GetTasks(TaskFilter filter)
        {
            try
            {
                var tasks = _taskRepository.GetAll().
                    WhereIf(!string.IsNullOrWhiteSpace(filter.Name), x => x.Name == filter.Name).
                    WhereIf(filter.Priority.HasValue, x=> x.Priority == filter.Priority).
                    Where(x  => !x.IsDone).
                    Select(x => new TaskVievModel()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        IsDone = x.IsDone == true? "Готово" : "Не готово",
                        Description = x.Description,
                        Priority = x.Priority.GetDisplayName(),
                        Created = x.Created.ToLongDateString()
                    }).
                    Skip(filter.Skip).
                    Take(filter.PageSize);

                var count = _taskRepository.GetAll()
                 .Where(x => !x.IsDone)
                 .WhereIf(!string.IsNullOrWhiteSpace(filter.Name), x => x.Name == filter.Name)
                 .WhereIf(filter.Priority.HasValue, x => x.Priority == filter.Priority)
                 .Count();


                return new DataTableResult()
                {
                    Data = tasks,
                    Total = count,

                };

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[TaskService.GetTasks]: {ex.Message}");
                return new DataTableResult()
                {
                    Data = null,
                    Total = 0                   
                };
            }
        }

        public async Task<IBaseRespone<TaskEntity>> Update(TaskVievModel model)
        {
            try
            {
                model.Validate();
                var task = await _taskRepository.GetAll()
                   .FirstOrDefaultAsync(x => x.Id == model.Id);
                

                if (task == null)
                {
                    return new BaseRespone<TaskEntity>
                    {
                        Description = "Задача не найдена",
                        StatusCode = StatusCode.TaskIsHasAlready
                    };
                }  
                
                _logger.LogInformation($"Запрос на удаление задачи - {model.Name}");

                task.Name = model.Name;
                task.Description = model.Description;
                task.Priority = model.GetPriorityFromDisplayName();
                task.IsDone = model.IsDone == "false"? false : true;
                              
                await _taskRepository.Update(task);
                _logger.LogInformation($"Задача обновилась: {task.Name} ");

                return new BaseRespone<TaskEntity>()
                {
                    Description = "Задача обновлена!",
                    StatusCode = StatusCode.OK
                };

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[TaskService.Update]: {ex.Message}");
                return new BaseRespone<TaskEntity>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }


        }

        public async Task<IBaseRespone<TaskEntity>> Delete(TaskVievModel model)
        {
            try
            {
                var task = await _taskRepository.GetAll()
                  .FirstOrDefaultAsync(x => x.Id == model.Id);
                if (task == null)
                {
                    return new BaseRespone<TaskEntity>
                    {
                        Description = "Задача не найдена",
                        StatusCode = StatusCode.TaskIsHasAlready
                    };
                }
                await _taskRepository.Delete(task);
                return new BaseRespone<TaskEntity>()
                {
                    Description = "Задача удалена",
                    StatusCode = StatusCode.OK
                };
             }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[TaskService.Delete]: {ex.Message}");
                return new BaseRespone<TaskEntity>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseRespone<TaskEntity>> Create(CreateTaskVievModel model)
        {
            
            try
            {
                model.Validate();
                _logger.LogInformation($"Запрос на создание задачи - {model.Name}");
                var task = await _taskRepository.GetAll()
                    .Where(x => x.Created.Date == DateTime.Today)
                    .FirstOrDefaultAsync(x => x.Name == model.Name);
                if (task != null)
                {
                    return new BaseRespone<TaskEntity>
                    {
                        Description = "Задача с таким названием уже есть",
                        StatusCode = StatusCode.TaskIsHasAlready
                    };
                }
                task = new TaskEntity()
                {
                    Name = model.Name,
                    Description = model.Description,
                    Priority = model.Priority,
                    Created = DateTime.Now
                };
                await _taskRepository.Create(task);
                _logger.LogInformation($"Задача добавилась: {task.Name} ");
                return new BaseRespone<TaskEntity>()
                {
                    Description = "Задача добавлена",
                    StatusCode= StatusCode.OK
                };

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[TaskService.Create]: {ex.Message}");
                return new BaseRespone<TaskEntity>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseRespone<IEnumerable<TaskVievModel>>> GetCompletedTasks()
        {
            try
            {
                var tasks = await _taskRepository.GetAll().                    
                    Where(x => x.IsDone).
                    Select(x => new TaskVievModel()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        IsDone = x.IsDone == false ? "Не готово": "Готово",
                        Description = x.Description,
                        Priority = x.Priority.GetDisplayName(),
                        Created = x.Created.ToLongDateString()
                    }).
                    ToListAsync();
                _logger.LogInformation($"[TaskService.GetCompletedTasks] получено элементов {tasks.Count()}");

                return new BaseRespone<IEnumerable<TaskVievModel>>()
                {
                    Data = tasks,
                    StatusCode = StatusCode.OK

                };

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[TaskService.GetCompletedTasks]: {ex.Message}");
                return new BaseRespone<IEnumerable<TaskVievModel>>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        
    }
}
