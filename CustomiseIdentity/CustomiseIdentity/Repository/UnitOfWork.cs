using CustomiseIdentity.Data;
using CustomiseIdentity.Repository.iRepository;

namespace CustomiseIdentity.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Subject = new SubjectRepository(_context);
        }

        public ISubjectRepository Subject { get; private set; }
    }
}
