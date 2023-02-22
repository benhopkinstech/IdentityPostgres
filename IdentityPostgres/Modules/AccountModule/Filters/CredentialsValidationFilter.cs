using IdentityPostgres.Classes;
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

            errors.AddRange(Validation.EmailValidation(credentials.Email));

            errors.AddRange(Validation.PasswordValidation(credentials.Password));

            if (errors.Count > 0)
                return Results.BadRequest(errors);

            return await next(context);
        }
    }
}