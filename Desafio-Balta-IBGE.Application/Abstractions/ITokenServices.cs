﻿using Desafio_Balta_IBGE.Application.UseCases.Users.Response;
using Desafio_Balta_IBGE.Domain.Models;
using System.Security.Claims;

namespace Desafio_Balta_IBGE.Domain.Interfaces
{
    public interface ITokenServices
    {
        string GerarToken(User user);
        string GerarToken(IEnumerable<Claim> claims);
    }
}