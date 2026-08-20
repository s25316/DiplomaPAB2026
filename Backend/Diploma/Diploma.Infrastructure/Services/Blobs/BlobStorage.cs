using Azure.Storage.Blobs;
using Diploma.Application.Interfaces.Blobs;
using Diploma.Domain.Base.Results;
using Microsoft.AspNetCore.Http;

namespace Diploma.Infrastructure.Services.Blobs;

public class BlobFormFile(Stream stream, string fileName, long length, string contentType) : IFormFile
{
    private readonly Stream _stream = stream;

    public string ContentType => contentType;
    public string ContentDisposition => $"inline; filename={fileName}";
    public IHeaderDictionary Headers => new HeaderDictionary();
    public long Length => length;
    public string Name => fileName;
    public string FileName => fileName;

    public void CopyTo(Stream target)
    {
        _stream.CopyTo(target);
    }

    public Task CopyToAsync(Stream target, CancellationToken cancellationToken = default)
    {
        return _stream.CopyToAsync(target, cancellationToken);
    }

    public Stream OpenReadStream()
    {
        return _stream;
    }
}

public class BlobStorage(
    BlobServiceClient client
    ) : IBlobStorage
{
    private const string CONTAINER_NAME = "messages";

    public async Task<OptionalResult<IFormFile>> GetAsync(
        string catalog,
        string blobName,
        CancellationToken cancellationToken = default)
    {
        var containerClient = client.GetBlobContainerClient(CONTAINER_NAME);
        var blobClient = containerClient.GetBlobClient($"{catalog}/{blobName}");

        try
        {
            var properties = await blobClient.GetPropertiesAsync(cancellationToken: cancellationToken);
            var downloadInfo = await blobClient.DownloadStreamingAsync(cancellationToken: cancellationToken);

            IFormFile formFile = new BlobFormFile(
                downloadInfo.Value.Content,
                blobName,
                properties.Value.ContentLength,
                properties.Value.ContentType
            );

            return OptionalResult<IFormFile>.Success(formFile);
        }
        catch (Azure.RequestFailedException ex) when (ex.ErrorCode == "BlobNotFound")
        {
            return OptionalResult<IFormFile>.NotFound();
        }
    }

    public async Task SaveAsync(
        string catalog,
        IFormFile file,
        CancellationToken cancellationToken = default)
    {
        var containerClient = client.GetBlobContainerClient(CONTAINER_NAME);
        await containerClient.CreateIfNotExistsAsync(cancellationToken: cancellationToken);

        var blobClient = containerClient.GetBlobClient($"{catalog}/{file.FileName}");

        using var stream = file.OpenReadStream();
        await blobClient.UploadAsync(stream, overwrite: true, cancellationToken: cancellationToken);
    }
}