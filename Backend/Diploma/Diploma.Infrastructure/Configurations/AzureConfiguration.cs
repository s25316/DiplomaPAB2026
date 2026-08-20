namespace Diploma.Infrastructure.Configurations;

internal class AzureConfiguration
{
    public required string BlobConnectionString { get; init; }
    public required string QueueConnectionString { get; init; }
    public required string TableConnectionString { get; init; }
}
