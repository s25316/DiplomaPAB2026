using Base.Pipelines.Interfaces.Operations;
using Base.Pipelines.Models;

namespace GUS.REGON.PipelineOperations.Base.FirstValueTo;

internal class FirstValueToFirstValueOperation : ISyncOperation<string, string>
{
    public string Name => typeof(FirstValueToFirstValueOperation).Name;

    public OperationResult<string> Execute(string input) => OperationResult.Success(input);
}