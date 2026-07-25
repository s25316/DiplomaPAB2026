using Diploma.Infrastructure.Jobs.Educations;
using Quartz;

namespace Diploma.Infrastructure.Jobs;

public class JobChainerListener : IJobListener
{
    private static readonly SemaphoreSlim semaphore = new(1, 1);
    private static readonly HashSet<string> dictionaryJobs = [
        nameof(EducationDisciplineJob),
    ];

    private static int completedDictionariesCount = 0;

    public string Name => nameof(JobChainerListener);

    public Task JobExecutionVetoed(
        IJobExecutionContext context,
        CancellationToken cancellationToken = default
    ) => Task.CompletedTask;

    public Task JobToBeExecuted(
        IJobExecutionContext context,
        CancellationToken cancellationToken = default
    ) => Task.CompletedTask;

    public async Task JobWasExecuted(
        IJobExecutionContext context,
        JobExecutionException? jobException,
        CancellationToken cancellationToken = default)
    {
        if (jobException != null) return;

        var jobName = context.JobDetail.Key.Name;

        try
        {
            await semaphore.WaitAsync(cancellationToken);

            if (dictionaryJobs.Contains(jobName))
            {
                completedDictionariesCount++;

                if (completedDictionariesCount == dictionaryJobs.Count)
                {
                    completedDictionariesCount = 0;
                    await context.Scheduler.TriggerJob(new JobKey(nameof(EducationInstitutionJob)), cancellationToken);
                }
            }

            if (jobName == nameof(EducationInstitutionJob))
            {
                await context.Scheduler.TriggerJob(new JobKey(nameof(EducationCouseJobs)), cancellationToken);
            }

            if (jobName == nameof(EducationCouseJobs))
            {
                // Final Execution
            }
        }
        finally
        {
            semaphore.Release();
        }
    }
}