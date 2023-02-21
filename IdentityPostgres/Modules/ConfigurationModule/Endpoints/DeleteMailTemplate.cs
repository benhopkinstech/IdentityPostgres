using IdentityPostgres.Classes;
using IdentityPostgres.Data;
using IdentityPostgres.Modules.ConfigurationModule.Models;
using Microsoft.EntityFrameworkCore;

namespace IdentityPostgres.Modules.ConfigurationModule.Endpoints
{
    public static class DeleteMailTemplate
    {
        public static async Task<IResult> DeleteMailTemplateAsync(string provider, string type, IdentityContext context)
        {
            short providerId = MailHelper.DetermineMailProviderId(provider);
            if (providerId == -1)
                return Results.NotFound("Provider not found or supported");

            short typeId = MailHelper.DetermineMailTypeId(type);
            if (typeId == -1)
                return Results.NotFound("Mail type not found");

            string providerName = MailHelper.DetermineMailProviderName(providerId);
            string typeName = MailHelper.DetermineMailTypeName(typeId);

            var configMail = await context.ConfigMail.Where(x => x.ProviderId == providerId).FirstOrDefaultAsync();
            if (configMail == null)
                return Results.NotFound($"No mail configuration found for provider {providerName}");

            var configMailTemplate = await context.ConfigMailTemplate.Where(x => x.MailId == configMail.Id && x.TypeId == typeId).FirstOrDefaultAsync();
            if (configMailTemplate == null)
                return Results.NotFound($"{providerName} template not found for {typeName}");

            context.Remove(configMailTemplate);
            await context.SaveChangesAsync();

            return Results.NoContent();
        }
    }
}