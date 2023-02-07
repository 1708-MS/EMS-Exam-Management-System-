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
           if(ModelState.IsValid)
            {
                //var subjects = _unitOfWork.Subject.GetAll().Select(_mapper.Map<Subject, GetSubjectDto>);
                //return Ok(subjects);
                var subjects = _unitOfWork.Subject.GetAll();
                var getSubjectsDto = _mapper.Map<IEnumerable<GetSubjectDto>>(subjects);
                return Ok(getSubjectsDto);
            }
            return BadRequest();

        }

        // GetSubject retreives specific Subjects through Id and its details from the database
        [HttpGet("{id}")]
        public ActionResult GetSubject(int id)
        {
            if (ModelState.IsValid)
            {
                var subject = _unitOfWork.Subject.Get(id);
                if (subject != null)
                {
                    var subjectDto = _mapper.Map<AddSubjectDto>(subject);
                    return Ok(subjectDto);
                }
                return NotFound();
            }
            return BadRequest();

        }

        public ActionResult AddSubject([FromBody] AddSubjectDto addSubjectDto)
        {
            if (addSubjectDto == null)
                return BadRequest(ModelState);  //400
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var subject = _mapper.Map<AddSubjectDto, Subject>(addSubjectDto);

            if (_unitOfWork.Subject.Exists(s => s.SubjectName == subject.SubjectName))
            {
                return BadRequest("A subject with the same name already exists.");
            }
            _unitOfWork.Subject.Add(subject);
            return Ok(subject);
        }

        // UpdateSubject edit and 
        [HttpPut("{id}")]
        public ActionResult UpdateSubject(int id, UpdateSubjectDto updateSubjectDto)
        {
            if (ModelState.IsValid)
            {
                var subject = _unitOfWork.Subject.Get(id);
                if (subject != null)
                {
                    _mapper.Map(updateSubjectDto, subject);
                    _unitOfWork.Subject.Update(subject);
                    return Ok(updateSubjectDto);
                }
            }
            return NotFound();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteSubject(int id)
        {
            if (ModelState.IsValid)
            {
                var subject = _unitOfWork.Subject.Get(id);
                if (subject != null)
                {
                    _unitOfWork.Subject.Remove(subject);
                    return Ok();
                }
                return NotFound();
            }
            return BadRequest();
        }
    }
}
