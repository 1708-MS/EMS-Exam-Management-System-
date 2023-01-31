using AutoMapper;
using EMS_Api_Identity_React.Data;
using EMS_Api_Identity_React.Models;
using EMS_Api_Identity_React.Models.DTOs;
using EMS_Api_Identity_React.Models.Identity;
using EMS_Api_Identity_React.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace EMS_Api_Identity_React.Controllers
{

    [Route("api/teacher")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationUser> _roleManager;
        private readonly ILogger<AccountController> _logger;
        private readonly IMapper _mapper;


        public TeacherController(ApplicationDbContext context, UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, ILogger<AccountController> logger,
            RoleManager<ApplicationUser> roleManager, IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTeacher([FromBody] TeacherDto teacherDto)
        {
            try
            {
                //create a new ApplicationUser with the role of "Teacher"
                var user = new ApplicationUser
                {
                    UserName = teacherDto.TeacherUserName,
                    Email = teacherDto.TeacherUserName,
                    Address = teacherDto.TeacherAddress,
                    PhoneNumber = teacherDto.TeacherContactNumber,
                    Role = SD.Role_Teacher
                };
                var result = await _userManager.CreateAsync(user, teacherDto.TeacherPassword);
                if (result.Succeeded)
                {
                    //retrieve the Subject by Id
                    var subject = await _context.Subjects.FindAsync(teacherDto.SubjectDto.SubjectId);
                    if (subject != null)
                    {
                        //assign the subject to the teacher
                        user.Subjects.Add(subject);
                        _context.SaveChanges();
                    }
                    return Ok();
                }
                return BadRequest("Subject not found");
            }
            catch (Exception ex)
            {
                return BadRequest("Error creating teacher: " + ex.Message);
            }
        }


        //[HttpPost]
        //public async Task<IActionResult> CreateTeacher([FromBody] TeacherDto teacherDto)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return BadRequest(ModelState);
        //        }

        //        var user = new ApplicationUser
        //        {
        //            UserName = teacherDto.TeacherUserName,
        //            Email = teacherDto.TeacherUserName,
        //            Address = teacherDto.TeacherAddress,
        //            PhoneNumber = teacherDto.TeacherContactNumber
        //        };

        //        var result = await _userManager.CreateAsync(user, teacherDto.TeacherPassword);

        //        if (result.Succeeded)
        //        {
        //            await _userManager.AddToRoleAsync(user, SD.Role_Teacher);
        //            Subject subject = _context.Subjects.FirstOrDefault(s => s.SubjectId == teacherDto.SubjectId);
        //            if (subject != null)
        //            {
        //                user.Subjects.Add(subject);
        //                _context.SaveChanges();
        //            }
        //            return Ok();
        //        }

        //        return BadRequest(result.Errors);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "An error occurred while creating a new Teacher User.");
        //        return StatusCode(500, "An error occurred while creating a new Teacher User. Please try again.");
        //    }
        //}

        // [Authorize(Roles = SD.Role_Admin)]
        //[HttpPost]
        //public async Task<IActionResult> CreateTeacher([FromBody] TeacherDto teacherDto)
        //{
        //    try
        //    {
        //        // Check if username is already taken
        //        if (await _userManager.FindByNameAsync(teacherDto.UserName) == null)
        //        {
        //            // Create teacher user
        //            var teacherUser = new ApplicationUser()
        //            {
        //                UserName = teacherDto.UserName,
        //                Address = teacherDto.Address,
        //                PhoneNumber = teacherDto.ContactNumber
        //            };
        //            var result = await _userManager.CreateAsync(teacherUser, teacherDto.Password);
        //            if (result.Succeeded)
        //            {
        //                // Assign teacher role
        //                await _userManager.AddToRoleAsync(teacherUser, SD.Role_Teacher);

        //                // Assign subject to teacher
        //                var subject = _context.Subjects.Find(teacherDto.SubjectId);
        //                if (subject != null)
        //                {
        //                    teacherUser.Subjects = new List<Subject> { subject };
        //                    _context.SaveChanges();
        //                }
        //                else
        //                {
        //                    return BadRequest("Invalid SubjectId");
        //                }

        //                return Ok();
        //            }
        //            else
        //            {
        //                return BadRequest("Error creating teacher");
        //            }
        //        }
        //        else
        //        {
        //            return BadRequest("Username already taken");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "An error occurred while creating a new Teacher User.");
        //        return StatusCode(500, "An error occurred while creating a new Teacher User. Please try again.");
        //    }
        //}

        //[HttpGet]
        //public async Task<IActionResult> GetAllTeachers()
        //{
        //    try
        //    {
        //        var teachers = await _userManager.GetUsersInRoleAsync(SD.Role_Teacher);
        //        var teacherDtos = new List<TeacherDto>();
        //        foreach (var teacher in teachers)
        //        {
        //            var subject = _context.Subjects.FirstOrDefault(s => s.SubjectId == teacher.Subjects.FirstOrDefault().SubjectId);
        //            var subjectDto = _mapper.Map<SubjectDto>(subject);
        //            var teacherDto = new TeacherDto
        //            {
        //                Id = teacher.Id,
        //                UserName = teacher.UserName,
        //                ContactNumber = teacher.PhoneNumber,
        //                Address = teacher.Address,
        //                Subject = subjectDto
        //            };
        //            teacherDtos.Add(teacherDto);
        //        }
        //        return Ok(teacherDtos);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "An error occurred while getting all teachers.");
        //        return StatusCode(500, "An error occurred while retreiving all teachers. Please try again.");
        //    }
        //}


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeacher(string id)
        {
            try
            {
                var teacher = await _userManager.FindByIdAsync(id);
                if (teacher != null)
                {
                    var result = await _userManager.DeleteAsync(teacher);
                    if (result.Succeeded)
                    {
                        return Ok();
                    }
                    else
                    {
                        return BadRequest("Error deleting teacher");
                    }
                }
                else
                {
                    return NotFound("Teacher not found");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting a Teacher User.");
                return StatusCode(500, "An error occurred while deleting a Teacher User. Please try again.");
            }
        }
    }
}