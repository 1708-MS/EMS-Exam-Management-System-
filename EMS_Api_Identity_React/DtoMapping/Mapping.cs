using AutoMapper;
using EMS_Api_Identity_React.Models;
using EMS_Api_Identity_React.Models.DTOs;
using EMS_Api_Identity_React.Models.Identity;
using System.Runtime;

namespace EMS_Api_Identity_React.DtoMapping
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<ApplicationUser, UserSignInDto>().ReverseMap();
            CreateMap<ApplicationUser, UserSignUpDto>().ReverseMap();
            CreateMap<Subject, SubjectDto>().ReverseMap();
        }
    }
}
