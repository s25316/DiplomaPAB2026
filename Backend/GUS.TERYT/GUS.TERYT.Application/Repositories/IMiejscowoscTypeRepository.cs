// Ignore Spelling: Miejscowosc
using Base.Models.Interfaces.Repositories;
using GUS.TERYT.Models.Responses;

namespace GUS.TERYT.Application.Repositories;

public interface IMiejscowoscTypeRepository : IDictionaryRepository<string, Miejscowosc.Type>;