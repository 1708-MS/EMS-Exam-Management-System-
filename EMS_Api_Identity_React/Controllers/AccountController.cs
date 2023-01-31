using EMS_Api_Identity_React.Data;
using EMS_Api_Identity_React.Email;
using EMS_Api_Identity_React.Identity;
using EMS_Api_Identity_React.Models.DTOs;
using EMS_Api_Identity_React.Models.Identity;
using EMS_Api_Identity_React.Services.Email_Service.Email_Interface;
using EMS_Api_Identity_React.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using MimeKit;
using System.Text;

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
        private readonly IEmailTemplateService _emailTemplateService;
        private readonly ILogger<AccountController> _logger;
        private readonly ApplicationDbContext _context;

        public AccountController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager,
        SignInManager<ApplicationUser> signInManager, IEmailSender emailSender, IEmailTemplateService emailTemplateService, ILogger<AccountController> logger, ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _emailTemplateService = emailTemplateService;
            _logger = logger;
            _context = context;
        }


        [HttpPost("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                _logger.LogError($"Unable to load user with ID '{model.UserId}'.");
                return NotFound($"Unable to load user with ID '{model.UserId}'.");
            }
            var result = await _userManager.ConfirmEmailAsync(user, model.Code);
            if (!result.Succeeded)
            {
                _logger.LogError($"Error confirming email for user with ID '{model.UserId}': {string.Join(", ", result.Errors)}");
                return BadRequest($"Error confirming email for user with ID '{model.UserId}': {string.Join(", ", result.Errors)}");
            }
            return Ok();
        }

        [HttpPost("UserSignUp")]
        public async Task<IActionResult> UserSignUp([FromBody] UserSignUpDto userSignUpDto)
        {
            try
            {
                // Create roles
                if (!await _roleManager.RoleExistsAsync(SD.Role_Admin))
                {
                    await _roleManager.CreateAsync(new ApplicationRole(SD.Role_Admin));
                }
                if (!await _roleManager.RoleExistsAsync(SD.Role_Teacher))
                {
                    await _roleManager.CreateAsync(new ApplicationRole(SD.Role_Teacher));
                }
                if (!await _roleManager.RoleExistsAsync(SD.Role_Student))
                {
                    await _roleManager.CreateAsync(new ApplicationRole(SD.Role_Student));
                }
                if (await _userManager.FindByEmailAsync(userSignUpDto.UserEmail)==null)
                {
                    var applicationUser = new ApplicationUser()
                    {
                        UserName = userSignUpDto.UserName,
                        Email = userSignUpDto.UserEmail,
                    };
                    var result = await _userManager.CreateAsync(applicationUser, userSignUpDto.UserPassword);
                    if (result.Succeeded)
                    {
                        // Assign role to user
                        if (userSignUpDto.UserRole == SD.Role_Admin)
                        {
                            await _userManager.AddToRoleAsync(applicationUser, SD.Role_Admin);
                        }
                        //else if (userSignUpDto.UserRole == SD.Role_Teacher)
                        //{
                        //    await _userManager.AddToRoleAsync(applicationUser, SD.Role_Teacher);
                        //}
                        //else if (userSignUpDto.UserRole == "")
                        //{
                        //    await _userManager.AddToRoleAsync(applicationUser, SD.Role_Student);
                        //}
                        var code = await _userManager.GenerateEmailConfirmationTokenAsync(applicationUser);
                        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                        var callbackUrl = Url.Action(nameof(ConfirmEmail), "Account", new { userId = applicationUser.Id, code }, protocol: HttpContext.Request.Scheme);

                        string Message = "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>";
                        var subject = "Confirm Account Registration";
                        var builder = new BodyBuilder();
                        builder.HtmlBody = _emailTemplateService.GetWelcomeEmailTemplate();
                        string messageBody = string.Format(builder.HtmlBody,
                            String.Format("{0:dddd, d MMMM yyyy}", DateTime.Now),
                            subject,
                            userSignUpDto.UserEmail,
                            userSignUpDto.UserName,
                            userSignUpDto.UserPassword,
                            Message,
                            callbackUrl
                            );
                        await _emailSender.SendEmailAsync(userSignUpDto.UserEmail, subject, messageBody);
                        return Ok();    
                    }
                    switch (result.Errors)
                    {
                        case IdentityError e when e.Code == "DuplicateUserName":
                            return BadRequest("UserName already taken");
                        case IdentityError e when e.Code == "InvalidEmail":
                            return BadRequest("Invalid email address");
                        default:
                            return BadRequest("Error creating user");
                    }
                }
                else
                {
                    return BadRequest("Invalid data");
                }
            }
            catch (Exception ex)
            {
                //log exception using HttpContext.TraceIdentifier
                var requestId = HttpContext.TraceIdentifier;
                _logger.LogError($"RequestId: {requestId} - Error creating user. {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating user");
            }
        }
    }
}
