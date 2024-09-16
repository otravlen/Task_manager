using Task_manager.DAL.Interfaces;
using Task_manager.Entity;
using Microsoft.EntityFrameworkCore;
using Task_manager.DAL;

namespace Task_manager.DAL.Repositories
{
    public class TaskRepository: IBaseRepository<TaskEntity>
    {
        private readonly AppDbContext _AppDbContext;
        public TaskRepository(AppDbContext appDbContext)
        {
            _AppDbContext = appDbContext;
        }

        public async Task Create(TaskEntity entity)
        {
            await _AppDbContext.tasks.AddAsync(entity);
            await _AppDbContext.SaveChangesAsync();
        }

        public IQueryable<TaskEntity> GetAll()
        {
            return _AppDbContext.tasks;
        }
        public async Task Delete(TaskEntity entity)
        {
             _AppDbContext.tasks.Remove(entity);
            await _AppDbContext.SaveChangesAsync();
        }

        

        public async Task<TaskEntity> Update(TaskEntity entity)
        {
            _AppDbContext.Update(entity);
            await _AppDbContext.SaveChangesAsync();
            return entity;
        }
    }
}
