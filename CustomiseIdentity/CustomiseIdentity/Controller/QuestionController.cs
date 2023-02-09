using AutoMapper;
using CustomiseIdentity.Data;
using CustomiseIdentity.Models;
using CustomiseIdentity.Models.DTOs.QuestionDto;
using CustomiseIdentity.Repository.iRepository;
using Microsoft.AspNetCore.Builder.Extensions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CustomiseIdentity.Controller
{
    [Route("api/question")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly JsonSerializerSettings _jsonSettings = new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };

        public QuestionController(ApplicationDbContext context, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _context = context;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        //This method saves a new Question in the database
        [HttpPost]
        public ActionResult CreateQuestion(CreateQuestionDto createQuestionDto)
        {
            if (ModelState.IsValid && createQuestionDto.QuestionType == QuestionType.ShortAnswerQuestion || createQuestionDto.QuestionType == QuestionType.LongAnswerQuestion)
            {
                if (createQuestionDto.ExamPaperId == null && createQuestionDto.MCQOptions != null) return BadRequest();
                var examPaperFromDb = _context.ExamPapers.Find(createQuestionDto.ExamPaperId);
                if (examPaperFromDb == null) return BadRequest();
                var createQuestion = _mapper.Map<Question>(createQuestionDto);
                _unitOfWork.Question.Add(createQuestion);
                _unitOfWork.Save();
                return Ok(createQuestion);
            }
            else
            {
                if (createQuestionDto.ExamPaperId == null && createQuestionDto.MCQOptions == null) return BadRequest();
                var examPaperFromDb = _context.ExamPapers.Find(createQuestionDto.ExamPaperId);
                if (examPaperFromDb == null) return BadRequest();
                var createQuestion = _mapper.Map<Question>(createQuestionDto);
                var mcqOptions = createQuestionDto.MCQOptions.Select(x => new MCQOption
                {
                    MCQOptionsOfQuestion = x.MCQOptionsOfQuestion
                }).ToList();
                if (mcqOptions.Count < 1) return BadRequest("Enter atleast 2 options");
                createQuestion.MCQOptions = mcqOptions;
                _unitOfWork.Question.Add(createQuestion);
                _unitOfWork.Save();
                return Ok(JsonConvert.SerializeObject(createQuestion, _jsonSettings));
            }
        }

        //[HttpGet("{id}")]
        //public async Task<ActionResult<GetAllQuestionsDto>> GetQuestion(int id)
        //{
        //    if (!ModelState.IsValid) return BadRequest();
        //    var question = await _unitOfWork.ques

        //    if (question == null)
        //    {
        //        return NotFound();
        //    }

        //    var questionDto = _mapper.Map<GetAllQuestionsDto>(question);
        //    return Ok(questionDto);
        //}

        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateQuestion(int id, UpdateQuestionDto updateQuestionDto)
        //{
        //    var questionFromRepo = await _unitOfWork.QuestionRepository.GetQuestion(id);

        //    if (questionFromRepo == null)
        //        return NotFound();

        //    _mapper.Map(updateQuestionDto, questionFromRepo);

        //    if (await _unitOfWork.SaveChangesAsync())
        //        return NoContent();

        //    throw new Exception($"Updating question {id} failed on save");
        //}


        //[HttpGet]
        //public async Task<IActionResult> GetAllQuestions()
        //{
        //    try
        //    {
        //        //var questions = await _context.Questions.ToListAsync();
        //        var questions = await _context.Questions.Include(Question => Question.MCQOptions).ToListAsync();
        //        if (questions == null) return NotFound("No questions found");
        //        var questionList = new List<GetAllQuestionsDto>();

        //        foreach (var question in questions)
        //        {
        //            var getAllQuestionDto = new GetAllQuestionsDto
        //            {
        //                QuestionId = question.QuestionId,
        //                QuestionText = question.QuestionText,
        //                QuestionMarks = question.QuestionMarks,
        //                ExamPaperId = question.ExamPaperId,
        //                AnswerId = question.AnswerId,
        //                QuestionType = question.QuestionType
        //            };
        //            questionList.Add(getAllQuestionDto);
        //        }
        //        return Ok(questionList);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //    }
        //}


    }
}
