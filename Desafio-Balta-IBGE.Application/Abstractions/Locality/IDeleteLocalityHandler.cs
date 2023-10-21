using Desafio_Balta_IBGE.Application.UseCases.Locality.Request;
using Desafio_Balta_IBGE.Application.UseCases.Locality.Response;
using Desafio_Balta_IBGE.Domain.Interfaces.Abstractions;

namespace Desafio_Balta_IBGE.Application.Abstractions.Locality;

public interface IDeleteLocalityHandler
{
    Task<IResponse> Handle(DeleteLocalityRequest request, CancellationToken cancellationToken);
}
