using IdentityPostgres.Classes;
using IdentityPostgres.Data;
using IdentityPostgres.Modules.ConfigurationModule.Models;
using Microsoft.EntityFrameworkCore;

namespace IdentityPostgres.Modules.ConfigurationModule.Endpoints
{
    public static class GetMailTemplate
    {
        public static async Task<IResult> GetMailTemplatesAsync(IdentityContext context)
        {
            var configMailTemplates = await context.ConfigMailTemplate.Include(x => x.Mail).ToListAsync();

            if (configMailTemplates.Count == 0)
                return Results.NotFound("No mail templates exist");

            var mailTemplates = new List<MailTemplateModel>();

            foreach (var configMailTemplate in configMailTemplates)
            {
                var mailTemplate = new MailTemplateModel();
                mailTemplate.Provider = MailHelper.DetermineMailProviderName(configMailTemplate.Mail.ProviderId);
                mailTemplate.Type = MailHelper.DetermineMailTypeName(configMailTemplate.TypeId);
                mailTemplate.ProviderTemplateIdentifier = configMailTemplate.ProviderTemplateIdentifier;
                mailTemplates.Add(mailTemplate);
            }

            return Results.Ok(mailTemplates);
        }
    }
}