using Base.Pipelines.Operations;
using GUS.REGON.Errors;

namespace GUS.REGON.Operations.Primitives.FirstValueTo;

internal class FirstValueToEnumOperation<TEnum> : ISyncOperation<string, TEnum>
    where TEnum : struct, Enum
{
    public const string NAME = nameof(FirstValueToEnumOperation<>);
    public string Name => NAME;


    public OperationResult<TEnum> Execute(string input)
    {
        if (!Enum.TryParse<TEnum>(input.ToCharArray(), true, out TEnum result) ||
            !Enum.IsDefined(typeof(TEnum), result))
        {
            var error = new RegonOperationError.DeserializeToEnum(typeof(TEnum), input);
            return OperationResult.Failed<TEnum>(error);
        }
        return OperationResult.Success(result);
    }
}