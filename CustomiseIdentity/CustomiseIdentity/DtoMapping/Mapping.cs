using AutoMapper;
using CustomiseIdentity.Identity;
using CustomiseIdentity.Models;
using CustomiseIdentity.Models.DTOs;

namespace CustomiseIdentity.DtoMapping
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<ApplicationUser, UserSignInDto>().ReverseMap();
            CreateMap<ApplicationUser, UserSignUpDto>().ReverseMap();
            CreateMap<ApplicationUser, GetAllTeacherDto>().ReverseMap();
            CreateMap<Subject, SubjectDto>().ReverseMap();
            CreateMap<ApplicationUser, TeacherSignUpDto>().ReverseMap();
            CreateMap<ApplicationUser, GetAllTeacherDto>().ReverseMap();
        }
    }
}
