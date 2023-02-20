using System.ComponentModel.DataAnnotations;

namespace IdentityPostgres.Modules.Account.Models
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
