namespace IdentityPostgres.Modules.ConfigurationModule.Models
{
    public class MailTemplateModel
    {
        public string Provider { get; set; }
        public string Type { get; set; }
        public string ProviderTemplateIdentifier { get; set; }

        public MailTemplateModel()
        {
            Provider = "";
            Type = "";
            ProviderTemplateIdentifier = "";
        }
    }
}