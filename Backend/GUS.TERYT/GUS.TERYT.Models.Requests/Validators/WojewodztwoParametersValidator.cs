// Ignore Spelling: Wojewodztwo, Validator
using Base.Models.Validators;
using FluentValidation;
using GUS.TERYT.Models.Requests.Parameters;

namespace GUS.TERYT.Models.Requests.Validators;

public class WojewodztwoParametersValidator : AbstractValidator<WojewodztwoParameters>
{
    public WojewodztwoParametersValidator(PaginationValidator p)
    {
        RuleFor(x => x.Pagination)
            .NotNull()
            .SetValidator(p);
    }
}