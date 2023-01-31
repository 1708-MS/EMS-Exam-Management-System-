﻿using AutoMapper;
using CustomiseIdentity.Data;
using CustomiseIdentity.Email.Email_Template;
using CustomiseIdentity.Identity;
using CustomiseIdentity.Models;
using CustomiseIdentity.Models.DTOs;
using CustomiseIdentity.Service.Email_Service.Email_Interface;
using CustomiseIdentity.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security.Cryptography.X509Certificates;

namespace CustomiseIdentity.Controller
{

    [Route("api/Teacher")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly IEmailTemplateService _emailTemplateService;
        private readonly ILogger<AccountController> _logger;
        private readonly ApplicationDbContext _context;

        public TeacherController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager,
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
        //[Authorize(Roles = SD.Role_Admin)]
        [HttpPost]
        public async Task<IActionResult> CreateTeacher(TeacherSignUpDto teacherSignUpDto)
        {
            try
            {
                if (await _userManager.FindByNameAsync(teacherSignUpDto.UserName) == null)
                {
                    var applicationUser = new ApplicationUser
                    {
                        UserName = teacherSignUpDto.UserName,
                        Address = teacherSignUpDto.TeacherAddress,
                        PhoneNumber = teacherSignUpDto.TeacherContactNumber
                    };

                    var result = await _userManager.CreateAsync(applicationUser, teacherSignUpDto.UserPassword);

                    if (result.Succeeded)
                    {
                        // Check if the "Teacher" role exists.
                        if (!await _roleManager.RoleExistsAsync(SD.Role_Teacher))
                        {
                            var Role = new ApplicationRole();
                            Role.Name = SD.Role_Teacher;
                            await _roleManager.CreateAsync(Role);
                        }

                        // If the role is "Teacher", assign the "Teacher" role to the user.
                        if (teacherSignUpDto.UserRole == SD.Role_Teacher)
                        {
                            await _userManager.AddToRoleAsync(applicationUser, SD.Role_Teacher);
                        }
                        var subject = await _context.Subjects.FindAsync(teacherSignUpDto.SubjectId);
                        if (subject != null)
                        {
                            if (applicationUser.Subjects != null)
                                applicationUser.Subjects.Add(subject);
                            else
                            {
                                applicationUser.Subjects = new List<Subject>();
                                applicationUser.Subjects.Add(subject);
                            }
                            await _context.SaveChangesAsync();
                        }
                        return Ok(new { message = "New Teacher User created successfully." });
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
                else return BadRequest("Invalid data");
            }
            catch (Exception ex)
            {
                var requestId = HttpContext.TraceIdentifier;
                _logger.LogError($"RequestId: {requestId} - Error creating user. {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating user");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTeachers()
        {
            try
            {
                var teachers = await _userManager.GetUsersInRoleAsync(SD.Role_Teacher);
                var teacherList = new List<GetAllTeacherDto>();
                foreach (var teacher in teachers)
                {
                    var getAllTeacherDto = new GetAllTeacherDto
                    {
                        TeacherId = teacher.Id,
                        TeacherUserName = teacher.UserName,
                        TeacherAddress = teacher.Address,
                        TeacherContactNumber = teacher.PhoneNumber
                    };
                    teacherList.Add(getAllTeacherDto);
                }
                return Ok(teacherList);
            }
            catch (Exception ex)
            {
                var requestId = HttpContext.TraceIdentifier;
                _logger.LogError($"RequestId: {requestId} - Error getting teachers. {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error getting teachers");
            }
        }




        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateTeacher(string id, UpdateTeacherDto updateTeacherDto)
        //{
        //    try
        //    {
        //        var teacherFromDb = await _userManager.FindByIdAsync(id);
        //        if (teacherFromDb == null)
        //        {
        //            return BadRequest("Teacher not found");
        //        }
        //        teacherFromDb.UserName = updateTeacherDto.TeacherUserName;
        //        teacherFromDb.Address = updateTeacherDto.TeacherAddress;
        //        teacherFromDb.PhoneNumber = updateTeacherDto.TeacherContactNumber;

        //        var result = await _userManager.UpdateAsync(teacherFromDb);
        //        if (result.Succeeded)
        //        {
        //            return Ok("Teacher updated successfully");
        //        }


        //        return BadRequest("Error updating teacher");
        //    }
        //    catch (Exception ex)
        //    {
        //        var requestId = HttpContext.TraceIdentifier;
        //        _logger.LogError($"RequestId: {requestId} - Error updating teacher. {ex}");
        //        return StatusCode(StatusCodes.Status500InternalServerError, "Error updating teacher");
        //    }
        //}

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTeacher(string id, UpdateTeacherDto updateTeacherDto)
        {
            //var teacherFromDb = await _userManager.FindByIdAsync(id);
            var teacherFromDb = await _context.ApplicationUsers
                            .Include(t => t.Subjects)
                            .FirstOrDefaultAsync(t => t.Id == id);
            if (teacherFromDb == null)
            {
                return NotFound("Teacher not found");
            }

            teacherFromDb.Address = updateTeacherDto.TeacherAddress;
            teacherFromDb.PhoneNumber = updateTeacherDto.TeacherContactNumber;

            // remove existing subjects of the teacher
            teacherFromDb.Subjects.Clear();

            // add new subjects for the teacher
            foreach (var subjectId in updateTeacherDto.SubjectIds)
            {
                var subject = await _context.Subjects.FindAsync(subjectId);
                if (subject == null)
                {
                    return BadRequest("Invalid subject id");
                }

                teacherFromDb.Subjects.Add(subject);
            }

            var result = await _userManager.UpdateAsync(teacherFromDb);
            if (result.Succeeded)
            {
                return NoContent();
            }

            return BadRequest("Update teacher failed");
        }

    }
}