using IdentityPostgres.Data;

namespace IdentityPostgres.Modules.Account.Endpoints
{
    public static class PostRegister
    {
        public static async Task<IResult> RegisterAsync(IdentityContext context)
        {
            return Results.Ok();
        }
    }
}
