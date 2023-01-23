using EMS_Api_Identity_React.Data;
using EMS_Api_Identity_React.DtoMapping;
using EMS_Api_Identity_React.Email;
using EMS_Api_Identity_React.Identity;
using EMS_Api_Identity_React.Models.Identity;
using EMS_Api_Identity_React.Services;
using EMS_Api_Identity_React.Services.Email_Service.Email_Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEntityFrameworkSqlServer().AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("conStr"), b => b.MigrationsAssembly("EMS_Api_Identity_React"));
});

// Set up ASP.NET Core Identity as a Service 
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Mapping));
builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.Configure<EmailSetting>(builder.Configuration.GetSection("EmailSetting"));
builder.Services.AddTransient<IEmailTemplateService, EmailTemplateService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
