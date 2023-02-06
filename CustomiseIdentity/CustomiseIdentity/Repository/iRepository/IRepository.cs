using System.Linq.Expressions;

namespace CustomiseIdentity.Repository.iRepository
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        T GetById(int id);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);

    }
}
