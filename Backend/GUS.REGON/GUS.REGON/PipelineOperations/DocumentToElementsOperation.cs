using Base.Pipelines;
using Base.Pipelines.Interfaces;
using GUS.REGON.Configurations;
using System.Xml.Linq;

namespace GUS.REGON.PipelineOperations;

internal class DocumentToElementsOperation(ElementDefinition definition) : ISyncOperation<XDocument, IEnumerable<XElement>>
{
    public string Name { get; } = nameof(DocumentToElementsOperation);


    public OperationResult<IEnumerable<XElement>> Execute(XDocument input)
    {
        var definitions = input.Descendants(definition.Namespace + definition.ElementName);
        return OperationResult.Success(definitions);
    }
}