using System.Net;
using Desafio_Balta_IBGE.Domain.Interfaces.IBGE;
using Desafio_Balta_IBGE.Domain.Interfaces.UnitOfWork;
using Desafio_Balta_IBGE.Application.UseCases.Locality.Request;
using Desafio_Balta_IBGE.Application.UseCases.Locality.Response;
using Desafio_Balta_IBGE.Domain.Models;

namespace Desafio_Balta_IBGE.Application.UseCases.Locality.Handler;

public class CreateLocalityHandler
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IIbgeRepository _ibgeRepository;

    public CreateLocalityHandler(IUnitOfWork unitOfWork, IIbgeRepository ibgeRepository)
    {
        _unitOfWork = unitOfWork;
        _ibgeRepository = ibgeRepository;
    }

    public async Task<CreateLocalityResponse> Handle(CreateLocalityRequest request, CancellationToken cancellationToken)
    {
        #region Validações de entrada 
        var result = request.Validar();

        if (!result.IsValid)
        {
            return new CreateLocalityResponse(StatusCode: HttpStatusCode.BadRequest,
                                                 Message: "Requisição inválida. Por favor, valide os dados informados.",
                                                 Errors: result.Errors.ToDictionary(error => error.PropertyName, error => error.ErrorMessage));
        }
        #endregion

        try
        {
            #region Validação de código do IBGE (Id)
            var verifyIbgeCodeExist = await _ibgeRepository.IsIbgeCodeRegisteredAsync(request.IbgeId);
            if (verifyIbgeCodeExist)
                return new CreateLocalityResponse(StatusCode: HttpStatusCode.Conflict,
                                                Message: "Já existe uma localidade com este código do IBGE cadastrado.",
                                                Errors: result.Errors.ToDictionary(error => error.PropertyName, error => error.ErrorMessage));
            #endregion

            #region Adiciona localidade
            var locality = new Ibge(ibgeId: request.IbgeId,
                                    city: request.City,
                                    state: request.State);

            _unitOfWork.BeginTransaction();
            await _ibgeRepository.AddAsync(locality);
            await _unitOfWork.Commit(cancellationToken);
            #endregion


            return new CreateLocalityResponse(StatusCode: HttpStatusCode.OK,
                                                Message: "Localidade criada com sucesso.",
                                                Errors: result.Errors.ToDictionary(error => error.PropertyName, error => error.ErrorMessage));
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
