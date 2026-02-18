using Azure.Storage.Blobs;
using GUS.TERYT.Infrastructure.Interfaces;
using System.Text;

namespace GUS.TERYT.Infrastructure.InterfacesImplementation;

public class BlobService(BlobServiceClient blobServiceClient) : IBlobService
{
    public async Task<StreamReader> GetStreamReaderAsync(
        string container,
        string blobName,
        CancellationToken cancellationToken = default)
    {
        var containerClient = blobServiceClient.GetBlobContainerClient(container);
        var blobClient = containerClient.GetBlobClient(blobName);
        Stream blobStream = await blobClient.OpenReadAsync();
        return new StreamReader(blobStream, Encoding.GetEncoding("windows-1250"));
    }
}