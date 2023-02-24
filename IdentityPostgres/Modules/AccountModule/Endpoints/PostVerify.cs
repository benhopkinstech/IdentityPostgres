using IdentityPostgres.Data;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace IdentityPostgres.Modules.AccountModule.Endpoints
{
    public static class PostVerify
    {
        public static async Task<IResult> VerifyAsync(string code, IdentityContext context)
        {
            var decodedItems = Encoding.Unicode.GetString(Convert.FromBase64String(code)).Split('&');
            if (decodedItems.Length != 3)
                return Results.NotFound();

            if (!Guid.TryParse(decodedItems[0], out var verificationId) || !Guid.TryParse(decodedItems[1], out var accountId) || !DateTime.TryParse(decodedItems[2], out var verificationCreated))
                return Results.NotFound();

            var accountVerification = await context.AccountVerification.Where(x => x.Id == verificationId).FirstOrDefaultAsync();
            if (accountVerification == null)
                return Results.NotFound();

            var difference = accountVerification.CreatedOn - verificationCreated;
            if (accountVerification.Id != verificationId || accountVerification.AccountId != accountId || difference > TimeSpan.FromSeconds(3))
                return Results.NotFound();

            var account = await context.Account.Where(x => x.Id == accountId).FirstOrDefaultAsync();
            if (account == null) 
                return Results.NotFound();

            if (account.Verified == false)
            {
                account.Verified = true;
                account.VerifiedOn = DateTime.UtcNow;
                account.UpdatedOn = DateTime.UtcNow;
                context.AccountVerification.Remove(accountVerification);
                await context.SaveChangesAsync();
            }

            return Results.Ok("Account verified");
        }
    }
}
