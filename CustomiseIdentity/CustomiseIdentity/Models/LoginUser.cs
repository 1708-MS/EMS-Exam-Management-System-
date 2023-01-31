using System.ComponentModel.DataAnnotations;

namespace CustomiseIdentity.Models
{
    public class LoginUser
    {
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; } = String.Empty;
        [Required]
        public string Password { get; set; } = String.Empty;
    }
}
