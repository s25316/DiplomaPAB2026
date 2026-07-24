using Quartz;

namespace Diploma.Infrastructure.Jobs;


public class JobConfigurator(IServiceCollectionQuartzConfigurator configurator)
{
    public JobConfigurator AddDictionaryJob<T>()
        where T : IJob
    {
        var jobName = GetJobName<T>();
        configurator.AddJob<T>(opts => opts.WithIdentity(jobName).StoreDurably());

        configurator.AddTrigger(opts => opts
            .ForJob(jobName)
            .WithIdentity($"{jobName}-StartNow-Trigger")
            .StartNow());

        configurator.AddTrigger(opts => opts
            .ForJob(jobName)
            .WithIdentity($"{jobName}-1-After-Midnight-Trigger")
            .WithCronSchedule("0 0 1 * * ?"));

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