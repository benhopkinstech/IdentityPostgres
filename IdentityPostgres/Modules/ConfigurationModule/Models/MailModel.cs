namespace IdentityPostgres.Modules.ConfigurationModule.Models
{
    public class MailModel
    {
        public string Provider { get; set; }
        public string ApiKey { get; set; }
        public string FromEmail { get; set; }
        public string FromName { get; set; }

        public MailModel()
        {
            Provider = "";
            ApiKey = "";
            FromEmail = "";
            FromName = "";
        }
    }
}