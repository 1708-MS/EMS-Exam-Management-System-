namespace EMS_Api_Identity_React.Email
{
    public class EmailTemplateService : IEmailTemplateService
    {
        private readonly IWebHostEnvironment _env;
        public EmailTemplateService(IWebHostEnvironment env)
        {
            _env = env;
        }
        public string GetWelcomeEmailTemplate()
        {
            var pathToFile = _env.ContentRootPath + "Email"
                  + Path.DirectorySeparatorChar.ToString()
                  + "EmailTemplate"
                  + Path.DirectorySeparatorChar.ToString()
                  + "WelcomeEmailTemplate.html";

            using (StreamReader SourceReader = System.IO.File.OpenText(pathToFile))
            {
                return SourceReader.ReadToEnd();
            }
        }
    }
}
