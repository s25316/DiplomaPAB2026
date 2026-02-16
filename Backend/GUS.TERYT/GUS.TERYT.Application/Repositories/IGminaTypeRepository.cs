// Ignore Spelling: Gmina
using Base.Models.Interfaces.Repositories;
using GUS.TERYT.Models.Responses;

namespace GUS.TERYT.Application.Repositories;

public interface IGminaTypeRepository : IDictionaryRepository<string, Gmina.Type>;