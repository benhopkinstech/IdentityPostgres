
using IdentityPostgres.Modules.ConfigurationModule.Models;
using System.Net.Mail;

namespace IdentityPostgres.Modules.ConfigurationModule.Filters
{
    public class MailTemplateValidationFilter : IEndpointFilter
    {
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            var mailTemplate = context.GetArgument<MailTemplateModel>(0);
            var errors = new List<string>();

            if (String.IsNullOrWhiteSpace(mailTemplate.ProviderTemplateIdentifier))
                errors.Add("Provider template identifier must be provided");

            if (mailTemplate.ProviderTemplateIdentifier.Length > 100)
                errors.Add("Provider template identifier maximum length is 100");

            if (errors.Count > 0)
                return Results.BadRequest(errors);

            return await next(context);
        }
    }
}