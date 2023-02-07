using AutoMapper;
using CustomiseIdentity.Data;
using CustomiseIdentity.Models;
using CustomiseIdentity.Models.DTOs.SubjectDto;
using CustomiseIdentity.Repository.iRepository;

namespace CustomiseIdentity.Repository
{
    public class SubjectRepository : Repository<Subject>, ISubjectRepository
    {
        private readonly ApplicationDbContext _context;
        public SubjectRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
         
        }

        public void Update(Subject subject)
        {
            _context.Update(subject);
        }
    }
}