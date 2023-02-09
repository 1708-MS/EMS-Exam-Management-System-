using AutoMapper;
using CustomiseIdentity.Models;
using CustomiseIdentity.Models.DTOs.SubjectDto;
using CustomiseIdentity.Repository;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public SubjectController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GetSubjects methods retreives all Subjects and its details from the database
        [HttpGet]
        public ActionResult GetSubjects()
        {
            if (!ModelState.IsValid) return BadRequest();
            var subjects = _unitOfWork.Subject.GetAll();
            var getSubjectsDto = _mapper.Map<IEnumerable<GetSubjectDto>>(subjects);
            return Ok(getSubjectsDto);
        }

        // GetSubject retreives specific Subjects through Id and its details from the database
        [HttpGet("{id}")]
        public ActionResult GetSubject(int id)
        {
            if (!ModelState.IsValid) return BadRequest();
            var subject = _unitOfWork.Subject.Get(id);
            if (subject == null) return NotFound();
            var subjectDto = _mapper.Map<AddSubjectDto>(subject);
            return Ok(subjectDto);
        }

        // This method create and save a new Subject record with null ExamPaperId in the database
        [HttpPost]
        public IActionResult CreateSubject(AddSubjectDto addSubjectDto)
        {
            var createSubject = _mapper.Map<Subject>(addSubjectDto);
            _unitOfWork.Subject.Add(createSubject);
            _unitOfWork.Save();
            return Ok(createSubject);
        }

        // UpdateSubject edit and 
        [HttpPut]
        public ActionResult UpdateSubject(UpdateSubjectDto updateSubjectDto)
        {
            if (!ModelState.IsValid) return NotFound();
            var updateSubject = _mapper.Map<Subject>(updateSubjectDto);
            _unitOfWork.Subject.Update(updateSubject);
            _unitOfWork.Save();
            return Ok(updateSubjectDto);
        }


        // This method deletes the full record of the Subject from the database 
        [HttpDelete("{id}")]
        public ActionResult DeleteSubject(int id)
        {
            if (!ModelState.IsValid) return BadRequest();
            var subject = _unitOfWork.Subject.Get(id);
            if (subject == null) return NotFound();
            _unitOfWork.Subject.Remove(subject);
            _unitOfWork.Save();
            return Ok();
        }
    }
}
