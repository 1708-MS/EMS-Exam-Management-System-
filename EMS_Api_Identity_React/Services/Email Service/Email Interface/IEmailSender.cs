using Microsoft.AspNetCore.Mvc;

namespace EMS_Api_Identity_React.Services.Email_Service.Email_Interface
{
    public interface IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage);
    }
}
