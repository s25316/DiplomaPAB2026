using Base.Exceptions;
using Base.Pipelines.Interfaces.Operations;
using Base.Pipelines.Models;

namespace GUS.REGON.PipelineOperations.Base.FirstValueTo;

internal class FirstValueToEnumOperation<TEnum> : ISyncOperation<string, TEnum>
    where TEnum : struct, Enum
{
    public string Name => typeof(FirstValueToEnumOperation<>).Name;

    public OperationResult<TEnum> Execute(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            var errorMessage = $"{nameof(input)} is null or empty";
            return OperationResult<TEnum>.Failed(errorMessage);
        }

        if (!Enum.TryParse<TEnum>(input.ToCharArray(), true, out TEnum result) &&
            !Enum.IsDefined(typeof(TEnum), result))
        {
            var errorMessage = $"Can not parse to {typeof(TEnum).Name}: {input}";
            return OperationResult.Failed<TEnum>(errorMessage, new ResourceException.IncorrectFormat(errorMessage));
        }

        return OperationResult.Success(result);
    }
}