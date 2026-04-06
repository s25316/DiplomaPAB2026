// Ignore Spelling: Deserialize
using Base.Pipelines.Operations;
using GUS.REGON.Errors;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace GUS.REGON.Operations.Primitives.ElementsTo;

internal class ElementsToClassesOperation<T> : ISyncOperation<IEnumerable<XElement>, IEnumerable<T>>
    where T : class
{
    private const string NAME = nameof(ElementsToClassesOperation<>);
    public string Name => NAME;


    public OperationResult<IEnumerable<T>> Execute(IEnumerable<XElement> input)
    {
        var list = new List<T>();
        var invalidElements = new List<XElement>();

        foreach (XElement element in input)
        {
            var item = Deserialize(element);
            if (item is not null)
            {
                list.Add(item);
            }
            else
            {
                invalidElements.Add(element);
            }
        }

        if (invalidElements.Any())
        {
            var error = new RegonOperationError.DeserializeToClass(typeof(T), invalidElements);
            return OperationResult.Failed<IEnumerable<T>>(error);
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
