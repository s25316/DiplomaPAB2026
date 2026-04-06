// Ignore Spelling: Type
using Base.Pipelines.Operations;
using System.Xml.Linq;
using static GUS.REGON.Errors.RegonOperationError;

namespace GUS.REGON.Errors;

public static class RegonOperationErrors
{
    public static readonly EmptyFirstValue EmptyFirstValue = new();
}

public abstract record RegonOperationError(string Message) : OperationError(Message)
{
    public record NotSuccessResponse(HttpResponseMessage Response) : RegonOperationError("");
    public record UnableExtractEnvelope(string Content) : RegonOperationError("");
    public record UnableMapToXmlDocument(string Content) : RegonOperationError("");
    public record DeserializeToClass(Type Type, IEnumerable<XElement> Elements) : RegonOperationError("");
    public record DeserializeToBool(string Input) : RegonOperationError("");
    public record DeserializeToDateOnly(string Input) : RegonOperationError("");
    public record DeserializeToEnum(Type Type, string Input) : RegonOperationError("");
    public record EmptyFirstValue() : RegonOperationError("");
    public record NotImplemented(string Message) : RegonOperationError(Message);
}

