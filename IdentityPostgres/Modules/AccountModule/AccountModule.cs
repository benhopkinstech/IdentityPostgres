using IdentityPostgres.Interfaces;
using IdentityPostgres.Modules.AccountModule.Endpoints;
using IdentityPostgres.Modules.AccountModule.Filters;

namespace IdentityPostgres.Modules.AccountModule
{
    public class AccountModule : IModule
    {
        private readonly string _module = "Account";

        public IServiceCollection RegisterModule(IServiceCollection services)
        {
            return services;
        }

        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapPost($"{_module}/Register", PostRegister.RegisterAsync)
                .AddEndpointFilter<CredentialsValidationFilter>()
                .Produces(StatusCodes.Status201Created).Produces(StatusCodes.Status400BadRequest).Produces(StatusCodes.Status409Conflict)
                .WithTags(_module).WithName(nameof(PostRegister.RegisterAsync)).WithOpenApi();

            endpoints.MapPost($"{_module}/Login", PostLogin.LoginAsync)
                .AddEndpointFilter<CredentialsValidationFilter>()
                .Produces(StatusCodes.Status200OK).Produces(StatusCodes.Status400BadRequest).Produces(StatusCodes.Status401Unauthorized).Produces(StatusCodes.Status403Forbidden)
                .WithTags(_module).WithName(nameof(PostLogin.LoginAsync)).WithOpenApi();

            return endpoints;
        }
    }
}