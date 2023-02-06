using AutoMapper;
using CustomiseIdentity.Data;
using CustomiseIdentity.Models;
using CustomiseIdentity.Models.DTOs.SubjectDto;
using CustomiseIdentity.Repository.iRepository;

namespace CustomiseIdentity.Repository
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public SubjectRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Add/Create the Subject and its details to the database
        public void AddSubject(SubjectDto subjectdto)
        {
            if (subjectdto != null)
            {
                var createSubject = _mapper.Map<Subject>(subjectdto);
                _context.Subjects.Add(createSubject);
                Save();
            }
            return;
        }

        // Delete the subject and its details from the databases
        public void DeleteSubject(int subjectId)
        {
            var subject = _context.Subjects.Find(subjectId);
            _context.Subjects.Remove(subject);
        }

        // Retrieve all subjects with its details from the database
        public IEnumerable<Subject> GetAllSubjects()
        {
            return _context.Subjects.ToList();
        }

        // Retrieve specific subject with its specific details from the database
        public Subject GetSubjectById(int subjectId)
        {
            return _context.Subjects.Find(subjectId);
        }

        // Save the changes made to the Subject properties
        public void Save()
        {
            _context.SaveChanges();
        }

        // Find the Subject details according to the S  ubjectId
        public bool SubjectExists(int subjectId)
        {
            return _context.Subjects.Any(e => e.SubjectId == subjectId);
        }

        // Edit and Update the Subject detials 
        public void UpdateSubject(Subject subject)
        {
            _context.Subjects.Update(subject);
        }
    }
}
