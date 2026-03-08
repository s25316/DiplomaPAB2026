using Base.Pipelines.Interfaces;

namespace Base.Pipelines;

public static class PipelineBuilder
{
    /*public static PipelineOperation<TInput, TInput> Create<TInput>() => new ("Empty Creating", input => input);

    public static PipelineOperation<TInput, TInput> Create<TInput>(
        string operationName,
        Action<TInput> action
    ) => new(operationName, item =>
    {
        action(item);
        return PipelineOperationResult.Success(item);
    });


    public static PipelineOperation<TInput, TOutput> Create<TInput, TOutput>(
        string operationName,
        Func<TInput, PipelineOperationResult<TOutput>> func
    ) => new(operationName, func);*/


    public static Operation<TInput, TOutput> Create<TInput, TOutput>(ISyncOperation<TInput, TOutput> operation, IEnumerable<Snapshot>? snapshots = null)
        => new(operation.Name, snapshots ?? [], operation.Execute);

    public static Operation<TInput, TOutput> Create<TInput, TOutput>(IAsyncOperation<TInput, TOutput> operation, IEnumerable<Snapshot>? snapshots = null)
        => new(operation.Name, snapshots ?? [], operation.ExecuteAsync);
}