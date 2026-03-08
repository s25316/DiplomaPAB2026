using Base.Pipelines;
using Base.Pipelines.Interfaces;

namespace GUS.REGON.PipelineOperations.FirstValueTo;

internal class FirstValueToFirstValueOperation : ISyncOperation<string, string>
{
    public string Name => typeof(FirstValueToFirstValueOperation).Name;

    public OperationResult<string> Execute(string input) => OperationResult.Success(input);
}