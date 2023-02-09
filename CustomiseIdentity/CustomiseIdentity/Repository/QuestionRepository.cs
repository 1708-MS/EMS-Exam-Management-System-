using CustomiseIdentity.Data;
using CustomiseIdentity.Models;
using CustomiseIdentity.Repository.iRepository;

namespace CustomiseIdentity.Repository
{
    public class QuestionRepository : Repository<Question>, IQuestionRepository
    {
        private readonly ApplicationDbContext _context;
        public QuestionRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public void Update(Question question)
        {
            _context.Update(question);
        }
    }
}
