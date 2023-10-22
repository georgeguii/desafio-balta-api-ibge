using Desafio_Balta_IBGE.Application.Abstractions.Locality;
using Desafio_Balta_IBGE.Application.Abstractions.Users;
using Desafio_Balta_IBGE.Application.UseCases.Locality.Handler;
using Desafio_Balta_IBGE.Application.UseCases.Users.Handler;
using Desafio_Balta_IBGE.Domain.Interfaces.BaseRepository;
using Desafio_Balta_IBGE.Domain.Interfaces.IBGE;
using Desafio_Balta_IBGE.Domain.Interfaces.Queries;
using Desafio_Balta_IBGE.Domain.Interfaces.Services;
using Desafio_Balta_IBGE.Domain.Interfaces.UnitOfWork;
using Desafio_Balta_IBGE.Domain.Interfaces.UserRepository;
using Desafio_Balta_IBGE.Domain.Queries;
using Desafio_Balta_IBGE.Infra.Data.Context;
using Desafio_Balta_IBGE.Infra.Repositories;
using Desafio_Balta_IBGE.Infra.Services;
using Desafio_Balta_IBGE.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Desafio_Balta_IBGE.API.Extensions.Services;

public static class ServicesExtension
{
    public static void AddContext(this IServiceCollection services, IConfiguration configuration)
    {
        #region Connection
        string connection = configuration.GetConnectionString("Database") ?? throw new NotConnectionDefinedException($"Ñenhuma conexão foi definida.");

        services.AddDbContext<IbgeContext>(options => options.UseSqlServer(connection));
        #endregion
    }

    public static void AddDependencies(this WebApplicationBuilder builder)
    {
        AddRepository(builder);
        AddLogin(builder);
        AddUserHandlers(builder);
        AddLocalityHandler(builder);
        AddQueries(builder);
        AddServices(builder);
    }

    private static void AddServices(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ITokenServices, TokenServices>();
        builder.Services.AddScoped<ILocalityQueriesServices, LocalityQueriesServices>();
        builder.Services.AddScoped<IUserQueriesServices, UserQueriesServices>();
    }

    private static void AddQueries(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ILocalityQueries, LocalityQueries>();
        builder.Services.AddScoped<IUserQueries, UserQueries>();
        
    }

    private static void AddLogin(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ILoginHandler, LoginHandler>();
    }

    private static void AddRepository(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped(serviceType: typeof(IBaseRepository<>), implementationType: typeof(BaseRepository<>));
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IIbgeRepository, IbgeRepository>();
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
    }

    private static void AddLocalityHandler(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ICreateLocalityHandler, CreateLocalityHandler>();
        builder.Services.AddScoped<IDeleteLocalityHandler, DeleteLocalityHandler>();
        builder.Services.AddScoped<IUpdateCityLocalityHandler, UpdateCityLocalityHandler>();
        builder.Services.AddScoped<IUpdateStateLocalityHandler, UpdateStateLocalityHandler>();
    }

    private static void AddUserHandlers(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ICreateUserHandler, CreateUserHandler>();
        builder.Services.AddScoped<IActivateUserHandler, ActivateUserHandler>();
        builder.Services.AddScoped<IDeleteUserHandler, DeleteUserHandler>();
        builder.Services.AddScoped<IUpdateNameUserHandler, UpdateNameUserHandler>();
        builder.Services.AddScoped<IUpdateEmailHandler, UpdateEmailHandler>();
        builder.Services.AddScoped<IUpdatePasswordUserHandler, UpdatePasswordUserHandler>();
    }
}
