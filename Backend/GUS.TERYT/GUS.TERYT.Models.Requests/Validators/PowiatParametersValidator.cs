// Ignore Spelling: Powiat, Validator
using Base.Models.Validators;
using FluentValidation;
using GUS.TERYT.Models.Requests.Parameters;

namespace GUS.TERYT.Models.Requests.Validators;

public class PowiatParametersValidator : AbstractValidator<PowiatParameters>
{
    public PowiatParametersValidator(PaginationValidator p)
    {
        RuleFor(x => x.Pagination)
            .NotNull()
            .SetValidator(p);
    }
}