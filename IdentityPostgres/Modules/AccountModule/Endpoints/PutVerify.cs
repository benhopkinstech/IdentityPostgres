using IdentityPostgres.Classes;
using IdentityPostgres.Data;
using IdentityPostgres.Data.Tables;
using IdentityPostgres.Modules.AccountModule.Models;
using Microsoft.EntityFrameworkCore;

namespace IdentityPostgres.Modules.AccountModule.Endpoints
{
    public static class PutVerify
    {
        public static async Task<IResult> VerifyAsync(Guid accountId, IdentityContext context)
        {
            var account = await context.Account.Where(x => x.Id == accountId).FirstOrDefaultAsync();
            if (account == null)
                return Results.Ok("Account verified");

            if (account.Verified == false)
            {
                account.Verified = true;
                account.VerifiedOn = DateTime.UtcNow;
                account.UpdatedOn = DateTime.UtcNow;
                await context.SaveChangesAsync();
            }

            return Results.Ok("Account verified");
        }
    }
}
