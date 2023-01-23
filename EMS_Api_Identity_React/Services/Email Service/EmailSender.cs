using EMS_Api_Identity_React.Services.Email_Service.Email_Interface;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Options;

namespace EMS_Api_Identity_React.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailSetting _emailSettings;
        public EmailSender(IOptions<EmailSetting> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            try
            {
                string toemail = string.IsNullOrEmpty(email) ? _emailSettings.ToEmail : email;
                MailMessage mail = new MailMessage()
                {
                    From = new MailAddress(_emailSettings.UserEmail, "Test Email")
                };
                mail.To.Add(new MailAddress(toemail));
                mail.Subject = "Test Email";
                mail.Body = htmlMessage;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                using (SmtpClient smtp = new SmtpClient(_emailSettings.PrimaryDomain, _emailSettings.PrimaryPort))
                {
                    smtp.Credentials = new NetworkCredential(_emailSettings.UserEmail, _emailSettings.UserPassword);
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(mail);
                }
            }
            catch (Exception ex)
            {
                // Handle the exception here
                string str = ex.Message;
            }
        }

    }
}
