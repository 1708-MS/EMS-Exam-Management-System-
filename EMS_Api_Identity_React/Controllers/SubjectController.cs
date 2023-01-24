using EMS_Api_Identity_React.Data;
using EMS_Api_Identity_React.Models;
using EMS_Api_Identity_React.Models.DTOs;
using EMS_Api_Identity_React.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EMS_Api_Identity_React.Controllers
{
   // [Authorize(Roles =SD.Role_Admin)]
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
        //[HttpPost]
        //public IActionResult AddSubject([FromBody] Subject subject)
        //{
        //    // Validate the subject information
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    try
        //    {
        //        // Add the subject to the database
        //        _context.Subjects.Add(subject);
        //        _context.SaveChanges();

        //        // Return a success response
        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        //return StatusCode(500, "An error occurred while adding the subject. Please try again or contact the administrator.");
        //        // Log the exception
        //        _logger.LogError(ex, "An error occurred while adding the subject.");

        //        // Return a generic error message
        //        return StatusCode(500, "An error occurred while adding the subject. Please try again.");
        //    }
        //}


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

        //try
        //{
        //    // Create a new subject
        //    var subject = new Subject()
        //    {
        //        SubjectName = subjectDto.SubjectName,
        //        ExamPaperId = -1
        //    };

        //    // Add the subject to the database
        //    _context.Subjects.Add(subject);
        //    _context.SaveChanges();

        //    // Return a success response
        //    return Ok();
        //}
        //catch (Exception ex)
        //{
        //    // Log the exception
        //    _logger.LogError(ex, "An error occurred while adding the subject.");

        //    // Return a generic error message to the client
        //    return StatusCode(500, "An error occurred while adding the subject. Please try again.");
        //}

        //Get the full detials of all the Subjects available in the database
        [HttpGet]
        public IActionResult GetSubjects()
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

        //[HttpPut("{id}")]
        //public IActionResult UpdateSubject(int id, [FromBody] Subject subject)
        //{
        //    try
        //    {
        //        // Retrieve the subject from the database
        //        var existingSubject = _context.Subjects.Find(id);

        //        if (existingSubject == null)
        //        {
        //            return NotFound("Subject not found");
        //        }
        //        // Update the subject properties
        //        existingSubject.SubjectName = subject.SubjectName;
        //        existingSubject.ExamPaperId = subject.ExamPaperId;

        //        // Save the changes to the database
        //        _context.SaveChanges();

        //        // Return a success response
        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the exception
        //        _logger.LogError(ex, "An error occurred while updating the subject.");

        //        // Return a generic error message
        //        return StatusCode(500, "An error occurred while updating the subject. Please try again.");
        //    }
        //}

        //Edit and Update the Subject details in the database 
        [HttpPut("{id}")]
        public IActionResult UpdateSubject(int id, [FromBody] SubjectDto subjectDto)
        {
            try
            {
                var subject = _context.Subjects.Find(id);

                if (subject == null)
                {
                    return NotFound("Subject not found");
                }

                // Update the subject properties
                subject.SubjectName = subjectDto.SubjectName;
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the subject.");
                return StatusCode(500, "An error occurred while updating the subject. Please try again.");
            }
        }

        //[HttpDelete("{id}")]
        //public IActionResult DeleteSubject(int id)
        //{
        //    try
        //    {
        //        var subject = _context.Subjects.Find(id);

        //        if (subject == null)
        //        {
        //            return NotFound("Subject not found");
        //        }

        //        // Remove the subject from the database
        //        _context.Subjects.Remove(subject);
        //        _context.SaveChanges();
        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "An error occurred while deleting the subject.");

        //        return StatusCode(500, "An error occurred while deleting the subject. Please try again.");
        //    }
        //}


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
