namespace Base.Pipelines.Operations;

public delegate OperationResult<TOutput> SyncOperationHandler<in TInput, TOutput>(TInput input);

public interface ISyncOperation<TOutput> : IBaseOperation
{
    OperationResult<TOutput> Execute();
}

public interface ISyncOperation<in TInput, TOutput> : IBaseOperation
{
    OperationResult<TOutput> Execute(TInput input);
}

internal class SyncOperation
{
}