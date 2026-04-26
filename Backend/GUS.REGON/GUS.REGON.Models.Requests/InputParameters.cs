// Ignore Spelling: regon
using Base.Models.ValueObjects.Regony;
using Microsoft.AspNetCore.Mvc;

namespace GUS.REGON.Models.Requests;

public class InputParameters
{
    [FromQuery(Name = "regon")]
    public IList<Regon> Regons { get; init; } = [];
}