
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using StudentCourseAPI.Data;
using StudentCourseAPI.Helpers;
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


        public async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _appContext.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var existingEntity = await _dbSet.FindAsync(id);

            if (existingEntity != null)
            {
                _dbSet.Remove(existingEntity);
                await _appContext.SaveChangesAsync();
            }
        }

      

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }   
            return await query.Where(predicate).ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _appContext.SaveChangesAsync();
            return entity;
        }

        public async Task<PagedResult<T>> GetAllAsync(PaginationParams? pagination, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            if (includes != null && includes.Any())
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            var pageNumber = pagination?.PageNumber ?? 1;
            var pageSize = pagination?.PageSize ?? 10;

            var totalCount = await query.CountAsync();
            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<T>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<PagedResult<T>> FindAsync(Expression<Func<T, bool>> predicate, PaginationParams? pagination, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet.Where(predicate);

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            var totalCount = await query.CountAsync();
            var items = await query
                .Skip((pagination.PageNumber - 1) * pagination.PageSize)
                .Take(pagination.PageSize)
                .ToListAsync();

            return new PagedResult<T>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = pagination.PageNumber,
                PageSize = pagination.PageSize
            };
        }
    }
}