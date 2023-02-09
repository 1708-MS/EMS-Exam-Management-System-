using AutoMapper;
using CustomiseIdentity.Data;
using CustomiseIdentity.Models;
using CustomiseIdentity.Models.DTOs.ExamPaperDto;
using CustomiseIdentity.Repository;
using CustomiseIdentity.Repository.iRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace CustomiseIdentity.Controller
{
    [Route("api/exampaper")]
    [ApiController]
    public class ExamPaperController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly JsonSerializerSettings _jsonSettings = new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };

        public ExamPaperController(ApplicationDbContext context, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _context = context;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // This method create and save a new ExamPaper created by a specific application user with specific subject
        [HttpPost]
        public ActionResult<ExamPaper> CreateExamPaper(CreateExamPaperDto createExamPaperDto)
        {
            if (!ModelState.IsValid) return BadRequest();
            var examPaper = _mapper.Map<CreateExamPaperDto, ExamPaper>(createExamPaperDto);
            _unitOfWork.ExamPaper.Add(examPaper);
            _unitOfWork.Save();
            var subjectInExam = _unitOfWork.Subject.FirstOrDefault(Subject => Subject.SubjectId == examPaper.SubjectId);
            if (subjectInExam == null) return BadRequest();
            subjectInExam.ExamPaperId = examPaper.ExamPaperId;
            _unitOfWork.Save();
            return Ok(JsonConvert.SerializeObject(examPaper, _jsonSettings));
        }

        /// <summary>
        /// This method retreives all ExamPapers with details like SubjectId, ApplicationUserId, All the Questions, Answers, AnswerSheetfrom the database
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAllExamPaper()
        {
            if (!ModelState.IsValid) return BadRequest();
            var examPaperFromDb = _unitOfWork.ExamPaper.GetAll(includeProperties: "Subject,ApplicationUsers,Questions,AnswerSheets");
            var examPaperDtos = _mapper.Map<IEnumerable<GetExamPaperDto>>(examPaperFromDb);
            return Ok(examPaperDtos);
        }

        /// <summary>
        /// This methods retrieves the full info of the sepcified Exampaper 
        /// including the details of Subject, ApplicationUser, AnswerSheet, Answer, Questions
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetExamPaperById(int id)
        {
            if (!ModelState.IsValid) return BadRequest();
            var examPaperFromDb = _unitOfWork.ExamPaper.FirstOrDefault(ExamPaper => ExamPaper.ExamPaperId == id, includeProperties: "Subject,Questions,AnswerSheets");
            if (examPaperFromDb == null) return NotFound();
            return Ok(JsonConvert.SerializeObject(examPaperFromDb, _jsonSettings));
        }

        /// <summary>
        /// In this method, the Details of the Exampaper can be edited and updated. 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateExamPaperDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult UpdateExamPaper(int id, [FromBody] UpdateExamPaperDto updateExamPaperDto)
        {
            // Validate if the input id is equal to the id in the URL
            if (id == updateExamPaperDto.ExamPaperId && !ModelState.IsValid) return BadRequest();
            var examPaperFromDb = _unitOfWork.ExamPaper.Get(id);
            if (examPaperFromDb == null) return NotFound();
            var updateExamPaper = _mapper.Map<ExamPaper>(updateExamPaperDto);
            _unitOfWork.ExamPaper.Update(updateExamPaper);
            _unitOfWork.Save();
            return Ok(updateExamPaperDto);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteExamPaper(int id)
        {
            if (!ModelState.IsValid) return BadRequest("Unable to delete ExamPaper");
            var examPaper = _unitOfWork.ExamPaper.Get(id);
            if (examPaper == null) return NotFound();
            _unitOfWork.ExamPaper.Remove(examPaper);
            _unitOfWork.Save();
            return Ok(examPaper);

        }
    }
}
