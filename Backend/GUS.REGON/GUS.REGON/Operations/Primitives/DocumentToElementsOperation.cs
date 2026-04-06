using Base.Pipelines.Operations;
using GUS.REGON.Configurations;
using System.Xml.Linq;

namespace GUS.REGON.Operations.Primitives;

internal class DocumentToElementsOperation(ElementDefinition definition) : ISyncOperation<XDocument, IEnumerable<XElement>>
{
    private const string NAME = nameof(DocumentToElementsOperation);
    public string Name { get; } = NAME;


    public OperationResult<IEnumerable<XElement>> Execute(XDocument input)
    {
        var definitions = input.Descendants(definition.Namespace + definition.ElementName);
        return OperationResult.Success(definitions);
    }
}