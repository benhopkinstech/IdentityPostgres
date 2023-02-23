
using IdentityPostgres.Classes;
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

            errors.AddRange(Validation.EmailCheck(mailTest.RecipientEmail));

            if (errors.Count > 0)
                return Results.BadRequest(errors);

            return await next(context);
        }
    }
}