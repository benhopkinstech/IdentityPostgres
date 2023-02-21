namespace IdentityPostgres.Modules.ConfigurationModule.Models
{
    public class MailTestModel
    {
        public string Provider { get; set; }
        public string Type { get; set; }
        public string RecipientEmail { get; set; }

        public MailTestModel()
        {
            Provider = "";
            Type = "";
            RecipientEmail = "";
        }
    }
}