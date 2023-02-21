using IdentityPostgres.Interfaces;
using IdentityPostgres.Modules.ConfigurationModule.Endpoints;
using IdentityPostgres.Modules.ConfigurationModule.Filters;

namespace IdentityPostgres.Modules.ConfigurationModule
{
    public class ConfigurationModule : IModule
    {
        private readonly string _module = "Configuration";

        public IServiceCollection RegisterModule(IServiceCollection services)
        {
            return services;
        }

        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGet($"{_module}", Get.GetConfigurationAsync)
                .Produces(StatusCodes.Status200OK).Produces(StatusCodes.Status404NotFound)
                .WithTags(_module).WithName(nameof(Get.GetConfigurationAsync)).WithOpenApi();

            endpoints.MapPut($"{_module}/", Put.PutConfigurationAsync)
                .Produces(StatusCodes.Status200OK).Produces(StatusCodes.Status201Created).Produces(StatusCodes.Status404NotFound)
                .WithTags(_module).WithName(nameof(Put.PutConfigurationAsync)).WithOpenApi();

            endpoints.MapGet($"{_module}/Mail", GetMail.GetMailConfigurationsAsync)
                .Produces(StatusCodes.Status200OK).Produces(StatusCodes.Status404NotFound)
                .WithTags(_module).WithName(nameof(GetMail.GetMailConfigurationsAsync)).WithOpenApi();

            endpoints.MapPut($"{_module}/Mail", PutMail.PutMailConfigurationAsync)
                .AddEndpointFilter<MailValidationFilter>()
                .Produces(StatusCodes.Status200OK).Produces(StatusCodes.Status201Created).Produces(StatusCodes.Status400BadRequest).Produces(StatusCodes.Status404NotFound)
                .WithTags(_module).WithName(nameof(PutMail.PutMailConfigurationAsync)).WithOpenApi();

            endpoints.MapGet($"{_module}/Mail/Provider", GetMailProvider.GetMailProvidersAsync)
                .Produces(StatusCodes.Status200OK).Produces(StatusCodes.Status404NotFound)
                .WithTags(_module).WithName(nameof(GetMailProvider.GetMailProvidersAsync)).WithOpenApi();

            endpoints.MapGet($"{_module}/Mail/Type", GetMailType.GetMailTypesAsync)
                .Produces(StatusCodes.Status200OK).Produces(StatusCodes.Status404NotFound)
                .WithTags(_module).WithName(nameof(GetMailType.GetMailTypesAsync)).WithOpenApi();

            endpoints.MapGet($"{_module}/Mail/Template", GetMailTemplate.GetMailTemplatesAsync)
                .Produces(StatusCodes.Status200OK).Produces(StatusCodes.Status404NotFound)
                .WithTags(_module).WithName(nameof(GetMailTemplate.GetMailTemplatesAsync)).WithOpenApi();

            endpoints.MapPut($"{_module}/Mail/Template", PutMailTemplate.PutMailTemplateAsync)
                .AddEndpointFilter<MailTemplateValidationFilter>()
                .Produces(StatusCodes.Status200OK).Produces(StatusCodes.Status201Created).Produces(StatusCodes.Status400BadRequest).Produces(StatusCodes.Status404NotFound)
                .WithTags(_module).WithName(nameof(PutMailTemplate.PutMailTemplateAsync)).WithOpenApi();

            return endpoints;
        }
    }
}