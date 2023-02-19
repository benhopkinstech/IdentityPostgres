namespace IdentityPostgres.Interfaces
{
    public interface IModule
    {
        IServiceCollection RegisterModule(IServiceCollection builder);
        IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints);
    }
}
