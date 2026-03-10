// Ignore Spelling: xml, sid
using Base.Exceptions;
using Base.Pipelines.Interfaces.Operations;
using Base.Pipelines.Models;
using GUS.REGON.Interfaces;
using System.Text;

namespace GUS.REGON.PipelineOperations.Base;

internal class MakeRequestOperation(HttpClient client, ISessionManager? sessionManager = null) : IAsyncOperation<string, string>
{
    private const string MEDIA_TYPE = "application/soap+xml";
    private const string SESSION_HEADER = "sid";
    private static readonly Encoding encoding = Encoding.UTF8;

    public string Name => nameof(MakeRequestOperation);


    public async Task<OperationResult<string>> ExecuteAsync(string input, CancellationToken cancellationToken = default)
    {
        if (sessionManager is not null)
        {
            var session = sessionManager.Session;
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
            Content = new StringContent(input, encoding, MEDIA_TYPE),
        };

        using var response = await client.SendAsync(requestMessage, cancellationToken);
        var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            var errorMessage = $"Code: {response.StatusCode}; Request: {input}; Response: {responseContent}";
            return OperationResult.Failed<string>(errorMessage, new ResourceException.IncorrectFormat(errorMessage));
        }
        return OperationResult.Success(responseContent);
    }
}