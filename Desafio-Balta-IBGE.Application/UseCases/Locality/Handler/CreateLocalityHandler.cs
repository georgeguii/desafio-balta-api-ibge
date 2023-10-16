using System.Net;
using Desafio_Balta_IBGE.Domain.Interfaces.IBGE;
using Desafio_Balta_IBGE.Domain.Interfaces.UnitOfWork;
using Desafio_Balta_IBGE.Application.UseCases.Locality.Request;
using Desafio_Balta_IBGE.Application.UseCases.Locality.Response;

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
        #region Validações

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


            return new CreateLocalityResponse(StatusCode: HttpStatusCode.OK,
                                                Message: "Localidade criada com sucesso.",
                                                Errors: result.Errors.ToDictionary(error => error.PropertyName, error => error.ErrorMessage));
        }
        catch (Exception)
        {

            throw;
        }



        

    }
}
