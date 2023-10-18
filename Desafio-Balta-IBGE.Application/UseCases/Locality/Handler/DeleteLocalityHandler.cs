using System.Net;
using Desafio_Balta_IBGE.Domain.Interfaces.IBGE;
using Desafio_Balta_IBGE.Domain.Interfaces.UnitOfWork;
using Desafio_Balta_IBGE.Application.UseCases.Locality.Request;
using Desafio_Balta_IBGE.Application.UseCases.Locality.Response;

namespace Desafio_Balta_IBGE.Application.UseCases.Locality.Handler;

public class DeleteLocalityHandler
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IIbgeRepository _ibgeRepository;

    public DeleteLocalityHandler(IUnitOfWork unitOfWork, IIbgeRepository ibgeRepository)
    {
        _unitOfWork = unitOfWork;
       _ibgeRepository = ibgeRepository;
    }

    public async Task<DeleteLocalityResponse> Handle(DeleteLocalityRequest request, CancellationToken cancellationToken)
    {
        #region Validações de entrada 
        var result = request.Validar();

        if (!result.IsValid)
        {
            return new DeleteLocalityResponse(StatusCode: HttpStatusCode.BadRequest,
                                                 Message: "Requisição inválida. Por favor, valide os dados informados.",
                                                 Errors: result.Errors.ToDictionary(error => error.PropertyName, error => error.ErrorMessage));
        }
        #endregion

        try
        {
            #region Validação de código do IBGE (Id)
            var ibge = await _ibgeRepository.GetByIdAsync(request.IbgeId);
            if (ibge is null)
                return new DeleteLocalityResponse(StatusCode: HttpStatusCode.NotFound,
                                         Message: "Código do IBGE informado não está cadastrado.",
                                         Errors: result.Errors.ToDictionary(error => error.PropertyName, error => error.ErrorMessage));
            #endregion

            _unitOfWork.BeginTransaction();
            _ibgeRepository.Delete(ibge);
            await _unitOfWork.Commit(cancellationToken);

            return new DeleteLocalityResponse(StatusCode: HttpStatusCode.OK,
                                Message: "Localidade criada com sucesso.",
                                Errors: result.Errors.ToDictionary(error => error.PropertyName, error => error.ErrorMessage));
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            _unitOfWork.Dispose();
        }
    }
}
