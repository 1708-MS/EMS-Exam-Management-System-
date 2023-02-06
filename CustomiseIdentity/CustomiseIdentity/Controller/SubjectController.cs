using AutoMapper;
using CustomiseIdentity.Models;
using CustomiseIdentity.Models.DTOs.SubjectDto;
using CustomiseIdentity.Repository.iRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace CustomiseIdentity.Controller
{
    [Route("api/subjects")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<AccountController> _logger;
        public SubjectController(ISubjectRepository subjectRepository, IMapper mapper, ILogger<AccountController> logger)
        {
            _subjectRepository = subjectRepository;
            _mapper = mapper;
            _logger = logger;
        }

        // Retreive All Subjects and its details from the database
        [HttpGet]
        public ActionResult<IEnumerable<SubjectDto>> GetAllSubjects()
        {
            var subjects = _subjectRepository.GetAllSubjects();
            var subjectDtos = _mapper.Map<IEnumerable<SubjectDto>>(subjects);
            return Ok(subjectDtos);
        }

        // Retreive the specific Subject and its details from the database through SubjectId
        [HttpGet("{subjectId}")]
        public ActionResult<SubjectDto> GetSubjectById(int subjectId)
        {
            var subject = _subjectRepository.GetSubjectById(subjectId);
            if (subject == null)
            {
                return NotFound();
            }
            var subjectDto = _mapper.Map<SubjectDto>(subject);
            return Ok(subjectDto);
        }

        // Save the Subject details in the database
        [HttpPost]
        public IActionResult AddSubject([FromBody] SubjectDto subjectDto)
        {

             _subjectRepository.AddSubject(subjectDto);
            return Ok(subjectDto);
        }



        // Edit and Update the details of the Subject which are already saved in the database
        [HttpPut("{subjectId}")]
        public IActionResult UpdateSubject(int subjectId, SubjectDto subjectDto)
        {
            var subject = _mapper.Map<Subject>(subjectDto);
            subject.SubjectId = subjectId;

            if (!_subjectRepository.SubjectExists(subjectId))
            {
                return NotFound();
            }

            _subjectRepository.UpdateSubject(subject);
            _subjectRepository.Save();
            return Ok(subjectDto);
        }

        // Delete the full details of the subject from the database
        [HttpDelete("{subjectId}")]
        public IActionResult DeleteSubject(int subjectId)
        {
            if (!_subjectRepository.SubjectExists(subjectId))
            {
                return NotFound();
            }

            _subjectRepository.DeleteSubject(subjectId);
            _subjectRepository.Save();
            return Ok();
        }
    }
}
