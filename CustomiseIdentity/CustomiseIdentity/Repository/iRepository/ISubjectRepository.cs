using CustomiseIdentity.Models;
using CustomiseIdentity.Models.DTOs.SubjectDto;

namespace CustomiseIdentity.Repository.iRepository
{
    public interface ISubjectRepository : IRepository<Subject>
    {
        void Update(Subject subject);
    }
}