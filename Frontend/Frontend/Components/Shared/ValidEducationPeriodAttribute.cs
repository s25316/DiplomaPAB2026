using System.ComponentModel.DataAnnotations;

namespace Frontend.Components.Shared;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class ValidEducationPeriodAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        // Sprawdzamy, czy obiekt to Twój model (lub inny model posiadający te właściwości)
        var instance = validationContext.ObjectInstance;

        // Pobieramy wartości za pomocą refleksji lub rzutowania (zakładając wspólne właściwości lub interfejs)
        // Możesz też stworzyć interfejs IEducationPeriod, jeśli oba modele (Create i Edit) go implementują.

        int startYear = 0;
        int startSem = 0;
        int? endYear = null;
        int? endSem = null;

        var startYearProp = instance.GetType().GetProperty("StartYear");
        var startSemProp = instance.GetType().GetProperty("StartSemestrId");
        var endYearProp = instance.GetType().GetProperty("EndYear");
        var endSemProp = instance.GetType().GetProperty("EndSemestrId");

        if (startYearProp != null && startSemProp != null)
        {
            startYear = (int)(startYearProp.GetValue(instance) ?? 0);
            startSem = (int)(startSemProp.GetValue(instance) ?? 0);
        }

        if (endYearProp != null && endSemProp != null)
        {
            endYear = (int?)(endYearProp.GetValue(instance));
            endSem = (int?)(endSemProp.GetValue(instance));
        }

        // Jeśli nie podano końca, okres jest poprawny
        if (!endYear.HasValue || !endSem.HasValue || endYear.Value == 0 || endSem.Value == 0)
        {
            return ValidationResult.Success;
        }

        // Walidacja lat: Koniec nie może być wcześniej niż Start
        if (endYear.Value < startYear)
        {
            return new ValidationResult("Rok zakończenia nie może być wcześniejszy niż rok rozpoczęcia.", new[] { nameof(endYear) });
        }

        // Jeśli to ten sam rok, sprawdzamy semestry (zakładając, że wyższy ID semestru oznacza późniejszy semestr, np. 1 = zimowy, 2 = letni)
        if (endYear.Value == startYear && endSem.Value < startSem)
        {
            return new ValidationResult("Semestr zakończenia nie może być wcześniejszy niż semestr rozpoczęcia w tym samym roku.", new[] { nameof(endSem) });
        }

        return ValidationResult.Success;
    }
}