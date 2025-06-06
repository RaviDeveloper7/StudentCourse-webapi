
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using StudentCourseAPI.Data;
using System.Linq.Expressions;

namespace StudentCourseAPI.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _appContext;
        private readonly DbSet<T> _dbSet;

        public Repository(AppDbContext appContext)
        {
            _appContext = appContext;
            _dbSet = appContext.Set<T>();
        }

        async Task<T> IRepository<T>.AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _appContext.SaveChangesAsync();
            return entity;
        }

        async Task IRepository<T>.DeleteAsync(int id)
        {
            var existingEntity = await _dbSet.FindAsync(id);

            if (existingEntity != null)
            {
                _dbSet.Remove(existingEntity);
                await _appContext.SaveChangesAsync();
            }
        }

        async Task<IEnumerable<T>> IRepository<T>.GetAllAsync(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.ToListAsync();
        }

        async Task<T> IRepository<T>.GetByIdAsync(int id, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);
        }

        async Task<T> IRepository<T>.UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _appContext.SaveChangesAsync();
            return entity;
        }
    }
}