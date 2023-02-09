using AutoMapper;
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
            ExamPaper = new ExamPaperRepository(_context);
            Question = new QuestionRepository(_context);
        }

        public ISubjectRepository Subject { get; private set; }

        public IExamPaperRepository ExamPaper { get; private set; }
        public IQuestionRepository Question { get; private set; }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}