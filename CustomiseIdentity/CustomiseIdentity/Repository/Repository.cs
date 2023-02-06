using CustomiseIdentity.Data;
using CustomiseIdentity.Migrations;
using CustomiseIdentity.Repository.iRepository;
using Microsoft.EntityFrameworkCore;

namespace CustomiseIdentity.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        internal DbSet<T> dbSet;
        public Repository(ApplicationDbContext context)
        {
            _context = context;
            dbSet = _context.Set<T>();
        }

        public void Create(T entity)
        {
            dbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            dbSet.Remove(entity);
        }

        public IQueryable<T> GetAll()
        {
            return dbSet.AsNoTracking();
        }

        public T GetById(int id)
        {
            return dbSet.Find(id);
        }

        public void Update(T entity)
        {
            dbSet.Update(entity);
        }
    }
}
