using Base.Pipelines.Operations;
using GUS.REGON.Errors;
using System.Xml.Linq;

namespace GUS.REGON.Operations.Primitives.ElementsTo;

internal class ElementsToFirstValueOperation : ISyncOperation<IEnumerable<XElement>, string>
{
    private const string NAME = nameof(ElementsToFirstValueOperation);
    public string Name => NAME;


    public OperationResult<string> Execute(IEnumerable<XElement> input)
    {
        var element = input.FirstOrDefault();
        if (element is null ||
            element.IsEmpty ||
            element.FirstNode == null ||
            string.IsNullOrWhiteSpace(element.Value))
        {
            return OperationResult.Failed<string>(RegonOperationErrors.EmptyFirstValue);
        }
        return OperationResult.Success(element.Value);
    }
}