using Base.Models.ValueObjects.Krsy;
using Base.Models.ValueObjects.Nipy;
using Base.Models.ValueObjects.Regony;
using Microsoft.EntityFrameworkCore;
using RADON.Database;
using RADON.Database.Enums;
using RADON.Database.Models.Institutions;
using RADON.Infrastructure.QueryBuilders.Base;
using RADON.Models.Shared;
using static RADON.Models.Institutions.QueryParameters;

namespace RADON.Infrastructure.QueryBuilders;

public sealed class InstitutionQueryBuilder(RadonDbContext context) : BaseQueryBuilder<Institution>(
    context
    .Institutions
    .AsNoTracking()

    .Include(i => i.DataSource)
    .Include(i => i.InstitutionKind)

    .Include(i => i.NameSnapshots)

    .Include(i => i.TypeSnapshots)
    .ThenInclude(i => i.InstitutionType)

    .Include(i => i.StatusSnapshots)
    .ThenInclude(i => i.InstitutionStatus))
{
    public InstitutionQueryBuilder WithInstitutionUuids(IEnumerable<Guid> values)
    {
        if (!values.Any())
            return this;

        With(query => query.Where(i => values.Contains(i.InstitutionUuid)));
        return this;
    }


    public InstitutionQueryBuilder WithName(string? value)
    {
        var searchWords = SplitToUpperInvariant(value);
        if (!searchWords.Any())
            return this;

        With(query => query.Where(i =>
            i.NameSnapshots.Any(nameSnapshot =>
                searchWords.Any(w =>
                    nameSnapshot.Name.ToUpper().Contains(w)
                )
            )
        ));
        return this;
    }


    public InstitutionQueryBuilder WithRegon(Regon? value)
    {
        if (value is null)
            return this;

        var stringValue = value.To14SCharacters();
        With(query => query.Where(i => i.Regon == stringValue));
        return this;
    }


    public InstitutionQueryBuilder WithNip(Nip? value)
    {
        if (value is null)
            return this;

        var stringValue = value.ToString();
        With(query => query.Where(i => i.Nip == stringValue));
        return this;
    }


    public InstitutionQueryBuilder WithKrs(Krs? value)
    {
        if (value is null)
            return this;

        var stringValue = value.ToString();
        With(query => query.Where(i => i.Krs == stringValue));
        return this;
    }


    public InstitutionQueryBuilder WithKindCodes(IEnumerable<string> values)
    {
        if (!values.Any())
            return this;

        With(query => query.Where(i =>
            values.Any(code => i.InstitutionKindCode == code)
        ));
        return this;
    }


    public InstitutionQueryBuilder WithUniversityTypeCodes(IEnumerable<string> values)
    {
        WithInstitutionTypeCodes(values, InstitutionClassificationCode.UNIVERSITY);
        return this;
    }

    public InstitutionQueryBuilder WithScientificInstitutionTypeCodes(IEnumerable<string> values)
    {
        WithInstitutionTypeCodes(values, InstitutionClassificationCode.SCIENTIFIC_INSTITUTION);
        return this;
    }
    private void WithInstitutionTypeCodes(
        IEnumerable<string> values,
        InstitutionClassificationCode institutionClassificationCode)
    {
        if (!values.Any())
            return;

        var classificationCode = ((int)institutionClassificationCode).ToString();
        With(query => query.Where(i =>
            i.TypeSnapshots
            .OrderByDescending(typeSnapshot => typeSnapshot.Date)
            .Take(1)
            .Any(typeSnapshot =>
                typeSnapshot.InstitutionType.ClassificationCode == classificationCode &&
                values.Contains(typeSnapshot.InstitutionType.InstitutionTypeCode)
            )
        ));
    }


    public InstitutionQueryBuilder WithStatusCodes(IEnumerable<string> values)
    {
        if (!values.Any())
            return this;

        With(query => query.Where(i =>
            i.StatusSnapshots
            .OrderByDescending(statusSnapshot => statusSnapshot.Date)
            .Take(1)
            .Any(statusSnapshot =>
                values.Contains(statusSnapshot.InstitutionStatusCode)
            )
        ));
        return this;
    }

    public InstitutionQueryBuilder WithOrderBy(
        QueryParametersOrderBy orderBy,
        Order order,
        QueryParametersPagination pagination)
    {
        With(query => orderBy switch
        {
            QueryParametersOrderBy.Name => order == Order.Ascending
                ? query.OrderBy(i => i.NameSnapshots
                    .OrderByDescending(nameSnapshot => nameSnapshot.Date).First().Name)
                : query.OrderByDescending(i => i.NameSnapshots
                    .OrderByDescending(nameSnapshot => nameSnapshot.Date).First().Name),

            QueryParametersOrderBy.StartDate => order == Order.Ascending
                ? query.OrderBy(i => i.StartDate)
                : query.OrderByDescending(i => i.StartDate),

            _ => throw new NotImplementedException(orderBy.ToString())

        });
        Paginate(pagination);
        return this;
    }
}