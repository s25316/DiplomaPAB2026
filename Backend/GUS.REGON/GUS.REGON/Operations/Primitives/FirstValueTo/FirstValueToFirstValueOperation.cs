using Base.Pipelines.Operations;

namespace GUS.REGON.Operations.Primitives.FirstValueTo;

internal class FirstValueToFirstValueOperation : ISyncOperation<string, string>
{
    private const string NAME = nameof(FirstValueToFirstValueOperation);
    public string Name => NAME;


    public OperationResult<string> Execute(string input) => OperationResult.Success(input);
}