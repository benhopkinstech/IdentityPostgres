using IdentityPostgres.Classes;
using IdentityPostgres.Data;
using IdentityPostgres.Modules.ConfigurationModule.Models;
using Microsoft.EntityFrameworkCore;

namespace IdentityPostgres.Modules.ConfigurationModule.Endpoints
{
    public static class Get
    {
        public static async Task<IResult> GetConfigurationAsync(IdentityContext context)
        {
            var config = await context.Config.Include(x => x.Mail).FirstOrDefaultAsync();

            if (config == null)
                return Results.NotFound("No configuration record found");

            ConfigurationModel configuration = new ConfigurationModel();
            configuration.MailProvider = config.Mail != null ? MailHelper.DetermineMailProviderName(config.Mail.ProviderId) : "";
            configuration.AccountVerificationRequired = config.AccountVerificationRequired;

            return Results.Ok(configuration);
        }
    }
}