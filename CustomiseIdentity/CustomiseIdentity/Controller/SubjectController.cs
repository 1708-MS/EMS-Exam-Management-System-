using CustomiseIdentity.Data;
using CustomiseIdentity.Models.DTOs;
using CustomiseIdentity.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomiseIdentity.Controller
{
    [Route("api/subject")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AccountController> _logger;
        public SubjectController(ApplicationDbContext context, ILogger<AccountController> logger)
        {
            _context = context;
            _logger = logger;
        }

        //Add Subjects only to the database
        [HttpPost]
        public IActionResult AddSubject([FromBody] SubjectDto subjectDto)
        {
            try
            {
                if (subjectDto.ExamPaperId == -1)
                {
                    var subject = new Subject
                    {
                        SubjectName = subjectDto.SubjectName,
                        ExamPaperId = null,
                    };
                    _context.Subjects.Add(subject);
                    _context.SaveChanges();
                    return Ok();
                }
                else if (_context.ExamPapers.Any(ep => ep.ExamPaperId == subjectDto.ExamPaperId))
                {
                    var subject = new Subject
                    {
                        SubjectName = subjectDto.SubjectName,
                        ExamPaperId = subjectDto.ExamPaperId,
                    };
                    _context.Subjects.Add(subject);
                    _context.SaveChanges();
                    return Ok();
                }
                else
                {
                    return BadRequest("Invalid ExamPaperId");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding new subject.");
                return StatusCode(500, "An error occurred while adding new the subject. Please try again.");
                // return StatusCode(500, "An error occurred while adding a new subject. Please try again later.");
            }
        }

        [HttpGet]
        public IActionResult GetAllSubjects()
        {
            try
            {
                // Retrieve all subjects from the database
                var subjects = _context.Subjects.ToList();

                // Return the subjects
                return Ok(subjects);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving subjects.");
                return StatusCode(500, "An error occurred while retrieving subjects. Please try again.");
            }
        }

        //Get the full details of the specific Subjects through Id
        [HttpGet("{id}")]
        public IActionResult GetSubjectById(int id)
        {
            try
            {
                // Retrieve the subject by its id
                var subject = _context.Subjects.Find(id);

                if (subject == null)
                {
                    return NotFound("Subject not found");
                }

                // Return the subject
                return Ok(subject);
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "An error occurred while retrieving subject by id.");

                // Return a generic error message
                return StatusCode(500, "An error occurred while retrieving subject by id. Please try again.");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateSubject(int id, [FromBody] UpdateSubjectDto updateSubjectDto)
        {
            try
            {
                var subject = _context.Subjects.Find(id);

                if (subject == null)
                {
                    return NotFound("Subject not found");
                }

                // Update the subject properties
                subject.SubjectName = updateSubjectDto.SubjectName;
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the subject.");
                return StatusCode(500, "An error occurred while updating the subject. Please try again.");
            }
        }

        //Delete the selected Subject in the database
        [HttpDelete("{id}")]
        public IActionResult DeleteSubject(int id)
        {
            try
            {
                var subject = _context.Subjects.Find(id);

                if (subject == null)
                {
                    return NotFound("Subject not found");
                }
                _context.Subjects.Remove(subject);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the subject.");
                return StatusCode(500, "An error occurred while deleting the subject. Please try again  .");
            }
        }
    }
}
