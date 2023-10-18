using FluentValidation.Results;
using Desafio_Balta_IBGE.Application.Validators.Locality;

namespace Desafio_Balta_IBGE.Application.UseCases.Locality.Request;

public class GetByIbgeIdLocalityRequest
{
    public string IbgeId { get; init; }

    public GetByIbgeIdLocalityRequest(string ibgeId)
    {
        IbgeId = ibgeId.Trim();
    }

    public ValidationResult Validar()
    {
        var validator = new GetByIbgeIdLocalityValidator();
        return validator.Validate(this);
    }
}
