using FluentValidation;
using RADON.Models.Shared;

namespace RADON.API.Validators;

public class PaginationValidator : AbstractValidator<QueryParametersPagination>
{
    public PaginationValidator()
    {
        RuleFor(x => x.Page)
            .GreaterThanOrEqualTo(1);

        RuleFor(x => x.ItemsPerPage)
            .GreaterThanOrEqualTo(1);
    }
}