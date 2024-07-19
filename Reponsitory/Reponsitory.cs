
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ReponsitoryPatternExample.Data;

namespace ReponsitoryPatternExample.Reponsitory
{
    public class Reponsitory<T> : IReponsitory<T> where T : class
    {

        private readonly AppDbContext _appDbContext;
        private readonly DbSet<T> _dbSet;

        public Reponsitory(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            _dbSet = appDbContext.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetEntityById(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task AddEntity(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteEntityById(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null)
                throw new ArgumentException($"Entity with id {id} not found.");

            _dbSet.Remove(entity);
            await _appDbContext.SaveChangesAsync();
        }       

        public async Task UpdateEntity(T entity)
        {
            //_dbSet.Attach(entity);
            //_appDbContext.Entry(entity).State = EntityState.Modified;
            _appDbContext.Update(entity);
            await _appDbContext.SaveChangesAsync();
        }



    }
}
