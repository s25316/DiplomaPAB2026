namespace GUS.TERYT.Infrastructure.Interfaces;

public interface IBlobService
{
    Task<StreamReader> GetStreamReaderAsync(
        string container,
        string blobName,
        CancellationToken cancellationToken = default);
}