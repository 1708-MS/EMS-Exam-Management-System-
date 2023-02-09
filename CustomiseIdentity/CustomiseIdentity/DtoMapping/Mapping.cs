using AutoMapper;
using CustomiseIdentity.Identity;
using CustomiseIdentity.Models;
using CustomiseIdentity.Models.DTOs.ExamPaperDto;
using CustomiseIdentity.Models.DTOs.QuestionDto;
using CustomiseIdentity.Models.DTOs.SubjectDto;
using CustomiseIdentity.Models.DTOs.TeacherDto;
using CustomiseIdentity.Models.DTOs.UserDto;

namespace CustomiseIdentity.DtoMapping
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<ApplicationUser, UserSignInDto>().ReverseMap();
            CreateMap<ApplicationUser, UserSignUpDto>().ReverseMap();

            CreateMap<ApplicationUser, TeacherSignUpDto>().ReverseMap();
            CreateMap<ApplicationUser, GetAllTeacherDto>().ReverseMap();

            // This is for Subject model
            CreateMap<Subject, GetSubjectDto>().ReverseMap();
            CreateMap<Subject, AddSubjectDto>().ReverseMap();
            CreateMap<Subject, UpdateSubjectDto>().ReverseMap();

            //This is for Exam model
            CreateMap<ExamPaper, CreateExamPaperDto>().ReverseMap();
            CreateMap<ExamPaper, GetExamPaperDto>().ReverseMap();
            CreateMap<ExamPaper, UpdateExamPaperDto>().ReverseMap();

            // This is for Question model
            CreateMap<Question, CreateQuestionDto>().ReverseMap();
            CreateMap<MCQOption, MCQOptionDto>().ReverseMap();
            CreateMap<Question, GetQuestionsDto>().ReverseMap();

        }
    }
}
