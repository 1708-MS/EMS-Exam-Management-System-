namespace CustomiseIdentity.Service.Email_Service.Email_Interface
{
    public interface IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage);
    }
}
