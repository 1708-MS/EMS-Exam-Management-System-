using EMS_Api_Identity_React.Identity;
using EMS_Api_Identity_React.Models.DTOs;
using EMS_Api_Identity_React.Models.Identity;
using EMS_Api_Identity_React.Services;
using EMS_Api_Identity_React.Services.Email_Service.Email_Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EMS_Api_Identity_React.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        
        public AccountController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, SignInManager<ApplicationUser> signInManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }

        [HttpPost("UserSignUp")]
        public async Task<IActionResult> UserSignUp([FromBody] UserSignUpDto userSignUpDto)
        {
            if (userSignUpDto != null && ModelState.IsValid)
            {
                var applicationUser = new ApplicationUser()
                {
                    UserName = userSignUpDto.UserName,
                    Email = userSignUpDto.UserEmail,
                };
                var result = await _userManager.CreateAsync(applicationUser, userSignUpDto.UserPassword);
                if (result.Succeeded)
                {
                    await _emailSender.SendEmailAsync(userSignUpDto.UserEmail, "Hello Testing", "Hello");
                    return Ok();
                }
            }
            return BadRequest(error: "Something Went Wrong");
        }
    }
}
