// Ignore Spelling: Powiat
using Base.Models.Interfaces.Repositories;
using GUS.TERYT.Models.Requests.Parameters;
using GUS.TERYT.Models.Responses;

namespace GUS.TERYT.Application.Repositories;

public interface IPowiatRepository : IRepository<PowiatParameters, Powiat>;