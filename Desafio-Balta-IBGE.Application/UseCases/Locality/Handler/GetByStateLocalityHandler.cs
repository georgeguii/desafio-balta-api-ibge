using System.Net;
using Desafio_Balta_IBGE.Domain.Interfaces.IBGE;
using Desafio_Balta_IBGE.Domain.Interfaces.UnitOfWork;
using Desafio_Balta_IBGE.Application.UseCases.Locality.Request;
using Desafio_Balta_IBGE.Application.UseCases.Locality.Response;

namespace Desafio_Balta_IBGE.Application.UseCases.Locality.Handler;
internal class GetByStateLocalityHandler
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IIbgeRepository _ibgeRepository;

    public GetByStateLocalityHandler(IUnitOfWork unitOfWork, IIbgeRepository ibgeRepository)
    {
        _unitOfWork = unitOfWork;
        _ibgeRepository = ibgeRepository;
    }

    public async Task<GetLocalityByStateResponse> Handler(GetByStateLocalityRequest request, CancellationToken cancellationToken)
    {
        #region Validações de entrada 
        var result = request.Validar();

        if (!result.IsValid)
            return new GetLocalityByStateResponse(StatusCode: HttpStatusCode.BadRequest,
                                                 Message: "Requisição inválida. Por favor, valide os dados informados.",
                                                 Errors: result.Errors.ToDictionary(error => error.PropertyName, error => error.ErrorMessage));
        #endregion

        try
        {
            var listLocality = await _ibgeRepository.SearchAsync(x => x.State.Contains(request.State));

            return new GetLocalityByStateResponse(StatusCode: HttpStatusCode.OK, localities: listLocality);
        }
        catch (Exception)
        {
            throw;
        }
    }
}
