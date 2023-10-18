using System.Net;
using Desafio_Balta_IBGE.Domain.Interfaces.IBGE;
using Desafio_Balta_IBGE.Domain.Interfaces.UnitOfWork;
using Desafio_Balta_IBGE.Application.UseCases.Locality.Request;
using Desafio_Balta_IBGE.Application.UseCases.Locality.Response;

namespace Desafio_Balta_IBGE.Application.UseCases.Locality.Handler;

public class GetByCityLocalityHandler
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IIbgeRepository _ibgeRepository;

    public GetByCityLocalityHandler(IUnitOfWork unitOfWork, IIbgeRepository ibgeRepository)
    {
        _unitOfWork = unitOfWork;
        _ibgeRepository = ibgeRepository;
    }

    public async Task<GetLocalityByCityResponse> Handler(GetByCityLocalityRequest request, CancellationToken cancellationToken)
    {
        #region Validações de entrada 
        var result = request.Validar();

        if (!result.IsValid)
            return new GetLocalityByCityResponse(StatusCode: HttpStatusCode.BadRequest,
                                                 Message: "Requisição inválida. Por favor, valide os dados informados.",
                                                 Errors: result.Errors.ToDictionary(error => error.PropertyName, error => error.ErrorMessage));
        #endregion

        try
        {
            var locality = await _ibgeRepository.GetByCityAsync(request.City);
            if (locality == null)
                return new GetLocalityByCityResponse(StatusCode: HttpStatusCode.NotFound,
                                Message: "Não foi possível encontrar a cidade informada.");

            return new GetLocalityByCityResponse(StatusCode: HttpStatusCode.OK,
                                IbgeId: locality.IbgeId,
                                State: locality.State,
                                City: locality.City);
        }
        catch (Exception)
        {
            throw;
        }
    }

}
