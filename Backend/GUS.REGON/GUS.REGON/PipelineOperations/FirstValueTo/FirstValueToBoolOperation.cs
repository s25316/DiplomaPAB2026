using Base.Models.Exceptions;
using Base.Pipelines;
using Base.Pipelines.Interfaces;

namespace GUS.REGON.PipelineOperations.FirstValueTo;

internal class FirstValueToBoolOperation : ISyncOperation<string, bool>
{
    public string Name => nameof(FirstValueToBoolOperation);


    public OperationResult<bool> Execute(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            var errorMessage = $"{nameof(input)} is null or empty";
            return OperationResult<bool>.Failed(errorMessage);
        }

        if (!bool.TryParse(input, out bool boolValue))
        {
            var errorMessage = $"Can not parse to bool: {input}";
            return OperationResult<bool>.Failed(errorMessage, new ResourceException.IncorrectFormat(errorMessage));
        }

        return OperationResult.Success(boolValue);
    }
}