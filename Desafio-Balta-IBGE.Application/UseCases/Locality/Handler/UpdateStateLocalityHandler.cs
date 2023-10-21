using Desafio_Balta_IBGE.Application.Abstractions.Locality;
using Desafio_Balta_IBGE.Application.UseCases.Locality.Request;
using Desafio_Balta_IBGE.Application.UseCases.Locality.Response;
using Desafio_Balta_IBGE.Domain.Interfaces.Abstractions;
using Desafio_Balta_IBGE.Domain.Interfaces.IBGE;
using Desafio_Balta_IBGE.Domain.Interfaces.UnitOfWork;
using Desafio_Balta_IBGE.Domain.Models;
using System.Net;

namespace Desafio_Balta_IBGE.Application.UseCases.Locality.Handler;

public class UpdateStateLocalityHandler : IUpdateStateLocalityHandler
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IIbgeRepository _ibgeRepository;

    public UpdateStateLocalityHandler(IUnitOfWork unitOfWork, IIbgeRepository ibgeRepository)
    {
        _unitOfWork = unitOfWork;
        _ibgeRepository = ibgeRepository;
    }


    public async Task<IResponse> Handle(UpdateStateLocalityRequest request, CancellationToken cancellationToken)
    {
        #region Validações

        var result = request.Validar();
        if (!result.IsValid)
            return new InvalidRequest(StatusCode: HttpStatusCode.BadRequest,
                                      Message: "Requisição inválida. Por favor, valide os dados informados.",
                                      Errors: result.Errors
                                                    .GroupBy(error => error.PropertyName)
                                                    .ToDictionary(group => group.Key, group => group.First().ErrorMessage));

        #endregion

        try
        {
            #region Validação de código do IBGE (Id)

            var ibge = await _ibgeRepository.GetByIdAsync(request.IbgeId);
            if (ibge is null)
                return new CodeAlreadyRegistered(StatusCode: HttpStatusCode.NotFound,
                                                 Message: "O código do IBGE informado não está cadastrado.");

            #endregion

            #region Atualizar estado

            return await UpdateLocality(request, ibge, cancellationToken);

            #endregion
        }
        catch (Exception ex)
        {
            _unitOfWork.Rollback();
            throw new Exception($"Falha ao atualizar localidade. Detalhe: {ex.Message}");
        }
        finally
        {
            _unitOfWork.Dispose();
        }
    }

    private async Task<IResponse> UpdateLocality(UpdateStateLocalityRequest request, Ibge ibge, CancellationToken cancellationToken)
    {
        ibge.UpdateState(request.State);
        _unitOfWork.BeginTransaction();

        var updated = await _ibgeRepository.UpdateStateAsync(ibge);
        if (updated == false)
            return new UpdateError(StatusCode: HttpStatusCode.InternalServerError,
                                   Message: $"Houve um erro ao atualizar o estado do código {request.IbgeId}.");

        await _unitOfWork.Commit(cancellationToken);

        return new UpdateSuccessfully(StatusCode: HttpStatusCode.OK,
                                     Message: $"Cidade com o código {ibge.IbgeId} atualizada com sucesso!");
    }
}
