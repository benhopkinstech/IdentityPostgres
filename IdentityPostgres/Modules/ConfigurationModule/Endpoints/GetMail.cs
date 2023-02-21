using IdentityPostgres.Classes;
using IdentityPostgres.Data;
using IdentityPostgres.Modules.ConfigurationModule.Models;
using Microsoft.EntityFrameworkCore;

namespace IdentityPostgres.Modules.ConfigurationModule.Endpoints
{
    public static class GetMail
    {
        public static async Task<IResult> GetMailConfigurationsAsync(IdentityContext context)
        {
            var configMails = await context.ConfigMail.ToListAsync();

            if (configMails.Count == 0)
                return Results.NotFound("No mail configurations exist");

            var mails = new List<MailModel>();

            foreach (var configMail in configMails)
            {
                var mail = new MailModel();
                mail.Provider = MailHelper.DetermineMailProviderName(configMail.ProviderId);
                mail.ApiKey = configMail.ApiKey;
                mail.FromEmail = configMail.Email;
                mail.FromName = configMail.Name;
                mails.Add(mail);
            }

            return Results.Ok(mails);
        }
    }
}