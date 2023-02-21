
using IdentityPostgres.Modules.ConfigurationModule.Models;
using System.Net.Mail;

namespace IdentityPostgres.Modules.ConfigurationModule.Filters
{
    public class MailTestValidationFilter : IEndpointFilter
    {
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            var mailTest = context.GetArgument<MailTestModel>(0);
            var errors = new List<string>();

            if (String.IsNullOrWhiteSpace(mailTest.RecipientEmail))
                errors.Add("Recipient email address must be provided");

            if (mailTest.RecipientEmail.Length > 256)
                errors.Add("Recipient email address maximum length is 256");

            if (!MailAddress.TryCreate(mailTest.RecipientEmail, out _))
                errors.Add("Recipient email address format invalid");

            if (errors.Count > 0)
                return Results.BadRequest(errors);

            return await next(context);
        }
    }
}