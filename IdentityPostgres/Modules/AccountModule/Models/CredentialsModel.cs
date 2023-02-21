namespace IdentityPostgres.Modules.AccountModule.Models
{
    public class CredentialsModel
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public CredentialsModel()
        {
            Email = "";
            Password = "";
        }
    }
}