using IdentityPostgres.Data;
using IdentityPostgres.Modules.Account.Models;

namespace IdentityPostgres.Modules.Account.Endpoints
{
    public static class PostRegister
    {
        public static async Task<IResult> RegisterAsync(CredentialsModel credentials, IdentityContext context)
        {
            return Results.Ok(credentials);
        }
    }
}
