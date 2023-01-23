namespace EMS_Api_Identity_React.Services
{
    public class EmailSetting
    {
        public String PrimaryDomain { get; set; }
        public int PrimaryPort { get; set; }
        public String UserEmail { get; set; }
        public String UserPassword { get; set; }
        public String FromEmail { get; set; }
        public String ToEmail { get; set; }
    }
}
