using CustomiseIdentity.Models;
using CustomiseIdentity.Models.DTOs.SubjectDto;

namespace CustomiseIdentity.Repository.iRepository
{
    public interface ISubjectRepository
    {
        IEnumerable<Subject> GetAllSubjects();
        Subject GetSubjectById(int subjectId);
        void AddSubject(SubjectDto subjectDto);
        void UpdateSubject(Subject subject);
        void DeleteSubject(int subjectId);
        bool SubjectExists(int subjectId);
        void Save();
    }
}