using CustomiseIdentity.Models;

namespace CustomiseIdentity.Repository.iRepository
{
    public interface IExamPaperRepository : IRepository<ExamPaper>
    {
       void Update(ExamPaper examPaper);
    }
}
