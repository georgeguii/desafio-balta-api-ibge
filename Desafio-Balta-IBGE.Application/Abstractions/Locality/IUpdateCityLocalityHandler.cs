using Desafio_Balta_IBGE.Application.UseCases.Locality.Request;
using Desafio_Balta_IBGE.Application.UseCases.Locality.Response;

namespace Desafio_Balta_IBGE.Application.Abstractions.Locality;

public interface IUpdateCityLocalityHandler
{
    Task<UpdateCityLocalityResponse> Handle(UpdateCityLocalityRequest request, CancellationToken cancellationToken);
}
