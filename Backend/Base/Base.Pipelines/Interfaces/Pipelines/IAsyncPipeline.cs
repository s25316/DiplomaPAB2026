using Base.Pipelines.Models;

namespace Base.Pipelines.Interfaces.Pipelines;

public interface IAsyncPipeline<TInput, TOutput>
{
    Task<PipelineResult<TOutput>> ExecuteAsync(TInput input, CancellationToken cancellationToken = default);
}