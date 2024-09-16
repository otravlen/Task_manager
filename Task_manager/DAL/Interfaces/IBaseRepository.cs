namespace Task_manager.DAL.Interfaces
{
    public interface IBaseRepository<T>
    {
        /// <summary>
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task Create(T entity);
        IQueryable<T> GetAll();
        Task Delete(T entity);
        Task<T> Update(T entity);
    }
}
