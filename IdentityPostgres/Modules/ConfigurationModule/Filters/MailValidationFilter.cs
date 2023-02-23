
using IdentityPostgres.Classes;
using IdentityPostgres.Modules.ConfigurationModule.Models;
using System.Net.Mail;

namespace IdentityPostgres.Modules.ConfigurationModule.Filters
{
    public class MailValidationFilter : IEndpointFilter
    {
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            var mail = context.GetArgument<MailModel>(0);
            var errors = new List<string>();

            if (String.IsNullOrWhiteSpace(mail.Provider))
                errors.Add("Provider must be provided");

            if (String.IsNullOrWhiteSpace(mail.ApiKey))
                errors.Add("API Key must be provided");

            if (mail.ApiKey.Length > 256)
                errors.Add("API Key maximum length is 256");

            errors.AddRange(Validation.EmailCheck(mail.FromEmail));

            if (String.IsNullOrWhiteSpace(mail.FromName))
                errors.Add("Name must be provided");

            if (mail.FromName.Length > 70)
                errors.Add("must maximum length is 70");

            if (errors.Count > 0)
                return Results.BadRequest(errors);

            return await next(context);
        }
    }
}