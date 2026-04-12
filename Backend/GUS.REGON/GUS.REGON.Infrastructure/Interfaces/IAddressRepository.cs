using DatabaseAddress = GUS.REGON.Database.Models.Addresses.Address;
using RegonAddress = GUS.REGON.Models.RaportJednostki.Address;

namespace GUS.REGON.Infrastructure.Interfaces;

public interface IAddressRepository
{
    Task<DatabaseAddress> GetAsync(RegonAddress input, CancellationToken cancellationToken = default);
}