using System.Net;
using Desafio_Balta_IBGE.Domain.Models;
using Desafio_Balta_IBGE.Domain.Interfaces.IBGE;
using Desafio_Balta_IBGE.Domain.Interfaces.UnitOfWork;
using Desafio_Balta_IBGE.Application.UseCases.Locality.Request;
using Desafio_Balta_IBGE.Application.Abstractions.Locality;
using Desafio_Balta_IBGE.Domain.Interfaces.Abstractions;
using FluentValidation.Results;
using Desafio_Balta_IBGE.Application.UseCases.Locality.Response;

namespace Desafio_Balta_IBGE.Application.UseCases.Locality.Handler;

public class CreateLocalityHandler : ICreateLocalityHandler
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IIbgeRepository _ibgeRepository;

    public CreateLocalityHandler(IUnitOfWork unitOfWork, IIbgeRepository ibgeRepository)
    {
        _unitOfWork = unitOfWork;
        _ibgeRepository = ibgeRepository;
    }

    public async Task<IResponse> Handle(CreateLocalityRequest request, CancellationToken cancellationToken)
    {
        #region Validações de entrada 
        var result = request.Validar();

        if (!result.IsValid)
            return new InvalidRequest(StatusCode: HttpStatusCode.BadRequest,
                                      Message: "Requisição inválida. Por favor, valide os dados informados.",
                                      Errors: result.Errors.ToDictionary(error => error.PropertyName, error => error.ErrorMessage));
        #endregion

        try
        {
            #region Validação de código do IBGE (Id)

            var isRegistered = await _ibgeRepository.IsIbgeCodeRegisteredAsync(request.IbgeId);
            if (isRegistered)
                return new CodeAlreadyRegistered(StatusCode: HttpStatusCode.Conflict,
                                                 Message: "Já existe uma localidade com este código do IBGE cadastrado.");

            #endregion

            #region Adiciona localidade

            return await AddLocality(request, cancellationToken);

            #endregion
        }
        catch (Exception ex)
        {
            _unitOfWork.Rollback();
            throw new Exception($"Falha ao criar localidade. Detalhe: {ex.Message}");
        }
        finally
        {
            _unitOfWork.Dispose();
        }
    }

    private async Task<IResponse> AddLocality(CreateLocalityRequest request, CancellationToken cancellationToken)
    {
        var locality = new Ibge(ibgeId: request.IbgeId,
                                            city: request.City,
                                            state: request.State);

        _unitOfWork.BeginTransaction();
        await _ibgeRepository.AddAsync(locality);
        await _unitOfWork.Commit(cancellationToken);

        return new CreatedSuccessfully(StatusCode: HttpStatusCode.OK,
                                            Message: "Localidade criada com sucesso.");
    }
}
