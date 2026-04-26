using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;

namespace RADON.Base;

public interface IInputQueryParameters;

public interface IUriBuilder<in TInputQueryParameters>
    where TInputQueryParameters : class, IInputQueryParameters
{
    string Build(TInputQueryParameters parameters);
}

public abstract class BaseUriBuilder<TInputQueryParameters>(Uri baseUri)
    where TInputQueryParameters : class, IInputQueryParameters
{
    protected abstract HashSet<KeyValuePair<string, string>> PrepareInputParameters(TInputQueryParameters parameters);

    public virtual string Build(TInputQueryParameters parameters)
    {
        var items = PrepareInputParameters(parameters);

        var groupedItems = items
            .Where(kvp => !string.IsNullOrWhiteSpace(kvp.Key) && !string.IsNullOrWhiteSpace(kvp.Value))
            .GroupBy(kvp => kvp.Key)
            .ToDictionary(
                g => g.Key,
                g => new StringValues([.. g.Select(x => x.Value)])
            );

        return QueryHelpers.AddQueryString(baseUri.ToString(), groupedItems);
    }


    protected virtual void AddParameter(HashSet<KeyValuePair<string, string>> parameters, string key, string? value)
    {
        if (string.IsNullOrWhiteSpace(key))
            return;

        if (string.IsNullOrWhiteSpace(value))
            return;

        var pair = new KeyValuePair<string, string>(key, value);
        if (parameters.Contains(pair))
            return;

        parameters.Add(pair);
    }
}