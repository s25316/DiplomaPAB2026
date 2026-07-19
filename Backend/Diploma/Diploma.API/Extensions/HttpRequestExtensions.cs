using System.Diagnostics.CodeAnalysis;

namespace Diploma.API.Extensions;

public static class HttpRequestExtensions
{
    private const string AUTHORIZATION_HEADER = "Authorization";
    private const string BEARER_START_PART = "Bearer ";


    public static bool TryGetAuthorization(this HttpRequest request, [NotNullWhen(true)] out string? authorization)
    {
        authorization = null;

        var headers = request.Headers;

        if (headers.Count == 0)
            return false;

        if (!request.Headers.TryGetValue(AUTHORIZATION_HEADER, out var authorizationHeader))
            return false;

        authorization = authorizationHeader.ToString();
        return !string.IsNullOrWhiteSpace(authorizationHeader);
    }

    public static bool TryGetJwt(this HttpRequest request, [NotNullWhen(true)] out string? jwtToken)
    {
        jwtToken = null;

        if (!request.TryGetAuthorization(out var authorizationHeader))
            return false;

        if (!authorizationHeader.StartsWith(BEARER_START_PART))
            return false;


        jwtToken = authorizationHeader[BEARER_START_PART.Length..].Trim();
        return !string.IsNullOrWhiteSpace(jwtToken);
    }
}