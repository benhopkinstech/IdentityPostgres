using IdentityPostgres.Classes;
using IdentityPostgres.Data;
using IdentityPostgres.Data.Tables;
using IdentityPostgres.Modules.AccountModule.Models;
using Microsoft.EntityFrameworkCore;

namespace IdentityPostgres.Modules.AccountModule.Endpoints
{
    public static class PostLogin
    {
        public static async Task<IResult> LoginAsync(CredentialsModel credentials, IdentityContext context, HttpContext httpContext)
        {
            if (!await context.Account.AnyAsync(x => x.ProviderId == (short)Enums.AccountProvider.LocalIdentity && x.Email == credentials.Email))
                return await AddLoginAsync(context, httpContext, null, credentials.Email, false);

            var account = await context.Account.Where(x => x.ProviderId == (short)Enums.AccountProvider.LocalIdentity && x.Email == credentials.Email).Include(x => x.AccountPassword).FirstOrDefaultAsync();
            if (account == null || account.AccountPassword == null)
                return await AddLoginAsync(context, httpContext, null, credentials.Email, false);

            var correctPassword = Encryption.VerifyHash(credentials.Password, account.AccountPassword.Hash);
            if (!correctPassword)
                return await AddLoginAsync(context, httpContext, account.Id, credentials.Email, false);

            var config = await context.Config.FirstOrDefaultAsync();
            if (config != null && config.AccountVerificationRequired && account.Verified == false)
            {
                await AddLoginAsync(context, httpContext, account.Id, credentials.Email, true);
                return Results.Forbid();
            }

            return await AddLoginAsync(context, httpContext, account.Id, credentials.Email, true);
        }

        private static async Task<IResult> AddLoginAsync(IdentityContext context, HttpContext httpContext, Guid? accountId, string email, bool successful)
        {
            var login = new AccountLogin { AccountId = accountId, Email = email, Successful = successful, IpAddress = httpContext.Connection.RemoteIpAddress };
            await context.AccountLogin.AddAsync(login);
            await context.SaveChangesAsync();
            return successful ? Results.Ok("Successful login") : Results.Unauthorized();
        }
    }
}
