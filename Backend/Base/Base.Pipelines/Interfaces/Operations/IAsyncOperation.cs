using Base.Pipelines.Models;

namespace Base.Pipelines.Interfaces.Operations;

public interface IAsyncOperation<TOutput> : IBaseOperation
{
    Task<OperationResult<TOutput>> ExecuteAsync(CancellationToken cancellationToken = default);
}

public interface IAsyncOperation<in TInput, TOutput> : IBaseOperation
{
    Task<OperationResult<TOutput>> ExecuteAsync(TInput input, CancellationToken cancellationToken = default);
}