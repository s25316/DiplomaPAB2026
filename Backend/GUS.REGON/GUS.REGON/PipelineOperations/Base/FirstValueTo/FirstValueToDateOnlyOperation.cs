using Base.Exceptions;
using Base.Pipelines.Interfaces.Operations;
using Base.Pipelines.Models;
using System.Globalization;

namespace GUS.REGON.PipelineOperations.Base.FirstValueTo;

internal class FirstValueToDateOnlyOperation : ISyncOperation<string, DateOnly>
{
    private static readonly CultureInfo cultureInfo = new("pl-PL");

    public string Name => nameof(FirstValueToDateOnlyOperation);


    public OperationResult<DateOnly> Execute(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            var errorMessage = $"{nameof(input)} is null or empty";
            return OperationResult<DateOnly>.Failed(errorMessage);
        }

        if (!DateOnly.TryParse(input, cultureInfo, out DateOnly dateOnly))
        {
            var errorMessage = $"Can not parse to {typeof(DateOnly).Name}: {input}";
            return OperationResult.Failed<DateOnly>(errorMessage, new ResourceException.IncorrectFormat(errorMessage));
        }

        return OperationResult.Success(dateOnly);
    }
}