using CustomiseIdentity.Data;
using CustomiseIdentity.DtoMapping;
using CustomiseIdentity.Identity;
using CustomiseIdentity.Service.Email_Service.Email_Interface;
using CustomiseIdentity.Service.Email_Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Configuration;
using System.Reflection;
using CustomiseIdentity.Email.Email_Template;
using CustomiseIdentity.Repository.iRepository;
using CustomiseIdentity.Repository;

WebApplicationBuilder webApplicationBuilder = WebApplication.CreateBuilder(args);

webApplicationBuilder.Services.AddEntityFrameworkSqlServer().AddDbContext<ApplicationDbContext>(
    option =>
    {
        option.UseSqlServer(webApplicationBuilder.Configuration.GetConnectionString("conStr"));
    }
    );

webApplicationBuilder.Services.AddTransient<IRoleStore<ApplicationRole>, ApplicationRoleStore>();
webApplicationBuilder.Services.AddTransient<UserManager<ApplicationUser>, ApplicationUserManager>();
webApplicationBuilder.Services.AddTransient<SignInManager<ApplicationUser>, ApplicationSignInManager>();
webApplicationBuilder.Services.AddTransient<RoleManager<ApplicationRole>, ApplicationRoleManager>();
webApplicationBuilder.Services.AddTransient<IUserStore<ApplicationUser>, ApplicationUserStore>();
webApplicationBuilder.Services.AddIdentity<ApplicationUser, ApplicationRole>().
    AddEntityFrameworkStores<ApplicationDbContext>().AddUserStore<ApplicationUserStore>().
    AddUserManager<ApplicationUserManager>().AddRoleManager<ApplicationRoleManager>().
    AddSignInManager<ApplicationSignInManager>().AddRoleStore<ApplicationRoleStore>().
    AddDefaultTokenProviders();
//webApplicationBuilder.Services.AddAutoMapper(typeof(Program));
webApplicationBuilder.Services.AddAutoMapper(typeof(Mapping));
webApplicationBuilder.Services.AddControllers();
webApplicationBuilder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
//webApplicationBuilder.Services.AddScoped<ISubjectRepository, SubjectRepository>();
webApplicationBuilder.Services.AddEndpointsApiExplorer();
webApplicationBuilder.Services.AddSwaggerGen();

webApplicationBuilder.Services.AddTransient<IEmailSender, EmailSender>();
webApplicationBuilder.Services.Configure<EmailSetting>(webApplicationBuilder.Configuration.GetSection("EmailSetting"));
webApplicationBuilder.Services.AddTransient<IEmailTemplateService, EmailTemplateService>();



//Middleware
WebApplication app = webApplicationBuilder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(
        c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Idenity")
    );
}
app.MapControllers();
app.Run();