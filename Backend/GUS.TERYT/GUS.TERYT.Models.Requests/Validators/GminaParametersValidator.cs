// Ignore Spelling: Gmina, Validator
using Base.Models.Validators;
using FluentValidation;
using GUS.TERYT.Models.Requests.Parameters;

namespace GUS.TERYT.Models.Requests.Validators;

public class GminaParametersValidator : AbstractValidator<GminaParameters>
{
    public GminaParametersValidator(PaginationValidator p)
    {
        RuleFor(x => x.Pagination)
            .NotNull()
            .SetValidator(p);
    }
}