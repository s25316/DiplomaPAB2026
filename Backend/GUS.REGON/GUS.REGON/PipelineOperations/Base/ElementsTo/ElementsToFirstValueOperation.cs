using Base.Pipelines.Interfaces.Operations;
using Base.Pipelines.Models;
using System.Xml.Linq;

namespace GUS.REGON.PipelineOperations.Base.ElementsTo;

internal class ElementsToFirstValueOperation : ISyncOperation<IEnumerable<XElement>, string>
{
    public string Name => nameof(ElementsToFirstValueOperation);

    public OperationResult<string> Execute(IEnumerable<XElement> input)
    {
        var element = input.FirstOrDefault();
        if (element is null ||
            element.IsEmpty ||
            element.FirstNode == null ||
            string.IsNullOrWhiteSpace(element.Value))
        {
            var errorMessage = $"First element is empty";
            return OperationResult.Failed<string>(errorMessage);
        }
        return OperationResult.Success(element.Value);
    }
}