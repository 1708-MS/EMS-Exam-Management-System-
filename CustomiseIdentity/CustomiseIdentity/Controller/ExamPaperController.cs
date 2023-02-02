using CustomiseIdentity.Data;
using CustomiseIdentity.Identity;
using CustomiseIdentity.Models;
using CustomiseIdentity.Models.DTOs.ExamPaperDto;
using CustomiseIdentity.Models.DTOs.TeacherDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Newtonsoft.Json;
using System.Text.Json;
using System.Threading.Channels;

namespace CustomiseIdentity.Controller
{
    [Route("api/exampaper")]
    [ApiController]
    public class ExamPaperController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AccountController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JsonSerializerSettings _jsonSettings = new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };

        public ExamPaperController(ApplicationDbContext context, ILogger<AccountController> logger, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
        }

        // GET: api/ExamPaper

        [HttpGet]
        public async Task<IActionResult> GetAllExamPapers()
        {
            try
            {
                var examPapers = await _context.ExamPapers.ToListAsync();
                if (examPapers == null)
                {
                    return NotFound("No exam papers found");
                }
                var examPaperList = new List<GetAllExamPaperDto>();

                foreach (var examPaper in examPapers)
                {
                    var getAllExamPaperDto = new GetAllExamPaperDto
                    {
                        ExamPaperId = examPaper.ExamPaperId,
                        SubjectId = examPaper.SubjectId,
                        //ApplicationUserId = examPaper.ApplicationUsers.Select(ApplicationUser => ApplicationUser.Id).ToList(),
                        //QuestionId = examPaper.Questions.Select(Question => Question.QuestionId).ToList(),
                        //AnswerSheetId = examPaper.AnswerSheets.Select(AnswerSheet => AnswerSheet.AnswerSheetId).ToList(),
                    };
                    examPaperList.Add(getAllExamPaperDto);
                }
                return Ok(examPaperList);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // GET: api/ExamPaper/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetExamPaperById(int id)
        {
            try
            {
                var examPaper = await _context.ExamPapers.FindAsync(id);
                if (examPaper == null)
                {
                    return NotFound($"Exam Paper with id {id} was not found");
                }

                var getExamPaperByIdDto = new GetAllExamPaperDto
                {
                    ExamPaperId = examPaper.ExamPaperId,
                    SubjectId = examPaper.SubjectId,
                    //ApplicationUserId = examPaper.ApplicationUsers.Select(ApplicationUser => ApplicationUser.Id).ToList(),
                    //QuestionId = examPaper.Questions.Select(Question => Question.QuestionId).ToList(),
                    //AnswerSheetId = examPaper.AnswerSheets.Select(AnswerSheet => AnswerSheet.AnswerSheetId).ToList(),
                };
                return Ok(getExamPaperByIdDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateExamPaper(CreateExamPaperDto createExamPaperDto)
        {
            try
            {
                var examPaper = new ExamPaper
                {
                    SubjectId = createExamPaperDto.SubjectId,
                };

                var applicationUsers = new List<ApplicationUser>();
                foreach (var userId in createExamPaperDto.ApplicationUserId)
                {
                    var user = await _userManager.FindByIdAsync(userId);
                    if (user == null)
                    {
                        return BadRequest($"User with Id {userId} not found");
                    }
                    applicationUsers.Add(user);
                }
                examPaper.ApplicationUsers = applicationUsers;

                _context.ExamPapers.Add(examPaper);
                await _context.SaveChangesAsync();

                var subjectinexam = _context.Subjects.FirstOrDefault(Subject => Subject.SubjectId == examPaper.SubjectId);
                if (subjectinexam == null)
                {
                    return BadRequest();
                }
                subjectinexam.ExamPaperId = examPaper.ExamPaperId;
                await _context.SaveChangesAsync();


                return Ok(JsonConvert.SerializeObject((new { examPaperId = examPaper.ExamPaperId, subjectId=examPaper.SubjectId}), _jsonSettings));
            }
            catch (Exception ex)
            {
                var requestId = HttpContext.TraceIdentifier;
                _logger.LogError($"RequestId: {requestId} - Error creating exam paper. {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating exam paper");
            }
        }


        //[HttpPost]
        //public async Task<IActionResult> CreateExamPaper(CreateExamPaperDto createExamPaperDto)
        //{
        //    try
        //    {
        //        var examPaper = new ExamPaper
        //        {
        //            SubjectId = createExamPaperDto.SubjectId,
        //            ApplicationUsers = await _context.ApplicationUsers.Where(ApplicationUser => createExamPaperDto.ApplicationUserId.Contains(ApplicationUser.Id)).ToListAsync(),
        //            Questions = await _context.Questions.Where(Question => createExamPaperDto.QuestionId.Contains(Question.QuestionId)).ToListAsync(),
        //            AnswerSheets = await _context.AnswerSheets.Where(AnswerSheet => createExamPaperDto.AnswerSheetId.Contains(AnswerSheet.AnswerSheetId)).ToListAsync()
        //        };

        //        _context.ExamPapers.Add(examPaper);
        //        await _context.SaveChangesAsync();
        //        return Ok(new { examPaperId = examPaper.ExamPaperId });
        //    }
        //    catch (Exception ex)
        //    {
        //        var requestId = HttpContext.TraceIdentifier;
        //        _logger.LogError($"RequestId: {requestId} - Error adding exam paper. {ex}");
        //        return StatusCode(StatusCodes.Status500InternalServerError, "Error adding exam paper");
        //    }
        //}

        [HttpPut("{id}")]
        public async Task<IActionResult> PutExamPaper(int id, [FromBody] UpdateExamPaperDto updateExamPaperDto)
        {
            var examPaper = await _context.ExamPapers.FindAsync(id);
            if (examPaper == null)
            {
                return NotFound($"Exam paper with Id {id} does not exist");
            }
            
            examPaper.SubjectId = updateExamPaperDto.SubjectId;
            if (examPaper.SubjectId == 0)
            {
                return BadRequest("Subject Id is not selected/added. Select/Add a Subject");
            }
            var subject = await _context.Subjects.FindAsync(examPaper.SubjectId);
            if (subject == null)
            {
                return BadRequest("Invalid Subject Id selected/added.");
            }
            try
            {
                await _context.SaveChangesAsync();
                return Ok(examPaper);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExamPaper(int id)
        {
            try
            {
                var examPaper = await _context.ExamPapers.FindAsync(id);
                if (examPaper == null)
                {
                    return NotFound($"Exam paper with Id {id} not found");
                }
                _context.ExamPapers.Remove(examPaper);
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                var requestId = HttpContext.TraceIdentifier;
                _logger.LogError($"RequestId: {requestId} - Error deleting exam paper. {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting exam paper");
            }
        }
    }

}
