using Base.Exceptions;
using Base.Pipelines.Interfaces.Operations;
using Base.Pipelines.Models;

namespace GUS.REGON.PipelineOperations.Base;

internal class ResponseToEnvelopeOperation : ISyncOperation<string, string>
{
    private const int LINES_BEFORE = 6;
    private const int LINES_AFTER = 2;
    private const int RESPONSE_MIN_LINES = LINES_BEFORE + LINES_AFTER + 1;

    public string Name { get; } = nameof(ResponseToEnvelopeOperation);


    public OperationResult<string> Execute(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            var errorMessage = $"{nameof(input)} is empty";
            return OperationResult.Failed<string>(errorMessage, new ResourceException.IncorrectFormat(errorMessage));
        }

        var lines = input.Split("\n");

        if (lines.Length < RESPONSE_MIN_LINES)
        {
            var errorMessage = $"Structure of {nameof(input)} is changed: {input}";
            return OperationResult.Failed<string>(errorMessage, new ResourceException.IncorrectFormat(errorMessage));
        }

        var contentLines = lines[LINES_BEFORE..^LINES_AFTER];
        var envelope = string.Concat(contentLines.Select(l => l.Trim()));

        if (string.IsNullOrWhiteSpace(envelope))
        {
            var errorMessage = $"Structure of {nameof(input)} is changed: {input}";
            return OperationResult.Failed<string>(errorMessage, new ResourceException.IncorrectFormat(errorMessage));
        }

        return OperationResult.Success(envelope);
    }
}