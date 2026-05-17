using GUS.REGON.Models;
using GUS.REGON.Models.Responses;

namespace GUS.REGON.Application.Interfaces;

public interface IRequestRepository
{
    Task<IEnumerable<Report.Full>> GetAsync(
        QueryParameters parameters,
        CancellationToken cancellationToken = default);
}