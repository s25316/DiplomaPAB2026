namespace GUS.TERYT.Infrastructure.Configurations;

public class AzureConfiguration
{
    public string BlobConnectionString { get; init; } = null!;
    public string QueueConnectionString { get; init; } = null!;
    public string TableConnectionString { get; init; } = null!;
}