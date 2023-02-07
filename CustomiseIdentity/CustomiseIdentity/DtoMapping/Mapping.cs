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
            CreateMap<Subject, GetSubjectDto>().ReverseMap();
            CreateMap<Subject, AddSubjectDto>().ReverseMap();
            CreateMap<ApplicationUser, TeacherSignUpDto>().ReverseMap();
            CreateMap<ApplicationUser, GetAllTeacherDto>().ReverseMap();
            CreateMap<ApplicationUser, GetAllExamPaperDto>().ReverseMap();
            CreateMap<ApplicationUser, CreateExamPaperDto>().ReverseMap();
            CreateMap<ApplicationUser, UpdateExamPaperDto>().ReverseMap();
            CreateMap<ApplicationUser, CreateQuestionDto>().ReverseMap();
            CreateMap<ApplicationUser, GetAllQuestionsDto>().ReverseMap();
        }
    }
}
