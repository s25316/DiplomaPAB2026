using Diploma.Infrastructure.Configurations;
using Diploma.Infrastructure.Services.Generators;
using Microsoft.Extensions.Options;

namespace Diploma.Infrastructure.Persons.Lifecycle.LinkGenerators;

public sealed record PersonActivationLinkInput : LinkGeneratorInput
{
    public required Guid OperationId { get; init; }
}
public interface IPersonActivationLinkGenerator : ILinkGenerator<PersonActivationLinkInput>;

public class PersonActivationLinkGenerator(
    IOptions<FrontendHostConfiguration> options
    ) : IPersonActivationLinkGenerator
{
    private const string PATH_TEMPLATE = "api/person/profile/activate/";


    public Uri Generate(PersonActivationLinkInput input)
    {
        var configuration = options.Value;
        var uri = $"{configuration.Uri.TrimEnd('/')}/{PATH_TEMPLATE}{input.OperationId}";
        return new Uri(uri);
    }
}