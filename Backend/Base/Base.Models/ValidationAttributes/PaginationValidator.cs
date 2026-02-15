// Ignore Spelling: Validator
using Base.Models.Interfaces.Repositories;
using FluentValidation;

namespace Base.Models.ValidationAttributes;

public class PaginationValidator : AbstractValidator<Pagination>
{
    public PaginationValidator()
    {
        RuleFor(x => x.Page)
            .GreaterThanOrEqualTo(1)
            .WithMessage(Messages.ErrorMessagePage);

        RuleFor(x => x.ItemsPerPage)
            .GreaterThanOrEqualTo(1)
            .WithMessage(Messages.ErrorMessageItemsPerPage);
    }
}
