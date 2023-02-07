using AutoMapper;
using CustomiseIdentity.Data;
using CustomiseIdentity.Models;
using CustomiseIdentity.Repository.iRepository;

namespace CustomiseIdentity.Repository
{
    public class ExamPaperRepository : Repository<ExamPaper>, IExamPaperRepository
    {
        private readonly ApplicationDbContext _context;
        public ExamPaperRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(ExamPaper examPaper)
        {
            _context.Update(examPaper);
        }
    }
}
