namespace Base.Pipelines.Interfaces;

public interface ISyncOperation<in TInput, TOutput> : IBaseOperation
{
    OperationResult<TOutput> Execute(TInput input);
}