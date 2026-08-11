using Diploma.Infrastructure.Configurations;
using Diploma.Infrastructure.Services.Generators;
using Microsoft.Extensions.Options;

namespace Diploma.Infrastructure.Persons.Lifecycle.LinkGenerators;

public sealed record PersonRestoreLinkInput : LinkGeneratorInput
{
    public required Guid OperationId { get; init; }
}
public interface IPersonRestoreLinkGenerator : ILinkGenerator<PersonActivationLinkInput>;

public class PersonRestoreLinkGenerator(
    IOptions<FrontendHostConfiguration> options
    ) : IPersonRestoreLinkGenerator
{
    private const string PATH_TEMPLATE = "restore/";

    public Uri Generate(PersonActivationLinkInput input)
    {
        var configuration = options.Value;
        var uri = $"{configuration.Uri.TrimEnd('/')}/{PATH_TEMPLATE}{input.OperationId}";
        return new Uri(uri);
    }
}