﻿using IdentityPostgres.Interfaces;
using IdentityPostgres.Modules.Account.Endpoints;
using Microsoft.AspNetCore.Builder;

namespace IdentityPostgres.Modules.Account
{
    public class AccountModule : IModule
    {
        private string _module = "Account";

        public IServiceCollection RegisterModule(IServiceCollection services)
        {
            return services;
        }

        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapPost($"{_module}/Register", PostRegister.RegisterAsync)
                .Produces(StatusCodes.Status201Created).Produces(StatusCodes.Status409Conflict)
                .WithTags(_module).WithName(nameof(PostRegister.RegisterAsync)).WithOpenApi();
            return endpoints;
        }
    }
}