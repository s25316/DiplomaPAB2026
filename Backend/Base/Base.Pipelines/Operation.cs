using Base.Pipelines.Interfaces.Operations;
using Base.Pipelines.Models;

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

    public virtual IEnumerable<Snapshot> GetSnapshots() => snapshots;
    public virtual OperationResult<TOutput> Execute(TInput input) => function(input, CancellationToken.None).GetAwaiter().GetResult();
    public virtual async Task<OperationResult<TOutput>> ExecuteAsync(
        TInput input,
        CancellationToken cancellationToken = default
    ) => await function(input, cancellationToken).ConfigureAwait(false);
}