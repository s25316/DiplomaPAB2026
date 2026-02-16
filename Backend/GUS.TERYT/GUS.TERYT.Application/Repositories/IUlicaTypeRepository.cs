// Ignore Spelling: Ulica
using Base.Models.Interfaces.Repositories;
using GUS.TERYT.Models.Responses;

namespace GUS.TERYT.Application.Repositories;

public interface IUlicaTypeRepository : IDictionaryRepository<int, Ulica.Type>;