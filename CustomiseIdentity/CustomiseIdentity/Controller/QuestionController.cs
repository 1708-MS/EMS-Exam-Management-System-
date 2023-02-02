using CustomiseIdentity.Data;
using CustomiseIdentity.Identity;
using CustomiseIdentity.Models;
using CustomiseIdentity.Models.DTOs.QuestionDto;
using Microsoft.AspNetCore.Builder.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CustomiseIdentity.Controller
{
    [Route("api/question")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public QuestionController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<ActionResult<Question>> CreateQuestion([FromBody] CreateQuestionDto createQuestionDto)
        {
            if (createQuestionDto.QuestionType == QuestionType.ShortAnswerQuestion && createQuestionDto.QuestionType == QuestionType.LongAnswerQuestion)
            {
                if (createQuestionDto.ExamPaperId == null)
                {
                    var examPaperFromDb = await _context.ExamPapers.FindAsync(createQuestionDto.ExamPaperId);
                    if (examPaperFromDb != null)
                    {
                        var question = new Question
                        {
                            QuestionText = createQuestionDto.QuestionText,
                            QuestionMarks = createQuestionDto.QuestionMarks,
                            QuestionType = QuestionType.ShortAnswerQuestion,
                            ExamPaperId = createQuestionDto.ExamPaperId
                        };
                        _context.Questions.Add(question);
                        await _context.SaveChangesAsync();
                        return Ok();
                    }
                }
            }
            else
            {
                if (createQuestionDto.QuestionType == QuestionType.MCQs)
                {
                    if (createQuestionDto.ExamPaperId == null)
                    {
                        return BadRequest("ExamPaperId cannot be null, please select an exam paper");
                    }

                    if (createQuestionDto.MCQOptions == null || !createQuestionDto.MCQOptions.Any())
                    {
                        return BadRequest("MCQOptions cannot be null, please provide at least one option");
                    }
                    var examPaperFromDb = await _context.ExamPapers.FindAsync(createQuestionDto.ExamPaperId);
                    if (examPaperFromDb == null)
                    {
                        return BadRequest("Invalid ExamPaperId, please select a valid exam paper");
                    }

                    var question = new Question
                    {
                        QuestionText = createQuestionDto.QuestionText,
                        QuestionMarks = createQuestionDto.QuestionMarks,
                        QuestionType = QuestionType.MCQs,
                        ExamPaperId = createQuestionDto.ExamPaperId
                    };

                    var mcqOptions = createQuestionDto.MCQOptions.Select(option => new MCQOption
                    {
                        SubmittedAnswerOfMCQ = option.MCQOption,
                        Question = question
                    }).ToList();
                    if (mcqOptions.Count > 1)
                    {
                        question.MCQOptions = mcqOptions;
                        _context.Questions.Add(question);

                        await _context.SaveChangesAsync();
                        return Ok();
                    }
                    return BadRequest("Add atleast 2 options for the MCQ");
                }
            }
            return BadRequest("Invalid QuestionType, please select a valid QuestionType: ShortAnswerQuestion = 1, LongAnswerQuestion = 2, MCQs = 3");
        }
    }
}
