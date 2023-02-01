using CustomiseIdentity.Data;
using CustomiseIdentity.Email.Email_Template;
using CustomiseIdentity.Identity;
using CustomiseIdentity.Models.DTOs.UserDto;
using CustomiseIdentity.Service.Email_Service.Email_Interface;
using CustomiseIdentity.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using MimeKit;
using System.Text;

namespace CustomiseIdentity.Controller
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
            SignInManager<ApplicationUser> signInManager, IEmailSender emailSender, IEmailTemplateService emailTemplateService,
            ILogger<AccountController> logger, ApplicationDbContext context)
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

        [HttpPost("register")]
        public async Task<IActionResult> UserSignUp(UserSignUpDto userSignUpDto)
        {
            try
            {
                if (await _userManager.FindByEmailAsync(userSignUpDto.UserEmail) == null)
                {
                    var applicationUser = new ApplicationUser
                    {
                        UserName = userSignUpDto.UserName,
                        Email = userSignUpDto.UserEmail
                    };

                    var result = await _userManager.CreateAsync(applicationUser, userSignUpDto.UserPassword);

                    if (result.Succeeded)
                    {
                        // Check if the "admin" role exists.
                        if (!await _roleManager.RoleExistsAsync(SD.Role_Admin))
                        {
                            var Role = new ApplicationRole();
                            Role.Name = SD.Role_Admin;
                            await _roleManager.CreateAsync(Role);
                        }

                        // If the role is "admin", assign the "admin" role to the user.
                        if (userSignUpDto.UserRole.ToLower() == SD.Role_Admin)
                        {
                            await _userManager.AddToRoleAsync(applicationUser, SD.Role_Admin);
                        }
                        //var code = await _userManager.GenerateEmailConfirmationTokenAsync(applicationUser);
                        //code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                        //var callbackUrl = Url.Action(nameof(ConfirmEmail), "Account", new { userId = applicationUser.Id, code }, protocol: HttpContext.Request.Scheme);

                        //string Message = "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>";
                        //var subject = "Confirm Account Registration";
                        //var builder = new BodyBuilder();
                        //builder.HtmlBody = _emailTemplateService.GetWelcomeEmailTemplate();
                        //string messageBody = string.Format(builder.HtmlBody,
                        //    String.Format("{0:dddd, d MMMM yyyy}", DateTime.Now),
                        //    subject,
                        //    userSignUpDto.UserEmail,
                        //    userSignUpDto.UserName,
                        //    userSignUpDto.UserPassword,
                        //    Message,
                        //    callbackUrl
                        //    );
                        //await _emailSender.SendEmailAsync(userSignUpDto.UserEmail, subject, messageBody);
                        return Ok(new { message = "New Admin User created successfully." });
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
                //_logger.LogError($"Error creating a new Admin User: {ex}");
                //return BadRequest(new { message = "An error occurred while creating a new Admin User." });
                var requestId = HttpContext.TraceIdentifier;
                _logger.LogError($"RequestId: {requestId} - Error creating user. {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating user");
            }
        }

    }
}
