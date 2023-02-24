using IdentityPostgres.Classes;
using IdentityPostgres.Data;
using IdentityPostgres.Data.Tables;
using IdentityPostgres.Modules.AccountModule.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace IdentityPostgres.Modules.AccountModule.Endpoints
{
    public static class PostRegister
    {
        public static async Task<IResult> RegisterAsync(CredentialsModel credentials, IdentityContext context, HttpContext httpContext)
        {
            if (await context.Account.AnyAsync(x => x.ProviderId == (short)Enums.AccountProvider.LocalIdentity && x.Email == credentials.Email))
                return Results.Conflict("Email already in use");

            var accountId = Guid.NewGuid();
            var account = new Account { Id = accountId, Email = credentials.Email };
            var password = new AccountPassword { AccountId = accountId, Hash = Encryption.GenerateHash(credentials.Password) };
            var verificationId = Guid.NewGuid();
            var verificationCreated = DateTime.UtcNow;
            var verification =  new AccountVerification { Id = verificationId, AccountId = accountId, CreatedOn = verificationCreated };
            await context.Account.AddAsync(account);
            await context.AccountPassword.AddAsync(password);
            await context.AccountVerification.AddAsync(verification);
            await context.SaveChangesAsync();

            var config = await context.Config.Include(x => x.Mail).FirstOrDefaultAsync();
            if (config != null && config.Mail != null)
            {
                var baseUrl = $"{httpContext.Request.Scheme}://{httpContext.Request.Host}";
                var url = baseUrl + "/account/verify?code=" + Convert.ToBase64String(Encoding.Unicode.GetBytes($"{verificationId}&{accountId}&{verificationCreated}"));
                await MailHelper.SendMailAsync(config.Mail, Enums.MailType.EmailVerification, credentials.Email, url);
            }

            return Results.Created(accountId.ToString(), "Account created");
        }
    }
}
