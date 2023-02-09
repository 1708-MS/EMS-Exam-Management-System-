using CustomiseIdentity.Models;

namespace CustomiseIdentity.Repository.iRepository
{
    public interface IQuestionRepository : IRepository<Question>
    {
        void Update(Question question);

    }
}
