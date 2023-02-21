namespace IdentityPostgres.Modules.ConfigurationModule.Models
{
    public class ConfigurationModel
    {
        public string CurrentMailProvider { get; set; }
        public bool AccountVerificationRequired { get; set; }

        public ConfigurationModel()
        {
            CurrentMailProvider = "";
            AccountVerificationRequired = false;
        }
    }
}