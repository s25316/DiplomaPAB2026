using Quartz;

namespace RADON.Infrastructure.Jobs;

public class JobConfigurator(IServiceCollectionQuartzConfigurator configurator)
{
    public JobConfigurator AddDictionaryJob<T>()
        where T : IJob
    {
        var jobName = GetJobName<T>();
        configurator.AddJob<T>(opts => opts.WithIdentity(jobName).StoreDurably());
        configurator.AddTrigger(opts => opts.ForJob(jobName).StartNow());
        return this;
    }

    public JobConfigurator AddJob<T>()
        where T : IJob
    {
        var jobName = GetJobName<T>();
        configurator.AddJob<T>(opts => opts.WithIdentity(jobName).StoreDurably());
        return this;
    }

    private static string GetJobName<T>() => typeof(T).Name;
}