using Diploma.Database;
using Microsoft.EntityFrameworkCore;
using Quartz;

namespace Diploma.Infrastructure.Jobs.Educations;

public class PersonCleanProfileJob(DiplomaDbContext dbContext) : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        var hasPeopleToAnonymization = true;
        do
        {
            var now = DateTimeOffset.Now;
            var peopleToAnonymization = await dbContext.People
                .Where(i =>
                    i.AnonymizedAt <= now && (
                        i.Login != null ||
                        i.Password != null ||
                        i.Salt != null
                    )
                )
                .Take(100)
                .ToListAsync();

            foreach (var person in peopleToAnonymization)
            {
                person.Login = null;
                person.Password = null;
                person.Salt = null;
            }

            if (peopleToAnonymization.Count > 0)
            {
                await dbContext.SaveChangesAsync();
                hasPeopleToAnonymization = true;
            }
            else
            {
                hasPeopleToAnonymization = false;
            }


        } while (hasPeopleToAnonymization);
    }
}