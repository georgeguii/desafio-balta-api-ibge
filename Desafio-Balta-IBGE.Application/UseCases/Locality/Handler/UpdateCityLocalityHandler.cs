using System.Net;
using Desafio_Balta_IBGE.Domain.Interfaces.IBGE;
using Desafio_Balta_IBGE.Domain.Interfaces.UnitOfWork;
using Desafio_Balta_IBGE.Application.UseCases.Locality.Request;
using Desafio_Balta_IBGE.Application.UseCases.Locality.Response;
using Desafio_Balta_IBGE.Application.Abstractions.Locality;

namespace Desafio_Balta_IBGE.Application.UseCases.Locality.Handler;

public class UpdateCityLocalityHandler : IUpdateCityLocalityHandler
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IIbgeRepository _ibgeRepository;

    public UpdateCityLocalityHandler(IUnitOfWork unitOfWork, IIbgeRepository ibgeRepository)
    {
        _unitOfWork = unitOfWork;
        _ibgeRepository = ibgeRepository;
    }

    public async Task<UpdateCityLocalityResponse> Handle(UpdateCityLocalityRequest request, CancellationToken cancellationToken)
    {
        #region Validações

        var result = request.Validar();

        if (!result.IsValid)

            return new UpdateCityLocalityResponse(StatusCode: HttpStatusCode.BadRequest,
                                         Message: "Requisição inválida. Por favor, valide os dados informados.",
                                         Errors: result.Errors.ToDictionary(error => error.PropertyName, error => error.ErrorMessage));

        #endregion

        try
        {
            #region Validação de código do IBGE (Id)
            var ibge = await _ibgeRepository.GetByIdAsync(request.IbgeId);
            if (ibge is null)
                return new UpdateCityLocalityResponse(StatusCode: HttpStatusCode.NotFound,
                                         Message: "O código do IBGE informado não está cadastrado.",
                                         Errors: result.Errors.ToDictionary(error => error.PropertyName, error => error.ErrorMessage));
            #endregion

            #region Atualizar cidade

            ibge.UpdateCity(request.City);
            _unitOfWork.BeginTransaction();

            var updated = await _ibgeRepository.UpdateCityAsync(ibge);
            if (updated == false)
                return new UpdateCityLocalityResponse(StatusCode: HttpStatusCode.InternalServerError,
                                         Message: "Houve um erro ao atualizar a cidade.");

            await _unitOfWork.Commit(cancellationToken);

            #endregion

            return new UpdateCityLocalityResponse(StatusCode: HttpStatusCode.OK,
                                         Message: $"Cidade com o código {ibge.IbgeId} atualizada com sucesso!");
        }
        catch (Exception)
        {
            _unitOfWork.Rollback();
            throw;
        }
        finally
        {
            _unitOfWork.Dispose();
        }
    }

}
