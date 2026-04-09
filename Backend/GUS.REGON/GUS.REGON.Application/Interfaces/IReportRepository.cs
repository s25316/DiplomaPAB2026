using Base.Models.Interfaces.Repositories;
using GUS.REGON.Models.Requests;
using GUS.REGON.Models.Responses;

namespace GUS.REGON.Application.Interfaces;

public interface IReportRepository : IRepository<InputParameters, Result>;