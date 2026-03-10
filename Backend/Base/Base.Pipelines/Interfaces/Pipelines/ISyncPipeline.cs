using Base.Pipelines.Models;

namespace Base.Pipelines.Interfaces.Pipelines;

public interface ISyncPipeline<TInput, TOutput>
{
    PipelineResult<TOutput> Execute(TInput input);
}