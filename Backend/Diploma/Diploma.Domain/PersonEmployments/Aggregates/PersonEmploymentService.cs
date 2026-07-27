using Base.Models.ValueObjects.Regony;
using Diploma.Domain.Base.Results;
using Diploma.Domain.Companies.Aggregates;

namespace Diploma.Domain.PersonEmployments.Aggregates;

public abstract record PersonEmploymentServiceResult
{
    public sealed record Sucess : PersonEmploymentServiceResult;
    public abstract record Failure : PersonEmploymentServiceResult
    {
        public sealed record NotExist : Failure;
        public sealed record NotExistCompany(Regon Regon) : Failure;
        public sealed record InvalidCompanyDates(DateOnly? Start, DateOnly? End) : Failure;
    }
}

public interface IPersonEmploymentService
{
    Task<OptionalResult<PersonEmployment>> GetAsync(PersonEmploymentId id, CancellationToken cancellationToken = default);
    Task<PersonEmploymentServiceResult> CreateAsync(PersonEmployment item, CancellationToken cancellationToken = default);
    Task<PersonEmploymentServiceResult> UpdateAsync(PersonEmployment item, CancellationToken cancellationToken = default);
    Task<PersonEmploymentServiceResult> DeleteAsync(PersonEmployment item, CancellationToken cancellationToken = default);
}

public class PersonEmploymentService(
    ICompanyRepository companyRepository,
    IPersonEmploymentRepository personEmploymentRepository
    ) : IPersonEmploymentService
{
    private static readonly PersonEmploymentServiceResult.Sucess Sucess = new();
    private static readonly PersonEmploymentServiceResult.Failure.NotExist NotExist = new();


    public async Task<OptionalResult<PersonEmployment>> GetAsync(
        PersonEmploymentId id,
        CancellationToken cancellationToken = default
    ) => await personEmploymentRepository.GetAsync(id, cancellationToken);

    public async Task<PersonEmploymentServiceResult> DeleteAsync(
        PersonEmployment item,
        CancellationToken cancellationToken = default)
    {
        var result = await personEmploymentRepository.DeleteAsync(item, cancellationToken);

        if (result.IsNotFound)
            return NotExist;

        return Sucess;
    }

    public async Task<PersonEmploymentServiceResult> CreateAsync(
        PersonEmployment item,
        CancellationToken cancellationToken = default)
    {
        var result = await IsValidAsync(item, cancellationToken);

        if (result is not null)
            return result;

        await personEmploymentRepository.CreateAsync(item, cancellationToken);
        return Sucess;
    }

    public async Task<PersonEmploymentServiceResult> UpdateAsync(
        PersonEmployment item,
        CancellationToken cancellationToken = default)
    {
        var result = await IsValidAsync(item, cancellationToken);

        if (result is not null)
            return result;

        var updatingResult = await personEmploymentRepository.UpdateAsync(item, cancellationToken);

        if (updatingResult.IsNotFound)
            return NotExist;

        return Sucess;
    }

    private async Task<PersonEmploymentServiceResult.Failure?> IsValidAsync(
        PersonEmployment item,
        CancellationToken cancellationToken = default)
    {
        var companyResult = await companyRepository.GetAsync(item.Regon, cancellationToken);

        if (!companyResult.HasValue)
            return new PersonEmploymentServiceResult.Failure.NotExistCompany(item.Regon);

        var company = companyResult.Value;

        if (item.From < company.StartDate)
        {
            return new PersonEmploymentServiceResult.Failure.InvalidCompanyDates(
                company.StartDate,
                company.EndDate
            );
        }

        if (company.EndDate.HasValue &&
            item.To.HasValue &&
            company.EndDate.Value < item.To.Value)
        {
            return new PersonEmploymentServiceResult.Failure.InvalidCompanyDates(
                company.StartDate,
                company.EndDate
            );
        }

        return null;
    }
}