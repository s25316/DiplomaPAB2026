using Base.Exceptions;
using Base.Pipelines.Interfaces.Operations;
using Base.Pipelines.Models;
using System.Net;
using System.Xml.Linq;

namespace GUS.REGON.PipelineOperations.Base;

internal class EnvelopeToDocumentOperation : ISyncOperation<string, XDocument>
{
    public string Name { get; } = nameof(EnvelopeToDocumentOperation);


    public OperationResult<XDocument> Execute(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            var errorMessage = $"{nameof(input)} is empty";
            return OperationResult.Failed<XDocument>(errorMessage, new ResourceException.IncorrectFormat(errorMessage));
        }

        try
        {
            var decodedXml = WebUtility.HtmlDecode(input);
            var envelope = XDocument.Parse(decodedXml);
            return OperationResult.Success(envelope);
        }
        catch (Exception ex)
        {
            var errorMessage = $"XML Parse Error: {input}; ErrorMessage: {ex.Message}";
            return OperationResult.Failed<XDocument>(errorMessage, new ResourceException.IncorrectFormat(errorMessage));
        }
    }

    private static string DecodeXmlEnvelope(string envelope) => envelope
        .Replace("&lt;", "<")
        .Replace("&gt;", ">")
        .Replace("&#xD;", "")
        .Replace("&amp;", "&");
}
