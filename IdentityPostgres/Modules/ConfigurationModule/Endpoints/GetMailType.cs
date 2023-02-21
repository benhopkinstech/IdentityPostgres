using IdentityPostgres.Data;
using Microsoft.EntityFrameworkCore;

namespace IdentityPostgres.Modules.ConfigurationModule.Endpoints
{
    public static class GetMailType
    {
        public static async Task<IResult> GetMailTypesAsync(IdentityContext context)
        {
            var configMailTypes = await context.ConfigMailType.ToListAsync();

            if (configMailTypes.Count == 0)
                return Results.NotFound("No mail types exist");

            var mailTypes = new List<string>();

            foreach (var configMailType in configMailTypes)
                mailTypes.Add(configMailType.Name);

            return Results.Ok(mailTypes);
        }
    }
}