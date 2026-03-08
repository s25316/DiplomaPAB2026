using Base.Pipelines.Interfaces;

namespace Base.Pipelines;

public class Operation<TInput, TOutput> :
    ISyncOperation<TInput, TOutput>,
    IAsyncOperation<TInput, TOutput>
{
    private readonly List<Snapshot> snapshots;
    private readonly Func<TInput, CancellationToken, Task<OperationResult<TOutput>>> function;

    public string Name { get; }


    public Operation(
        string operationName,
        IEnumerable<Snapshot> snapshots,
        Func<TInput, CancellationToken, Task<OperationResult<TOutput>>> function)
    {
        Name = operationName;
        this.snapshots = snapshots.ToList();
        this.function = function;
    }

    public Operation(
        string operationName,
        IEnumerable<Snapshot> snapshots,
        Func<TInput, OperationResult<TOutput>> function)
    {
        Name = operationName;
        this.snapshots = snapshots.ToList();
        this.function = (input, cancellationToken) => Task.FromResult(function(input));
    }


    public virtual Operation<TInput, TOut> Add<TOut>(
        ISyncOperation<TOutput, TOut> operation
    ) => new(operation.Name, snapshots, async (item, cancellationToken) =>
    {
        OperationResult<TOutput> result = await function(item, cancellationToken);
        snapshots.Add(new Snapshot(Name, result.IsSuccess, result.ErrorMessage, item, DateTimeOffset.Now));

        if (!result.IsSuccess)
        {
            return OperationResult<TOut>.Failed(result.ErrorMessage, result.Exception);
        }
        return operation.Execute(result.Value);
    });

    public virtual Operation<TInput, TOut> Add<TOut>(
        IAsyncOperation<TOutput, TOut> operation
    ) => new(operation.Name, snapshots, async (item, cancellationToken) =>
    {
        OperationResult<TOutput> result = await function(item, cancellationToken);
        snapshots.Add(new Snapshot(Name, result.IsSuccess, result.ErrorMessage, item, DateTimeOffset.Now));

        if (!result.IsSuccess)
        {
            return OperationResult<TOut>.Failed(result.ErrorMessage, result.Exception);
        }
        return await operation.ExecuteAsync(result.Value, cancellationToken);
    });

    /*
    public PipelineOperation<TInput, TOut> Add<TOut>(
        string operationName,
        Func<TOutput, PipelineOperationResult<TOut>> func
    ) => new(operationName, async (item, cancellationToken) =>
    {
        var result = await function(item, cancellationToken);
        if (!result.IsSuccess)
        {
            return PipelineOperationResult<TOut>.Failed(result.Message, result.Exception);
        }
        return func(result.Result);
    });

    public PipelineOperation<TInput, TOutput> Add(
        string operationName,
        Action<TInput> action
    ) => new(operationName, (item, cancellationToken) =>
    {
        action(item);
        return function(item, cancellationToken);
    });*/

    public virtual IEnumerable<Snapshot> GetSnapshots() => snapshots;
    public virtual OperationResult<TOutput> Execute(TInput input) => function(input, CancellationToken.None).GetAwaiter().GetResult();
    public virtual async Task<OperationResult<TOutput>> ExecuteAsync(
        TInput input,
        CancellationToken cancellationToken = default
    ) => await function(input, cancellationToken).ConfigureAwait(false);
}