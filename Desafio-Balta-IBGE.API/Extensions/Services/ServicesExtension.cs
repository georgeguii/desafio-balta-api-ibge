﻿using Microsoft.EntityFrameworkCore;

using Desafio_Balta_IBGE.Infra.Services;
using Desafio_Balta_IBGE.Infra.Repositories;
using Desafio_Balta_IBGE.Infra.Data.Context;
using Desafio_Balta_IBGE.Application.Abstractions;
using Desafio_Balta_IBGE.Application.UseCases.Users.Handler;
using Desafio_Balta_IBGE.Domain.Interfaces.UserRepository;
using Desafio_Balta_IBGE.Domain.Interfaces.IBGE;
using Desafio_Balta_IBGE.Domain.Interfaces.Services;
using Desafio_Balta_IBGE.Domain.Interfaces.UnitOfWork;
using Desafio_Balta_IBGE.Domain.Interfaces.BaseRepository;

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

    public static void AddDependencies(this WebApplicationBuilder builder)
    {
        #region BaseRepository

        builder.Services.AddScoped(serviceType: typeof(IBaseRepository<>), implementationType: typeof(BaseRepository<>));

        #endregion

        #region Repositories

        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IIbgeRepository, IbgeRepository>();

        #endregion

        #region User Handlers

        builder.Services.AddScoped<ICreateUserHandler, CreateUserHandler>();
        builder.Services.AddScoped<IActivateUserHandler, ActivateUserHandler>();
        builder.Services.AddScoped<IDeleteUserHandler, DeleteUserHandler>();
        builder.Services.AddScoped<IUpdateNameUserHandler, UpdateNameUserHandler>();
        builder.Services.AddScoped<IUpdateEmailHandler, UpdateEmailHandler>();
        builder.Services.AddScoped<IUpdatePasswordUserHandler, UpdatePasswordUserHandler>();

        #endregion

        #region Services

        builder.Services.AddScoped<IEmailServices, EmailServices>();

        #endregion
    }
}