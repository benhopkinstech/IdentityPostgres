using IdentityPostgres.Classes;
using IdentityPostgres.Data;
using IdentityPostgres.Data.Tables;
using IdentityPostgres.Modules.ConfigurationModule.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace IdentityPostgres.Modules.ConfigurationModule.Endpoints
{
    public static class PostMailTest
    {
        public static async Task<IResult> PostMailTestAsync(MailTestModel mailTest, IdentityContext context, HttpContext httpContext)
        {
            short providerId = MailHelper.DetermineMailProviderId(mailTest.Provider);
            if (providerId == -1 && !String.IsNullOrWhiteSpace(mailTest.Provider))
                return Results.NotFound("Provider not found or supported");

            short typeId = MailHelper.DetermineMailTypeId(mailTest.Type);
            if (typeId == -1 && !String.IsNullOrWhiteSpace(mailTest.Type))
                return Results.NotFound("Mail type not found");

            if (typeId == -1)
                typeId = (short)Enums.MailType.Test;

            if (providerId == -1)
            {
                var config = await context.Config.Include(x => x.Mail).FirstOrDefaultAsync();
                if (config == null || config.Mail == null)
                    return Results.NotFound("There is no current mail provider setup");

                return await SendMailAsync(config.Mail, httpContext, typeId, mailTest.RecipientEmail);
            }
            else
            {
                var configMail = await context.ConfigMail.Where(x => x.ProviderId == providerId).FirstOrDefaultAsync();
                if (configMail == null)
                    return Results.NotFound("No mail configuration found for this provider");

                return await SendMailAsync(configMail, httpContext, typeId, mailTest.RecipientEmail);
            }
        }

        private static async Task<IResult> SendMailAsync(ConfigMail configMail, HttpContext httpContext, short typeId, string email)
        {
            var typeName = MailHelper.DetermineMailTypeName(typeId);
            var providerName = MailHelper.DetermineMailProviderName(configMail.ProviderId);
            var baseUrl = $"{httpContext.Request.Scheme}://{httpContext.Request.Host}";
            bool successful = await MailHelper.SendMailAsync(configMail, (Enums.MailType)typeId, email, baseUrl);
            if (!successful)
                return Results.BadRequest($"An error occurred sending {typeName} to {email} via {providerName}");

            return Results.Ok($"Successfully sent {typeName} to {email} via {providerName}");
        }
    }
}