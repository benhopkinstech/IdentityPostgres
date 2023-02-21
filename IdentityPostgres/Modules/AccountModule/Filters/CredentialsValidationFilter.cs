using IdentityPostgres.Modules.AccountModule.Models;
using System.Net.Mail;

namespace IdentityPostgres.Modules.AccountModule.Filters
{
    public class CredentialsValidationFilter : IEndpointFilter
    {
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            var credentials = context.GetArgument<CredentialsModel>(0);
            var errors = new List<string>();

            if (String.IsNullOrWhiteSpace(credentials.Email))
                errors.Add("Email address must be provided");

            if (credentials.Email.Length > 256)
                errors.Add("Email address maximum length is 256");

            if (!MailAddress.TryCreate(credentials.Email, out _))
                errors.Add("Email address format invalid");

            if (String.IsNullOrWhiteSpace(credentials.Password))
                errors.Add("Password must be provided");

            if (credentials.Password.Length < 8)
                errors.Add("Password minimum length is 8");

            if (credentials.Password.Length > 72)
                errors.Add("Password maximum length is 72");

            if (errors.Count > 0)
                return Results.BadRequest(errors);

            return await next(context);
        }
    }
}