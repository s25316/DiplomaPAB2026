using Base.Pipelines.Operations;
using GUS.REGON.Errors;
using System.Globalization;

namespace GUS.REGON.Operations.Primitives.FirstValueTo;

internal class FirstValueToDateOnlyOperation : ISyncOperation<string, DateOnly>
{
    private static readonly CultureInfo cultureInfo = new("pl-PL");


    public const string NAME = nameof(FirstValueToDateOnlyOperation);
    public string Name => NAME;


    public OperationResult<DateOnly> Execute(string input)
    {
        if (!DateOnly.TryParse(input, cultureInfo, out DateOnly dateOnly))
        {
            var error = new RegonOperationError.DeserializeToDateOnly(input);
            return OperationResult.Failed<DateOnly>(error);
        }
        return OperationResult.Success(dateOnly);
    }
}