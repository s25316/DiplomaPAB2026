using Base.Pipelines.Models;

namespace Base.Pipelines.Interfaces.Operations;

public interface ISyncOperation<TOutput> : IBaseOperation
{
    OperationResult<TOutput> Execute();
}

public interface ISyncOperation<in TInput, TOutput> : IBaseOperation
{
    OperationResult<TOutput> Execute(TInput input);
}