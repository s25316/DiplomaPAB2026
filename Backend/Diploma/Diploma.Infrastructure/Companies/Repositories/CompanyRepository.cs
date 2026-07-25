using Base.Models.ValueObjects.Regony;
using Diploma.Domain.Base.Results;
using Diploma.Domain.Companies.Aggregates;
using Diploma.Infrastructure.Companies.Services;
using DomainCompany = Diploma.Domain.Companies.Aggregates.Company;

namespace Diploma.Infrastructure.Companies.Repositories;

public class CompanyRepository(
    ICompanyService service
    ) : ICompanyRepository
{
    private static readonly OptionalResult<DomainCompany> NotFound = OptionalResult.NotFound<DomainCompany>();


    public async Task<OptionalResult<DomainCompany>> GetAsync(CompanyId id, CancellationToken cancellationToken = default)
    {
        var enumerator = service.GetAsync([id.Value], cancellationToken);
        var item = await enumerator.FirstAsync(cancellationToken);

        if (item.Institution is null)
            return NotFound;

        return OptionalResult.Success(new DomainCompany
        {
            Id = new CompanyId() { Value = Regon.Parse(item.Regon) },
            StartDate = item.Institution.Daty.DataRozpoczecia,
            EndDate = item.Institution.Daty.DataZakonczenia,
        });
    }
}