using IdentityPostgres.Classes;
using IdentityPostgres.Data;
using IdentityPostgres.Data.Tables;
using IdentityPostgres.Modules.ConfigurationModule.Models;
using Microsoft.EntityFrameworkCore;

namespace IdentityPostgres.Modules.ConfigurationModule.Endpoints
{
    public static class PutMail
    {
        public static async Task<IResult> PutMailConfigurationAsync(MailModel mail, IdentityContext context)
        {
            short providerId = MailHelper.DetermineMailProviderId(mail.Provider);
            if (providerId == -1)
                return Results.NotFound("Provider not found or supported");

            string providerName = MailHelper.DetermineMailProviderName(providerId);
            var configMail = await context.ConfigMail.Where(x => x.ProviderId == providerId).FirstOrDefaultAsync();
            if (configMail == null)
            {
                Guid mailId = Guid.NewGuid();
                configMail = new ConfigMail { Id = mailId, ProviderId = providerId, ApiKey = mail.ApiKey, Email = mail.FromEmail, Name = mail.FromName };
                await context.ConfigMail.AddAsync(configMail);
                string message = $"{providerName} mail configuration setup";
                var config = await context.Config.FirstOrDefaultAsync();
                if (config != null && config.MailId == null)
                {
                    config.MailId = mailId;
                    message = message + " and made the current mail provider";
                }
                await context.SaveChangesAsync();
                return Results.Created(mailId.ToString(), message);
            }

            configMail.ApiKey = mail.ApiKey;
            configMail.Email = mail.FromEmail;
            configMail.Name = mail.FromName;
            configMail.UpdatedOn = DateTime.UtcNow;
            await context.SaveChangesAsync();

            return Results.Ok($"{providerName} mail configuration updated");
        }
    }
}