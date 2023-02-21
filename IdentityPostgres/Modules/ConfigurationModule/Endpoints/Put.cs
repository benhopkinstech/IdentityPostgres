using IdentityPostgres.Classes;
using IdentityPostgres.Data;
using IdentityPostgres.Data.Tables;
using IdentityPostgres.Modules.ConfigurationModule.Models;
using Microsoft.EntityFrameworkCore;

namespace IdentityPostgres.Modules.ConfigurationModule.Endpoints
{
    public static class Put
    {
        public static async Task<IResult> PutConfigurationAsync(ConfigurationModel configuration, IdentityContext context)
        {
            short providerId = MailHelper.DetermineMailProviderId(configuration.MailProvider);
            if (providerId == -1 && !String.IsNullOrWhiteSpace(configuration.MailProvider))
                return Results.NotFound("Provider not found or supported");

            var configMail = await context.ConfigMail.Where(x => x.ProviderId == providerId).FirstOrDefaultAsync();
            if (configMail == null && !String.IsNullOrWhiteSpace(configuration.MailProvider))
                return Results.NotFound("No mail configuration found for this provider");

            var config = await context.Config.FirstOrDefaultAsync();
            if (config == null)
            {
                await context.Config.AddAsync(new Config { MailId = configMail != null ? configMail.Id : null, AccountVerificationRequired = configuration.AccountVerificationRequired });
                await context.SaveChangesAsync();
                return Results.Created("0", "Configuration created");
            }

            config.MailId = configMail != null ? configMail.Id : null;
            config.AccountVerificationRequired = configuration.AccountVerificationRequired;
            config.UpdatedOn = DateTime.UtcNow;
            await context.SaveChangesAsync();

            return Results.Ok($"Configuration updated");
        }
    }
}