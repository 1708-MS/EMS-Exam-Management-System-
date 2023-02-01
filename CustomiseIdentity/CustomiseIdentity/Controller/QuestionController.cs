using CustomiseIdentity.Data;
using CustomiseIdentity.Models;
using CustomiseIdentity.Models.DTOs.QuestionDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CustomiseIdentity.Controller
{
    [Route("api/question")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public QuestionController(ApplicationDbContext context)
        {
            _context = context;
        }
       
        [HttpPost]
        public async Task<ActionResult<Question>> CreateQuestion([FromBody] CreateQuestionDto createQuestionDto)
        {
            var question = new Question
            {
                QuestionText = createQuestionDto.QuestionText,
                QuestionMarks = createQuestionDto.QuestionMarks,
                MCQOptions = createQuestionDto.MCQOptionId,
                Answer = createQuestionDto.AnswerId,
                QuestionType = createQuestionDto.QuestionType
            };
            _context.Questions.Add(question);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetQuestion", new { id = question.QuestionId }, question);
        }
    }
}
