// Ignore Spelling: Deserialize
using Base.Exceptions;
using Base.Pipelines.Interfaces.Operations;
using Base.Pipelines.Models;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace GUS.REGON.PipelineOperations.Base.ElementsTo;

internal class ElementsToClassesOperation<T> : ISyncOperation<IEnumerable<XElement>, IEnumerable<T>>
    where T : class
{
    public string Name => typeof(ElementsToClassesOperation<>).Name;


    public OperationResult<IEnumerable<T>> Execute(IEnumerable<XElement> input)
    {
        var list = new List<T>();
        var builder = new StringBuilder();

        foreach (XElement element in input)
        {
            var item = Deserialize(element);
            if (item is not null)
            {
                list.Add(item);
            }
            else
            {
                builder.AppendLine($"Can not deserialize to {typeof(T).Name}: {element.Value}");
            }
        }

        if (builder.Length > 0)
        {
            var errorMessage = builder.ToString();
            return OperationResult.Failed<IEnumerable<T>>(errorMessage, new ResourceException.IncorrectFormat(errorMessage));
        }
        return OperationResult.Success<IEnumerable<T>>(list);
    }

    private static T? Deserialize(XElement element)
    {
        if (element.IsEmpty ||
            element.FirstNode == null ||
            string.IsNullOrWhiteSpace(element.Value))
        {
            return null;
        }

        var serializer = new XmlSerializer(typeof(T));
        using var reader = element.CreateReader();

        if (!serializer.CanDeserialize(reader))
        {
            return null;
        }

        return serializer.Deserialize(reader) as T;
    }
}
