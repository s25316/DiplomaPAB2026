// Ignore Spelling: Miejscowosc, Validator
using Base.Models.Validators;
using FluentValidation;
using GUS.TERYT.Models.Requests.Parameters;

namespace GUS.TERYT.Models.Requests.Validators;

public class MiejscowoscParametersValidator : AbstractValidator<MiejscowoscParameters>
{
    public MiejscowoscParametersValidator(PaginationValidator p)
    {
        RuleFor(x => x.Pagination)
            .NotNull()
            .SetValidator(p);
    }
}