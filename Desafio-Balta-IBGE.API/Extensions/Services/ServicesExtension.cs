using Microsoft.EntityFrameworkCore;

using Desafio_Balta_IBGE.Infra.Data.Context;

namespace Desafio_Balta_IBGE.API.Extensions.Services;

public static class ServicesExtension
{
    public static void AddContext(this IServiceCollection services, IConfiguration configuration)
    {
        #region Connection
        string? connection = configuration.GetConnectionString("Database");
        services.AddDbContext<IbgeContext>(options => options.UseSqlServer(connection));
        #endregion
    }
}
