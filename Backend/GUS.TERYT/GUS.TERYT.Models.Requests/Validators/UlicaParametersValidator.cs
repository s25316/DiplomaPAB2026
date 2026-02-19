// Ignore Spelling: Ulica, Validator
using Base.Models.Validators;
using FluentValidation;
using GUS.TERYT.Models.Requests.Parameters;

namespace GUS.TERYT.Models.Requests.Validators;

public class UlicaParametersValidator : AbstractValidator<UlicaParameters>
{
    public UlicaParametersValidator(PaginationValidator p)
    {
        RuleFor(x => x.Pagination)
            .NotNull()
            .SetValidator(p);
    }
}