using Diploma.Domain.Base.Results;
using Microsoft.AspNetCore.Http;

namespace Diploma.Application.Interfaces.Blobs;

public interface IBlobStorage
{
    Task<OptionalResult<IFormFile>> GetAsync(
        string catalog,
        string blobName,
        CancellationToken cancellationToken = default);

    Task SaveAsync(
        string catalog,
        IFormFile file,
        CancellationToken cancellationToken = default);
}