    using StudentCourseAPI.Helpers;
using System.Linq.Expressions;

namespace StudentCourseAPI.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<PagedResult<T>> GetAllAsync(PaginationParams? pagination, string? sortBy = null,
                                        string? sortOrder = "asc", params Expression<Func<T, object>>[] includes );
        Task<PagedResult<T>> FindAsync(Expression<Func<T, bool>> predicate, PaginationParams? pagination, params Expression<Func<T, object>>[] includes);

        Task<T> GetByIdAsync(int id , params Expression<Func<T, object>>[]includes);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(int id);   
    }

}
