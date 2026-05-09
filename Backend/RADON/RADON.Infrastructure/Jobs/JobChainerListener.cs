using Quartz;

namespace RADON.Infrastructure.Jobs;

public class JobChainerListener : IJobListener
{
    private static readonly SemaphoreSlim semaphore = new(1, 1);
    private static readonly HashSet<string> dictionaryJobs = [
        // --- INSTITUTIONS ---
        nameof(UpdateInstitutionKindJob),
        nameof(UpdateInstitutionStatusJob),
        nameof(UpdateUniversityTypeJob),
        nameof(UpdateScientificInstitutionTypeJob),

        // --- COURSES ---
        nameof(UpdateCourseFormJob),
        nameof(UpdateCourseInstanceStatusJob),
        nameof(UpdateCourseLevelJob),
        nameof(UpdateCourseProfileJob),
        nameof(UpdateCourseStatusJob),
        nameof(UpdateLanguageJob),
        nameof(UpdateProfessionalTitleJob),

        // --- SHARED ---
        nameof(UpdateDisciplineJob),
    ];

    private static int completedDictionariesCount = 0;

    public string Name => nameof(JobChainerListener);


    public async Task JobExecutionVetoed(IJobExecutionContext context, CancellationToken cancellationToken = default) { }
    public async Task JobToBeExecuted(IJobExecutionContext context, CancellationToken cancellationToken = default) { }

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

                if (completedDictionariesCount == dictionaryJobs.Count())
                {
                    completedDictionariesCount = 0;
                    await context.Scheduler.TriggerJob(new JobKey(nameof(UpdateInstitutionJob)), cancellationToken);
                }
            }

            if (jobName == nameof(UpdateInstitutionJob))
            {
                await context.Scheduler.TriggerJob(new JobKey(nameof(UpdateCourseJob)), cancellationToken);
            }

            if (jobName == nameof(UpdateCourseJob))
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