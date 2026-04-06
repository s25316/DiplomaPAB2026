namespace Base.Pipelines.Operations;

public delegate Task<OperationResult<TOutput>> AsyncOperationHandler<in TInput, TOutput>(
    TInput input,
    CancellationToken cancellationToken = default);

public interface IAsyncOperation<TOutput> : IBaseOperation
{
    Task<OperationResult<TOutput>> ExecuteAsync(CancellationToken cancellationToken = default);
}

public interface IAsyncOperation<in TInput, TOutput> : IBaseOperation
{
    Task<OperationResult<TOutput>> ExecuteAsync(TInput input, CancellationToken cancellationToken = default);
}

public static class AsyncOperation
{
    public static AsyncOperation<TInput, TOutput> Create<TInput, TOutput>(
        IAsyncOperation<TInput, TOutput> operation
    ) => new(operation);

    public static AsyncOperation<TInput, TOutput> Create<TInput, TOutput>(
        string name,
        AsyncOperationHandler<TInput, TOutput> handler
    ) => new(name, handler);
}

public class AsyncOperation<TInput, TOutput>(
    string name,
    AsyncOperationHandler<TInput, TOutput> handler
) : IAsyncOperation<TInput, TOutput>
{
    public virtual string Name => name;


    public AsyncOperation(IAsyncOperation<TInput, TOutput> operation)
        : this(
            operation.Name,
            operation.ExecuteAsync
        )
    { }

    public AsyncOperation(ISyncOperation<TInput, TOutput> operation)
        : this(
            operation.Name,
            (input, cancellationToken) => Task.FromResult(operation.Execute(input))
        )
    { }


    public virtual AsyncOperation<TInput, TOut> Add<TOut>(
        IAsyncOperation<TOutput, TOut> operation
    ) => new(operation.Name, async (input, cancellationToken) =>
    {
        var result = await handler(input, cancellationToken);
        if (!result.IsSuccess)
        {
            return OperationResult<TOut>.Failed(result.Error);
        }
        return await operation.ExecuteAsync(result.Value, cancellationToken);
    });

    public virtual AsyncOperation<TInput, TOut> Add<TOut>(
        ISyncOperation<TOutput, TOut> operation
    ) => Add(new AsyncOperation<TOutput, TOut>(operation));

    public virtual AsyncOperation<TInput, TOut> Add<TOut>(
        string operaionName,
        Func<TInput, CancellationToken, AsyncOperationHandler<TInput, TOutput>, Task<OperationResult<TOut>>> func
    ) => new(name, async (input, cancellationToken) => await func(input, cancellationToken, handler));


    public virtual async Task<OperationResult<TOutput>> ExecuteAsync(
        TInput input,
        CancellationToken cancellationToken = default
    ) => await handler(input, cancellationToken).ConfigureAwait(false);
}