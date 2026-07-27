using Diploma.Application.Interfaces.Security;
using Diploma.Application.Persons.Commands.Authentication.Projections.RefreshTokens;
using Diploma.Domain.Persons.Aggregates;
using Diploma.Models.Persons.Authentication;
using MediatR;

namespace Diploma.Application.Persons.Commands.Authentication.UseCases;

public class RefreshTokenHandler(
    IJwtGenerator jwtGenerator,
    IJwtValidator jwtValidator,
    IJwtNameIdentifierExtractor identifierExtractor,
    IPersonRepository personRepository,
    IPersonRefreshTokenProjectionService personRefreshTokenProjectionService
    ) : IRequestHandler<RefreshTokenHandler.Request, RefreshTokenResult>
{
    public sealed record Request : IRequest<RefreshTokenResult>
    {
        public required string Jwt { get; init; }
        public required RefreshTokenRequest Model { get; init; }
    }


    private static readonly RefreshTokenResult.Failure Failure = new();


    public async Task<RefreshTokenResult> Handle(Request request, CancellationToken cancellationToken)
    {
        var hasGeneratedJwtByThisServer = jwtValidator.IsValid(
            request.Jwt,
            JwtValidtion.Issuer | JwtValidtion.Audience | JwtValidtion.IssuerSigningKey);

        if (!hasGeneratedJwtByThisServer)
            return Failure;

        var jwtPersonId = identifierExtractor.Extract(request.Jwt);

        var projectionResult = await personRefreshTokenProjectionService.GetAsync(
            request.Model.RefreshToken,
            cancellationToken);

        if (!projectionResult.HasValue)
            return Failure;

        var projection = projectionResult.Value;
        var projectionPersonId = projection.PersonId;

        if (jwtPersonId != projectionPersonId.Value)
            return Failure;

        if (projection.HasLogOut)
            return Failure;

        var personResult = await personRepository.GetAsync(projection.PersonId, cancellationToken);

        if (!personResult.HasValue)
            return Failure;

        var person = personResult.Value;

        if (!person.HasActive)
            return Failure;

        var jwtResult = jwtGenerator.Generate(projection.PersonId);
        return new RefreshTokenResult.Success
        {
            JwtToken = jwtResult.Jwt,
            JwtTokenExpiresAt = jwtResult.ExpiresAt,
            RefreshToken = projection.RefreshToken,
            RefreshTokenTokenExpiresAt = jwtResult.ExpiresAt,
        };
    }
}