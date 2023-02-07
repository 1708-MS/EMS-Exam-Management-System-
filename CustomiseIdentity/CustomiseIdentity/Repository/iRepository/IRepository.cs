using System.Linq.Expressions;

namespace CustomiseIdentity.Repository.iRepository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll(
              Expression<Func<T, bool>> filter = null,
              Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
              String includeProperties = null
              );
        T FirstOrDefault(
            Expression<Func<T, bool>> filter = null,
            String includeProperties = null
            );
        T Get(int id);
        void Add(T entity);
        void Remove(T entity);
        void Remove(int Id);
        void RemoveRange(IEnumerable<T> entity);
        bool Exists(Expression<Func<T, bool>> filter);
        bool Save();
    }
}