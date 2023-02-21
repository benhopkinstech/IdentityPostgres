using IdentityPostgres.Classes;
using IdentityPostgres.Data;
using IdentityPostgres.Data.Tables;
using IdentityPostgres.Modules.ConfigurationModule.Models;
using Microsoft.EntityFrameworkCore;

namespace IdentityPostgres.Modules.ConfigurationModule.Endpoints
{
    public static class PutMailTemplate
    {
        public static async Task<IResult> PutMailTemplateAsync(MailTemplateModel mailTemplate, IdentityContext context)
        {
            short providerId = MailHelper.DetermineMailProviderId(mailTemplate.Provider);
            if (providerId == -1)
                return Results.NotFound("Provider not found or supported");

            short typeId = MailHelper.DetermineMailTypeId(mailTemplate.Type);
            if (typeId == -1)
                return Results.NotFound("Mail type not found");

            string providerName = MailHelper.DetermineMailProviderName(providerId);
            string typeName = MailHelper.DetermineMailTypeName(typeId);

            var configMail = await context.ConfigMail.Where(x => x.ProviderId == providerId).FirstOrDefaultAsync();
            if (configMail == null)
                return Results.NotFound($"No mail configuration found for provider {providerName}");

            var configMailTemplate = await context.ConfigMailTemplate.Where(x => x.MailId == configMail.Id && x.TypeId == typeId).FirstOrDefaultAsync();
            if (configMailTemplate == null)
            {
                Guid mailTemplateId = Guid.NewGuid();
                await context.ConfigMailTemplate.AddAsync(new ConfigMailTemplate { Id = mailTemplateId, MailId = configMail.Id, TypeId = typeId, ProviderTemplateIdentifier = mailTemplate.ProviderTemplateIdentifier });
                await context.SaveChangesAsync();
                return Results.Created(mailTemplateId.ToString(), $"{providerName} template created for {typeName}");
            }

            configMailTemplate.ProviderTemplateIdentifier = mailTemplate.ProviderTemplateIdentifier;
            configMailTemplate.UpdatedOn = DateTime.UtcNow;
            await context.SaveChangesAsync();

            return Results.Ok($"{providerName} template updated for {typeName}");
        }
    }
}