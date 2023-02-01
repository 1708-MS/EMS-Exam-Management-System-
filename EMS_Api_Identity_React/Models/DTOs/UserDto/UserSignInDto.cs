using EMS_Api_Identity_React.Models.Identity;

namespace EMS_Api_Identity_React.Models.DTOs.UserDto
{
    public class UserSignInDto
    {
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
    }
}
