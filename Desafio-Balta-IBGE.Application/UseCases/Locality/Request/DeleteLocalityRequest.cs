using FluentValidation.Results;
using Desafio_Balta_IBGE.Application.Validators.Locality;

namespace Desafio_Balta_IBGE.Application.UseCases.Locality.Request;

public class DeleteLocalityRequest
{
    public string IbgeId { get; init; }

    public DeleteLocalityRequest(string ibgeId)
    {
        IbgeId = ibgeId.Trim();
    }

    public ValidationResult Validar()
    {
        var validator = new DeleteLocalityValidator();
        return validator.Validate(this);
    }
}
