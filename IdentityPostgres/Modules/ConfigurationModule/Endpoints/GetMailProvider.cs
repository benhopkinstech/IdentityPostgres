using IdentityPostgres.Data;
using Microsoft.EntityFrameworkCore;

namespace IdentityPostgres.Modules.ConfigurationModule.Endpoints
{
    public static class GetMailProvider
    {
        public static async Task<IResult> GetMailProvidersAsync(IdentityContext context)
        {
            var configMailProviders = await context.ConfigMailProvider.ToListAsync();

            if (configMailProviders.Count == 0)
                return Results.NotFound("No mail providers exist");

            var mailProviders = new List<string>();

            foreach (var configMailProvider in configMailProviders)
                mailProviders.Add(configMailProvider.Name);

            return Results.Ok(mailProviders);
        }
    }
}