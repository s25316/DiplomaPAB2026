namespace Base.Pipelines.Interfaces;

public interface IAsyncOperation<in TInput, TOutput> : IBaseOperation
{
    Task<OperationResult<TOutput>> ExecuteAsync(TInput input, CancellationToken cancellationToken = default);
}