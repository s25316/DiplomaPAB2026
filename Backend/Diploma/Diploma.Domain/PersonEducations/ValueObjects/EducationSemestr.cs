using Diploma.Shared.Semesters;

namespace Diploma.Domain.PersonEducations.ValueObjects;

public sealed record EducationSemestr
{
    private const int SUMMER_START_MONTH = 3;
    private const int WINTER_START_MONTH = 10;

    public int Year { get; }
    public Semester Semester { get; }
    public DateOnly SmesterStart { get; }
    public DateOnly SmesterEnd { get; }


    public EducationSemestr(int year, Semester semester)
    {
        if (year < 1900)
            throw new ArgumentOutOfRangeException(year.ToString());

        Year = year;
        Semester = semester;

        if (semester.Id == Semester.Summer.Id)
        {
            SmesterStart = new DateOnly(year, SUMMER_START_MONTH, 1);
            SmesterEnd = new DateOnly(year, WINTER_START_MONTH, 1).AddDays(-1);
        }
        else if (semester.Id == Semester.Winter.Id)
        {
            SmesterStart = new DateOnly(year, WINTER_START_MONTH, 1);
            SmesterEnd = new DateOnly(year + 1, SUMMER_START_MONTH, 1).AddDays(-1);
        }
        else
        {
            throw new NotImplementedException($"Unknown {typeof(Semester)}: {semester.Id}");
        }
    }
}