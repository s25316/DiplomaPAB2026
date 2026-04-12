using Base.Models.ValueObjects.Regony;

namespace GUS.REGON.Models.Requests;

public class InputParameters
{
    public IList<Regon> Regons { get; init; } = [];
}