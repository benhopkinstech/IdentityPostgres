namespace IdentityPostgres.Modules.ConfigurationModule.Models
{
    public class ConfigurationModel
    {
        public string MailProvider { get; set; }
        public bool AccountVerificationRequired { get; set; }

        public ConfigurationModel()
        {
            MailProvider = "";
            AccountVerificationRequired = false;
        }
    }
}