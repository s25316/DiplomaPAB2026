using RADON.Models.Responses.Dictionaries;

namespace RADON.Application.Interfaces;

public interface IDictionaryRespository<TKey, TItem>
{
    Task<IDictionary<TKey, TItem>> GetAsync(CancellationToken cancellationToken = default);
    Task CreateOrUpdateAsync(IEnumerable<TItem> items, CancellationToken cancellationToken = default);
}

public interface IRadonDictionaryRespository : IDictionaryRespository<string, DictionaryItem>;

public interface IInstitutionStatusRespository : IRadonDictionaryRespository;
public interface IInstitutionKindRespository : IRadonDictionaryRespository;
public interface IUniversityTypeRespository : IRadonDictionaryRespository;
public interface IScientificInstitutionTypeRespository : IRadonDictionaryRespository;


public interface IRespository<TItem>
{
    //Task<IEnumerable<TItem>> GetAsync(CancellationToken cancellationToken = default);
    Task CreateOrUpdateAsync(IEnumerable<TItem> items, CancellationToken cancellationToken = default);
}

