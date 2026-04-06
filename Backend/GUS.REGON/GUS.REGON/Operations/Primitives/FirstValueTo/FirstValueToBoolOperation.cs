using Base.Pipelines.Operations;
using GUS.REGON.Errors;

namespace GUS.REGON.Operations.Primitives.FirstValueTo;

internal class FirstValueToBoolOperation : ISyncOperation<string, bool>
{
    public const string NAME = nameof(FirstValueToBoolOperation);
    public string Name => NAME;


    public OperationResult<bool> Execute(string input)
    {
        if (!bool.TryParse(input, out bool boolValue))
        {
            var error = new RegonOperationError.DeserializeToBool(input);
            return OperationResult.Failed<bool>(error);
        }
        return OperationResult.Success(boolValue);
    }
}