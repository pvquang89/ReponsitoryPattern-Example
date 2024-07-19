using ReponsitoryPatternExample.Model;

namespace ReponsitoryPatternExample.Reponsitory
{
    public interface IReponsitory<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetEntityById(int id);
        Task AddEntity(T entity);
        Task UpdateEntity(T entity);
        Task DeleteEntityById(int id);
    }
}
