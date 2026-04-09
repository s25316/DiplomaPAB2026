using Base.Models.Interfaces.Repositories;
using Base.Models.ValueObjects.Regony;
using System.ComponentModel;

namespace GUS.REGON.Models.Requests;

public class InputParameters
{
    public IList<Regon> Regons { get; init; } = [];

    public Pagination Pagination { get; init; } = new();

    [DefaultValue(Order.Ascending)]
    public virtual Order Order { get; init; } = Order.Ascending;
}