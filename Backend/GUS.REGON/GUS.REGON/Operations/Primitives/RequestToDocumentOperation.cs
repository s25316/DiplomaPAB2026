// Ignore Spelling: xml, sid
using Base.Pipelines.Operations;
using GUS.REGON.Configurations;
using GUS.REGON.Errors;
using GUS.REGON.Interfaces;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Text;
using System.Xml.Linq;

namespace GUS.REGON.Operations.Primitives;

internal sealed record RequestInput(string Envelope, bool IsAuthorize);
internal class RequestToDocumentOperation(
    IHttpClientFactory clientFactory,
    ISessionManager? sessionManager
) : IAsyncOperation<RequestInput, XDocument>
{
    private const string MEDIA_TYPE = "application/soap+xml";
    private const string SESSION_HEADER = "sid";
    private static readonly Encoding encoding = Encoding.UTF8;

    private const int CONTENT_LINES_BEFORE = 6;
    private const int CONTENT_LINES_AFTER = 2;
    private const int CONTENT_MIN_LINES = CONTENT_LINES_BEFORE + CONTENT_LINES_AFTER + 1;

    private const string NAME = nameof(RequestToDocumentOperation);
    public string Name => NAME;


    public RequestToDocumentOperation(IHttpClientFactory clientFactory) : this(clientFactory, null) { }


    public async Task<OperationResult<XDocument>> ExecuteAsync(RequestInput input, CancellationToken cancellationToken = default)
    {
        using var response = await SendAsync(input, cancellationToken);
        var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            var error = new RegonOperationError.NotSuccessResponse(response);
            return OperationResult.Failed<XDocument>(error);
        }

        if (!TryExtractEnvelope(responseContent, out var stringEnvelope))
        {
            var error = new RegonOperationError.UnableExtractEnvelope(responseContent);
            return OperationResult.Failed<XDocument>(error);
        }

        try
        {
            var decodedXml = WebUtility.HtmlDecode(stringEnvelope);
            var envelope = XDocument.Parse(decodedXml);
            return OperationResult.Success(envelope);
        }
        catch
        {
            var error = new RegonOperationError.UnableMapToXmlDocument(responseContent);
            return OperationResult.Failed<XDocument>(error);
        }
    }

    private async Task<HttpResponseMessage> SendAsync(
        RequestInput input,
        CancellationToken cancellationToken)
    {
        using var client = clientFactory.CreateClient(HttpClientNames.BASE);

        if (input.IsAuthorize)
        {
            var session = sessionManager?.Session
                ?? throw new RegonException.RequiredAuthorization();

            if (session.IsExpired)
            {
                await sessionManager.UpdateSessionAsync(cancellationToken);
            }
            session = sessionManager.Session;
            client.DefaultRequestHeaders.Add(SESSION_HEADER, session.SessionId);
        }

        using var requestMessage = new HttpRequestMessage()
        {
            Method = HttpMethod.Post,
            Content = new StringContent(input.Envelope, encoding, MEDIA_TYPE),
        };

        return await client.SendAsync(requestMessage, cancellationToken);
    }

    private static bool TryExtractEnvelope(string? content, [NotNullWhen(true)] out string? envelope)
    {
        envelope = null;
        if (string.IsNullOrWhiteSpace(content))
        {
            return false;
        }

        var lines = content.Split("\n");
        if (lines.Length < CONTENT_MIN_LINES)
        {
            return false;
        }

        var contentLines = lines[CONTENT_LINES_BEFORE..^CONTENT_LINES_AFTER];
        var concatedLines = string.Concat(contentLines.Select(l => l.Trim()));
        if (string.IsNullOrWhiteSpace(concatedLines))
        {
            return false;
        }

        envelope = concatedLines;
        return true;
    }

    private static string DecodeXmlEnvelope(string envelope) => envelope
        .Replace("&lt;", "<")
        .Replace("&gt;", ">")
        .Replace("&#xD;", "")
        .Replace("&amp;", "&");
}